using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testes.Dominio.ModuloDisciplina;
using Testes.WinApp.Compartilhado;

namespace Testes.WinApp.ModuloDisciplina
{
    public partial class ListagemDisciplinasControl : UserControl
    {

        public ListagemDisciplinasControl()
        {
            InitializeComponent();
            grid.ConfigurarGridSomenteLeitura();
            grid.ConfigurarGridZebrado();
            grid.Columns.AddRange(ObterColunas());
        }

        private DataGridViewColumn[] ObterColunas()
        {
            var colunas = new DataGridViewColumn[]
            {
               
            };

            return colunas;
        }

        public int ObtemNumeroDisciplinaSelecionado()
        {
            return grid.SelecionarNumero<int>();
        }

        public void AtualizarRegistros(List<Disciplina> disciplinas)
        {
            grid.Rows.Clear();

            foreach (Disciplina d in disciplinas)
            {
                grid.Rows.Add(d.Numero, d.Nome);
            }
        }
    }
}
