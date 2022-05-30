using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testes.Dominio.ModuloMateria;
using Testes.Dominio.ModuloQuestao;

namespace Testes.Infra.BancoDeDados.ModuloQuestao
{
    public class RepositorioQuestaoBancoDeDados : IRepositorioQuestao
    {

        private const string enderecoBanco =
               "Data Source=(LocalDB)\\MSSqlLocalDB;" +
               "Initial Catalog=TesteDb;" +
               "Integrated Security=True;" +
               "Pooling=False";

        #region sqls

        private const string sqlInserirQuestao =
            @"INSERT INTO [TBQUESTAO]
                   (
                        [ENUNCIADO],
                        [NUMERO_MATERIA]
                   )
                VALUES
                   (
                        @ENUNCIADO,
                        @NUMERO_MATERIA
                    ); 
                SELECT SCOPE_IDENTITY()";

        private const string sqlInserirAlternativas =
            @"INSERT INTO [TBALTERNATIVA]
                   (
                        [ENUNCIADO],
                        [CORRETA],
                        [NUMERO_QUESTAO]
                   )
                VALUES
                   (
                        @ENUNCIADO,
                        @CORRETA,
                        @NUMERO_QUESTAO
                    ); 
                SELECT SCOPE_IDENTITY()";

        private const string sqlEditarAlternativas = 
            @"UPDATE [TBALTERNATIVA]	
		        SET
			        [ENUNCIADO] = @ENUNCIADO,
                    [CORRETA] = @CORRETA,
			        [NUMERO_QUESTAO] = @NUMERO_QUESTAO
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT 
	            Q.NUMERO,
	            Q.ENUNCIADO,
	            MT.NUMERO AS NUMERO_MATERIA,
                MT.NOME AS NOME_MATERIA
                
            FROM
	            TBQUESTAO AS Q INNER JOIN TBMATERIA AS MT
                ON Q.NUMERO_MATERIA = MT.NUMERO";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
		            Q.NUMERO, 
		            Q.ENUNCIADO,

                    MT.NUMERO AS NUMERO_MATERIA,
                    MT.NOME AS NOME_MATERIA
	            FROM 
		            TBQUESTAO AS Q INNER JOIN TBMATERIA AS MT ON Q.NUMERO_MATERIA = MT.NUMERO
		        WHERE
                    MT.NUMERO = @NUMERO;";

        private const string sqlEditar =
           @"UPDATE [TBQUESTAO]	
		        SET
			        [ENUNCIADO] = @ENUNCIADO,
			        [NUMERO_MATERIA] = @NUMERO_MATERIA
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
           @"DELETE FROM [TBQUESTAO]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluirAlternativasQuestao =
            @"DELETE FROM [TBALTERNATIVA]
		        WHERE
			        [NUMERO_QUESTAO] = @NUMERO_QUESTAO";

        private const string sqlSelecionarAlternativasQuestao =
            @"SELECT 
	            [NUMERO],
                [ENUNCIADO],
                [CORRETA],
                [NUMERO_QUESTAO]
              FROM 
	            [TBALTERNATIVA]
              WHERE 
	            [NUMERO_QUESTAO] = @NUMERO_QUESTAO";
        #endregion

        public ValidationResult Editar(Questao questao)
        {
            //var validador = new ValidadorQuestao();

            //var resultadoValidacao = validador.Validate(questao);

            //if (resultadoValidacao.IsValid == false)
            //    return resultadoValidacao;

            //SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            //SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            //ConfigurarParametrosQuestao(questao, comandoEdicao);

            //conexaoComBanco.Open();
            //comandoEdicao.ExecuteNonQuery();

            //conexaoComBanco.Close();

            //return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluirAlternativasQuestao, conexaoComBanco);

            ValidadorQuestao validador = new ValidadorQuestao();
            var result = validador.Validate(questao);

            ConfigurarParametrosQuestao(questao, comandoEdicao);

            conexaoComBanco.Open();

            comandoExclusao.Parameters.AddWithValue("NUMERO_QUESTAO", questao.Numero);

            comandoExclusao.ExecuteNonQuery();

            comandoEdicao.ExecuteNonQuery();

            EditarAlternativas(questao);

            conexaoComBanco.Close();

            return result;

        }

        public ValidationResult Excluir(Questao questao)
        {
            ExcluirAlternativasQuestao(questao);

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", questao.Numero);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Inserir(Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserirQuestao, conexaoComBanco);

            ValidadorQuestao validador = new ValidadorQuestao();
            var result = validador.Validate(questao);

            ConfigurarParametrosQuestao(questao, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            questao.Numero = Convert.ToInt32(id);

            AdicionarAlternativas(questao);

            conexaoComBanco.Close();

            return result;

        }

        public void EditarAlternativas(Questao questaoSelecionada)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);
            SqlCommand comandoEdicao = new SqlCommand(sqlEditarAlternativas, conexaoComBanco);

