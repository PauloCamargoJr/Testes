using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testes.Dominio.ModuloTeste;

namespace Testes.Infra.BancoDeDados.ModuloTeste
{
    public class RepositorioTesteBancoDeDados : IRepositorioTeste
    {

        private const string enderecoBanco =
               "Data Source=(LocalDB)\\MSSqlLocalDB;" +
               "Initial Catalog=TesteDb;" +
               "Integrated Security=True;" +
               "Pooling=False";

        private const string sqlInserir =
            @"INSERT INTO [TBTESTE] 
                (
                    [TITULO],
                    [NUMERO_DISCIPLINA],
                    [NUMERO_MATERIA],
                    [QUANTIDADE_QUESTOES],
                    [DATA]
	            )
	            VALUES
                (
                    @TITULO,
                    @NUMERO_DISCIPLINA,
                    @NUMERO_MATERIA,
                    @QUANTIDADE_QUESTOES,
                    @DATA
                );SELECT SCOPE_IDENTITY();";

        public ValidationResult Editar(Teste registro)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Excluir(Teste registro)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Inserir(Teste novoRegistro)
        {
            throw new NotImplementedException();
        }

        public Teste SelecionarPorNumero(int numero)
        {
            throw new NotImplementedException();
        }

        public List<Teste> SelecionarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
