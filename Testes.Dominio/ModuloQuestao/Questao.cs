using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testes.Dominio.Compartilhado;
using Testes.Dominio.ModuloMateria;

namespace Testes.Dominio.ModuloQuestao
{
    public class Questao : EntidadeBase<Questao>
    {

        public string Enunciado { get; set; }
        public Materia Materia { get; set; }
        public List<AlternativaQuestao> Alternativas { get; set; }
        public override void Atualizar(Questao registro)
        {
            


        }

        public Questao()
        {

            Alternativas = new List<AlternativaQuestao>();

        }

    }
}
