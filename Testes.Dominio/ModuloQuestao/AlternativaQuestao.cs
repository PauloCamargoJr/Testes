using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testes.Dominio.Compartilhado;

namespace Testes.Dominio.ModuloQuestao
{
    public class AlternativaQuestao : EntidadeBase<AlternativaQuestao>
    {

        public string Enunciado { get; set; }
        public bool AlternativaCorreta { get; set; }

        public override void Atualizar(AlternativaQuestao registro)
        {
            throw new NotImplementedException();
        }
    }
}
