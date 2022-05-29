using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Testes.Dominio.ModuloDisciplina;
using Testes.Dominio.ModuloMateria;

namespace Testes.Infra.BancoDeDados.ModuloMateria
{
    public class RepositorioMateriaBancoDeDados : IRepositorioMateria
    {

        private const string enderecoBanco =
               "Data Source=(LocalDB)\\MSSqlLocalDB;" +
               "Initial Catalog=TesteDb;" +
               "Integrated Security=True;" +
               "Pooling=False";



        private const string sqlInserir =
            @"INSERT INTO [TBMATERIA]
                   (
                        [NOME],
                        [SERIE],
                        [NUMERO_DISCIPLINA]
                   )
                VALUES
                   (
                        @NOME,
                        @SERIE,
                        @NUMERO_DISCIPLINA
                    ); 
                SELECT SCOPE_IDENTITY()";

        private const string sqlEditar =
           @"UPDATE [TBMATERIA]	
		        SET
			        [NOME] = @NOME,
                    [SERIE] = @SERIE,
                    [NUMERO_DISCIPLINA] = @NUMERO_DISCIPLINA
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir = 
            @"DELETE FROM [TBMATERIA] 
                WHERE [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT 
	            MT.NUMERO,
	            MT.NOME,
	            MT.SERIE,
	            D.NUMERO AS NUMERO_DISCIPLINA,
                D.NOME  AS NOME_DISCIPLINA
            FROM
	            TBMATERIA AS MT INNER JOIN TBDISCIPLINA AS D ON
	            MT.NUMERO_DISCIPLINA = D.NUMERO";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
		            MT.NUMERO, 
		            MT.NOME,
                    MT.SERIE,
                    D.NUMERO AS NUMERO_DISCIPLINA,
                    D.NOME AS NOME_DISCIPLINA
	            FROM 
		            TBMATERIA AS MT INNER JOIN TBDISCIPLINA AS D ON MT.NUMERO_DISCIPLINA = D.NUMERO
		        WHERE
                    MT.NUMERO = @NUMERO;";

        public ValidationResult Inserir(Materia materia)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosMateria(materia, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            materia.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return new ValidationResult();
        }

        private static void ConfigurarParametrosMateria(Materia materia, SqlCommand comandoInsercao)
        {
            comandoInsercao.Parameters.AddWithValue("NUMERO", materia.Numero);
            comandoInsercao.Parameters.AddWithValue("NOME", materia.Nome);
            comandoInsercao.Parameters.AddWithValue("SERIE", materia.Serie);
            comandoInsercao.Parameters.AddWithValue("NUMERO_DISCIPLINA", materia.Disciplina.Numero);
        }

        public ValidationResult Editar(Materia materia)
        {
            var validador = new ValidadorMateria();

            var resultadoValidacao = validador.Validate(materia);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosMateria(materia, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Materia materia)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", materia.Numero);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Materia SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorMateria = comandoSelecao.ExecuteReader();

            Materia materia = null;
            if (leitorMateria.Read())
                materia = ConverterParaMateria(leitorMateria);

            conexaoComBanco.Close();

            return materia;
        }

        public List<Materia> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorMateria = comandoSelecao.ExecuteReader();

            List<Materia> materias = new List<Materia>();

            while (leitorMateria.Read())
            {
                Materia materia = ConverterParaMateria(leitorMateria);

                materias.Add(materia);
            }

            conexaoComBanco.Close();

            return materias;
        }

        private Materia ConverterParaMateria(SqlDataReader leitorMateria)
        {
            int numero = Convert.ToInt32(leitorMateria["NUMERO"]);
            string nome = Convert.ToString(leitorMateria["NOME"]);
            int serie = Convert.ToInt32(leitorMateria["SERIE"]);

            int numeroDisciplina = Convert.ToInt32(leitorMateria["NUMERO_DISCIPLINA"]);
            string nomeDisciplina = Convert.ToString(leitorMateria["NOME_DISCIPLINA"]);

            var materia = new Materia
            {
                Numero = numero,
                Nome = nome,
                Serie = (SerieEnum)serie,
                Disciplina = new Disciplina
                {
                    Numero = numeroDisciplina,
                    Nome = nomeDisciplina
                }
            };

            return materia;
        }
    }
}
