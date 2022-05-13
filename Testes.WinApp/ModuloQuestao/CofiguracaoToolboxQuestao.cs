using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testes.WinApp.Compartilhado;

namespace Testes.WinApp.ModuloQuestao
{
    public class ConfiguracaoToolboxQuestao : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Controle de Questão";

        public override string TooltipInserir => "Inserir uma nova Questão";

        public override string TooltipEditar => "Editar uma Questão existente";

        public override string TooltipExcluir => "Excluir uma Questão existente";
    }
}
