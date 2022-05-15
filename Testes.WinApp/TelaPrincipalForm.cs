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
            DesativarBotoes();

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

            ConfigurarToolbox();

            ConfigurarListagem();
        }

        private void ConfigurarToolbox()
        {
            ConfiguracaoToolboxBase configuracao = controlador.ObtemConfiguracaoToolbox();

            if (configuracao != null)
            {
                toolbox.Enabled = true;

                labelTipoCadastro.Text = configuracao.TipoCadastro;

                ConfigurarTooltips(configuracao);

                ConfigurarBotoes(configuracao);
            }
        }

        private void ConfigurarBotoes(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.Enabled = configuracao.InserirHabilitado;
            btnEditar.Enabled = configuracao.EditarHabilitado;
            btnExcluir.Enabled = configuracao.ExcluirHabilitado;
            btnDuplicar.Enabled = configuracao.DuplicarHabilitado;
            btnPDF.Enabled = configuracao.PDFHabilitado;
        }

        private void ConfigurarTooltips(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.ToolTipText = configuracao.TooltipInserir;
            btnEditar.ToolTipText = configuracao.TooltipEditar;
            btnExcluir.ToolTipText = configuracao.TooltipExcluir;
            btnDuplicar.ToolTipText = configuracao.TooltipDuplicar;
            btnPDF.ToolTipText = configuracao.TooltipPDF;
        }

        private void ConfigurarListagem()
        {

            var listagemControl = controlador.ObtemListagem();

            panelRegistros.Controls.Clear();

            listagemControl.Dock = DockStyle.Fill;

            panelRegistros.Controls.Add(listagemControl);
        }

        private void DesativarBotoes()
        {
            btnInserir.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnDuplicar.Enabled = false;
            btnPDF.Enabled = false;
        }

        public void InicializarControladores()
        {

            var repositorioDisciplina = new RepositorioDisciplina(contextoDados);
            var repositorioMateria = new RepositorioMateria(contextoDados);
            var repositorioQuestao = new RepositorioQuestao(contextoDados);
            var repositorioTeste = new RepositorioTeste(contextoDados);

            controladores = new Dictionary<string, ControladorBase>();

            controladores.Add("Disciplinas", new ControladorDisciplina(repositorioDisciplina));
            controladores.Add("Matérias", new ControladorMateria(repositorioMateria, repositorioDisciplina));
            controladores.Add("Questões", new ControladorQuestao(repositorioQuestao, repositorioMateria));
            controladores.Add("Testes", new ControladorTeste(repositorioTeste, repositorioMateria, repositorioDisciplina, repositorioQuestao));


        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            controlador.Editar();

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

            controlador.Excluir();

        }

        private void btnDuplicar_Click(object sender, EventArgs e)
        {

            controlador.Duplicar();

        }

        private void btnPDF_Click(object sender, EventArgs e)
        {

            controlador.GerarPDF();

        }
    }
}
