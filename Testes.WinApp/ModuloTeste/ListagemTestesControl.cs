using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testes.Dominio.ModuloTeste;
using Testes.WinApp.Compartilhado;

namespace Testes.WinApp.ModuloTeste
{
    public partial class ListagemTestesControl : UserControl
    {
        public ListagemTestesControl()
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

        public int ObtemNumeroTestesSelecionado()
        {
            return grid.SelecionarNumero<int>();
        }

        public void AtualizarRegistros(List<Teste> testes)
        {
            grid.Rows.Clear();

            foreach (Teste t in testes)
            {
                grid.Rows.Add(t.Numero, t.Titulo, t.Disciplina.Nome, t.Materia.Nome, t.Data.ToShortDateString());
            }
        }

    }
}
