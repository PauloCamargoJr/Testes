using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testes.Dominio.ModuloQuestao;
using Testes.WinApp.Compartilhado;

namespace Testes.WinApp.ModuloQuestao
{
    public partial class ListagemQuestoesControl : UserControl
    {
        public ListagemQuestoesControl()
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

        public int ObtemNumeroQuestoesSelecionado()
        {
            return grid.SelecionarNumero<int>();
        }

        public void AtualizarRegistros(List<Questao> Questoes)
        {
            grid.Rows.Clear();

            foreach (Questao q in Questoes)
            {
                grid.Rows.Add(q.Numero, q.Enunciado, q.Materia.Nome);
            }
        }

    }
}
