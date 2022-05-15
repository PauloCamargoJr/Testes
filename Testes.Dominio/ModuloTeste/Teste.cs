using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testes.Dominio.Compartilhado;
using Testes.Dominio.ModuloDisciplina;
using Testes.Dominio.ModuloMateria;
using Testes.Dominio.ModuloQuestao;

namespace Testes.Dominio.ModuloTeste
{
    public class Teste : EntidadeBase<Teste>
    {

        public string Titulo { get; set; }
        public Materia Materia { get; set; }
        public Disciplina Disciplina { get; set; }
        public int NumeroQuestoes { get; set; }
        public List<Questao> Questoes { get; set; }

        public DateTime Data { get; set; }

        public override void Atualizar(Teste registro)
        {
            
        }

        public Teste()
        {

            Questoes = new List<Questao>();

        }

        public override string ToString()
        {
            return $"Número: {Numero}, Titulo: {Titulo}, Disciplina: {Disciplina}, Matéria: {Materia}, Data: {Data.ToShortDateString()}";
        }

    }
}
