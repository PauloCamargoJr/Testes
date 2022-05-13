using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testes.Infra;
using Testes.Infra.ModuloDisciplina;
using Testes.Infra.ModuloMateria;
using Testes.Infra.ModuloQuestao;
using Testes.Infra.ModuloTeste;
using Testes.WinApp.Compartilhado;
using Testes.WinApp.ModuloDisciplina;
using Testes.WinApp.ModuloMateria;
using Testes.WinApp.ModuloQuestao;
using Testes.WinApp.ModuloTeste;

namespace Testes.WinApp
{
    public partial class TelaPrincipalForm : Form
    {

        private ControladorBase controlador;
        private Dictionary<string, ControladorBase> controladores;
        private DataContext contextoDados;

        public TelaPrincipalForm(DataContext contextoDados)
        {
            InitializeComponent();

            Instancia = this;

            this.contextoDados = contextoDados;

            InicializarControladores();
        }

        public static TelaPrincipalForm Instancia
        {
            get;
            private set;
        }

        private void disciplinasMenuItem_Click(object sender, EventArgs e)
        {

            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);

        }

        private void matériasMenuItem_Click(object sender, EventArgs e)
        {

            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);

        }

        private void questõesMenuItem_Click(object sender, EventArgs e)
        {

            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);

        }

        private void testesMenuItem_Click(object sender, EventArgs e)
        {

            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);

        }

        private void btnInserir_Click(object sender, EventArgs e)
        {

            controlador.Inserir();

        }

        private void ConfigurarTelaPrincipal(ToolStripMenuItem opcaoSelecionada)
        {
            var tipo = opcaoSelecionada.Text;

            controlador = controladores[tipo];

            //ConfigurarToolbox();

            ConfigurarListagem();
        }

        private void ConfigurarListagem()
        {
            //AtualizarRodape("Disciplinas");

            var listagemControl = controlador.ObtemListagem();

            panelRegistros.Controls.Clear();

            listagemControl.Dock = DockStyle.Fill;

            panelRegistros.Controls.Add(listagemControl);
        }

        private void CarregarListagem()
        {
            var listagemControl = controlador.ObtemListagem();

            panelRegistros.Controls.Clear();

            listagemControl.Dock = DockStyle.Fill;

            panelRegistros.Controls.Add(listagemControl);
        }

        private void SelecionarControlador(ToolStripMenuItem opcaoSelecionada)
        {
            var tipo = opcaoSelecionada.Text;

            controlador = controladores[tipo];
        }

        public void InicializarControladores()
        {

            var repositorioDisciplina = new RepositorioDisciplina(contextoDados);
            var repositorioMateria = new RepositorioMateria(contextoDados);
            var repositorioQuestao = new RepositorioQuestao(contextoDados);
            var repositorioTeste = new RepositorioTeste(contextoDados);

            controladores = new Dictionary<string, ControladorBase>();

            controladores.Add("Disciplinas", new ControladorDisciplina(repositorioDisciplina));
            controladores.Add("Matérias", new ControladorMateria(repositorioMateria, contextoDados));
            controladores.Add("Questões", new ControladorQuestao(repositorioQuestao, contextoDados));
            controladores.Add("Testes", new ControladorTeste(repositorioTeste, contextoDados));


        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            controlador.Editar();

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

            controlador.Excluir();

        }
    }
}
