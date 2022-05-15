using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes.WinApp.Compartilhado
{
    public abstract class ConfiguracaoToolboxBase
    {

        public abstract string TipoCadastro { get; }

        public abstract string TooltipInserir { get; }

        public abstract string TooltipEditar { get; }

        public abstract string TooltipExcluir { get; }

        public virtual string TooltipDuplicar { get; }

        public virtual string TooltipPDF { get; }


        public virtual bool InserirHabilitado { get { return true; } }

        public virtual bool EditarHabilitado { get { return true; } }

        public virtual bool ExcluirHabilitado { get { return true; } }

        public virtual bool DuplicarHabilitado { get { return false; } }

        public virtual bool PDFHabilitado { get { return false; } }

    }
}
