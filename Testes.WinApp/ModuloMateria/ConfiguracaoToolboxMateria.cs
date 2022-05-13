using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testes.WinApp.Compartilhado;

namespace Testes.WinApp.ModuloMateria
{
    public class ConfiguracaoToolboxMateria : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Controle de Materia";

        public override string TooltipInserir => "Inserir uma nova Materia";

        public override string TooltipEditar => "Editar uma Materia existente";

        public override string TooltipExcluir => "Excluir uma Materia existente";
    }
}
