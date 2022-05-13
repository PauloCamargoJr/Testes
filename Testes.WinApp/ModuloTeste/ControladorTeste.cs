using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testes.Dominio.ModuloTeste;
using Testes.Infra;
using Testes.WinApp.Compartilhado;

namespace Testes.WinApp.ModuloTeste
{
    public class ControladorTeste : ControladorBase
    {

        private readonly IRepositorioTeste repositorioTeste;
        private ListagemTestesControl listagemTestes;
        DataContext dataContext;

        public ControladorTeste(IRepositorioTeste repositorio, DataContext dataContext)
        {

            repositorioTeste = repositorio;
            this.dataContext = dataContext;

        }

        public override void Editar()
        {

            Teste TesteSelecionada = ObtemTesteSelecionada();

            if (TesteSelecionada == null)
            {
                MessageBox.Show("Selecione uma despesa primeiro",
                    "Edição de Despesas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroTesteForm tela = new TelaCadastroTesteForm(dataContext);

            tela.Teste = TesteSelecionada;

            tela.GravarRegistro = repositorioTeste.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTestes();
            }

        }

        public override void Excluir()
        {

            Teste TesteSelecionada = ObtemTesteSelecionada();

            if (TesteSelecionada == null)
            {
                MessageBox.Show("Selecione uma diciplina primeiro",
                "Exclusão de Teste", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a Teste?",
                "Exclusão de Teste", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioTeste.Excluir(TesteSelecionada);
                CarregarTestes();
            }

        }

        public override void Inserir()
        {
            TelaCadastroTesteForm tela = new TelaCadastroTesteForm(dataContext);

            tela.Teste = new Teste();

            tela.GravarRegistro = repositorioTeste.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTestes();
            }
        }

        private void CarregarTestes()
        {
            List<Teste> Teste = repositorioTeste.SelecionarTodos();

            listagemTestes.AtualizarRegistros(Teste);
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            throw new NotImplementedException();
        }

        public override UserControl ObtemListagem()
        {
            if (listagemTestes == null)
                listagemTestes = new ListagemTestesControl();

            CarregarTestes();

            return listagemTestes;
        }

        private Teste ObtemTesteSelecionada()
        {
            int numeroSelecionado = listagemTestes.ObtemNumeroTestesSelecionado();

            Teste TesteSelecionada = repositorioTeste.SelecionarPorNumero(numeroSelecionado);

            return TesteSelecionada;
        }
    }
}
