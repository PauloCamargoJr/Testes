using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testes.WinApp.Compartilhado;

namespace Testes.WinApp.ModuloTeste
{
    public class ConfiguracaoToolboxTeste : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Controle de Teste";

        public override string TooltipInserir => "Inserir um nova Teste";

        public override string TooltipEditar => "Editar um Teste existente";

        public override string TooltipExcluir => "Excluir um Teste existente";

        public override string TooltipDuplicar => "Duplicar um Teste existente";

        public override string TooltipPDF => "Gerar PDF de um Teste existente";

        public override bool EditarHabilitado => false;

        public override bool DuplicarHabilitado => true;

        public override bool PDFHabilitado => true;
    }
}
