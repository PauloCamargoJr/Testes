using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testes.Dominio.Compartilhado;
using Testes.Dominio.ModuloDisciplina;
using Testes.Dominio.ModuloQuestao;

namespace Testes.Dominio.ModuloMateria
{
    public class Materia : EntidadeBase<Materia>
    {

        public string Nome { get; set; }
        public Disciplina Disciplina { get; set; }
        public Questao Questao { get; set; }
        public List<Questao> Questoes { get; set; }
        public SerieEnum Serie { get; set; }

        public override void Atualizar(Materia registro)
        {
            


        }

        public override string ToString()
        {
            return $"Número: {Numero}, Nome: {Nome}, Disciplina: {Disciplina}, Série: {Serie}";
        }
    }

    public enum SerieEnum
    {

        Primeira = 1, Segunda = 2

    }
}