            conexaoComBanco.Open();

            foreach (var alternativa in questaoSelecionada.Alternativas)
            {

                comandoEdicao.Parameters.Clear();
                ConfigurarParametrosAlternativas(alternativa, comandoEdicao, questaoSelecionada);

                comandoEdicao.ExecuteNonQuery();

            }

            conexaoComBanco.Close();

        }

        public void AdicionarAlternativas(Questao questaoSelecionada)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);
            SqlCommand comandoInsercao = new SqlCommand(sqlInserirAlternativas, conexaoComBanco);

            conexaoComBanco.Open();

            foreach (var alternativa in questaoSelecionada.Alternativas)
            {

                comandoInsercao.Parameters.Clear();
                ConfigurarParametrosAlternativas(alternativa, comandoInsercao, questaoSelecionada);

                var id = comandoInsercao.ExecuteScalar();
                alternativa.Numero = Convert.ToInt32(id);

            }

            conexaoComBanco.Close();

            Editar(questaoSelecionada);
        }

        private void ConfigurarParametrosQuestao(Questao questao, SqlCommand comandoInsercao)
        {

            comandoInsercao.Parameters.AddWithValue("NUMERO", questao.Numero);
            comandoInsercao.Parameters.AddWithValue("ENUNCIADO", questao.Enunciado);
            comandoInsercao.Parameters.AddWithValue("NUMERO_MATERIA", questao.Materia.Numero);

        }

        private void ConfigurarParametrosAlternativas(AlternativaQuestao alternativaQuestao, SqlCommand comando, Questao questao)
        {

            comando.Parameters.AddWithValue("NUMERO", alternativaQuestao.Numero);
            comando.Parameters.AddWithValue("ENUNCIADO", alternativaQuestao.Enunciado);
            comando.Parameters.AddWithValue("CORRETA", alternativaQuestao.AlternativaCorreta);
            comando.Parameters.AddWithValue("NUMERO_QUESTAO", questao.Numero);

        }

        public Questao SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorQuestao = comandoSelecao.ExecuteReader();

            Questao questao = new Questao();

            if (leitorQuestao.Read())
                questao = ConverterParaQuestao(leitorQuestao);

            conexaoComBanco.Close();

            CarregarAlternativasQuestao(questao);

            return questao;
        }

        public List<Questao> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorQuestao = comandoSelecao.ExecuteReader();

            List<Questao> questoes = new List<Questao>();

            while (leitorQuestao.Read())
            {
                Questao questao = ConverterParaQuestao(leitorQuestao);

                questoes.Add(questao);
            }

            conexaoComBanco.Close();

            return questoes;
        }

        private Questao ConverterParaQuestao(SqlDataReader leitorQuestao)
        {
            int numero = Convert.ToInt32(leitorQuestao["NUMERO"]);
            string enunciado = Convert.ToString(leitorQuestao["ENUNCIADO"]);

            int numeroMateria = Convert.ToInt32(leitorQuestao["NUMERO_MATERIA"]);
            string nomeMateria = Convert.ToString(leitorQuestao["NOME_MATERIA"]);

            var questao = new Questao
            {
                Numero = numero,
                Enunciado = enunciado,
                Materia = new Materia
                {

                    Numero = numeroMateria,
                    Nome = nomeMateria

                }
            };

            return questao;
        }

        private void ExcluirAlternativasQuestao(Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluirAlternativasQuestao, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO_QUESTAO", questao.Numero);

            conexaoComBanco.Open();
            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }

        private void CarregarAlternativasQuestao(Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarAlternativasQuestao, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO_QUESTAO", questao.Numero);

            conexaoComBanco.Open();
            SqlDataReader leitorAlternativa = comandoSelecao.ExecuteReader();

            List<AlternativaQuestao> alternativas = new List<AlternativaQuestao>();

            while (leitorAlternativa.Read())
            {
                AlternativaQuestao alternativa = ConverterParaAlternativa(leitorAlternativa);

                alternativas.Add(alternativa);
            }

            conexaoComBanco.Close();
        }

        private AlternativaQuestao ConverterParaAlternativa(SqlDataReader leitorAlternativa)
        {
            var numero = Convert.ToInt32(leitorAlternativa["NUMERO"]);
            var titulo = Convert.ToString(leitorAlternativa["ENUNCIADO"]);
            var correta = Convert.ToBoolean(leitorAlternativa["CORRETA"]);

            var alternativa = new AlternativaQuestao
            {
                Numero = numero,
                Enunciado = titulo,
                AlternativaCorreta = correta
            };

            return alternativa;
        }
    }
}
