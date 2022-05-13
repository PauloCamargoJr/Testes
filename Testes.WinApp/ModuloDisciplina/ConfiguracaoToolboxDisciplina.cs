using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testes.WinApp.Compartilhado;

namespace Testes.WinApp.ModuloDisciplina
{
    public class ConfiguracaoToolboxDisciplina : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Controle de Disciplinas";

        public override string TooltipInserir => "Inserir uma nova Disciplina";

        public override string TooltipEditar => "Editar uma Disciplina existente";

        public override string TooltipExcluir => "Excluir uma Disciplina existente";
    }
}
