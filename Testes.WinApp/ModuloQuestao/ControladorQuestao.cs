using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testes.Dominio.ModuloMateria;
using Testes.Dominio.ModuloQuestao;
using Testes.Infra;
using Testes.WinApp.Compartilhado;

namespace Testes.WinApp.ModuloQuestao
{
    public class ControladorQuestao : ControladorBase
    {

        private readonly IRepositorioQuestao repositorioQuestao;
        private ListagemQuestoesControl listagemQuestoes;
        IRepositorioMateria repositorioMateria;

        public ControladorQuestao(IRepositorioQuestao repositorio, IRepositorioMateria _repositorioMateria)
        {

            repositorioQuestao = repositorio;
            repositorioMateria = _repositorioMateria;

        }

        public override void Editar()
        {

            Questao QuestaoSelecionada = ObtemQuestaoSelecionada();

            if (QuestaoSelecionada == null)
            {
                MessageBox.Show("Selecione uma despesa primeiro",
                    "Edição de Despesas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroQuestaoForm tela = new TelaCadastroQuestaoForm(repositorioMateria);

            tela.Questao = QuestaoSelecionada;

            tela.GravarRegistro = repositorioQuestao.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarQuestaos();
            }

        }

        public override void Excluir()
        {

            Questao QuestaoSelecionada = ObtemQuestaoSelecionada();

            if (QuestaoSelecionada == null)
            {
                MessageBox.Show("Selecione uma diciplina primeiro",
                "Exclusão de Questao", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a Questao?",
                "Exclusão de Questao", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioQuestao.Excluir(QuestaoSelecionada);
                CarregarQuestaos();
            }
        }

        public override void Inserir()
        {
            TelaCadastroQuestaoForm tela = new TelaCadastroQuestaoForm(repositorioMateria);

            tela.Questao = new Questao();

            tela.GravarRegistro = repositorioQuestao.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarQuestaos();
            }
        }

        private void CarregarQuestaos()
        {
            List<Questao> Questao = repositorioQuestao.SelecionarTodos();

            listagemQuestoes.AtualizarRegistros(Questao);
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxQuestao();
        }

        public override UserControl ObtemListagem()
        {
            if (listagemQuestoes == null)
                listagemQuestoes = new ListagemQuestoesControl();

            CarregarQuestaos();

            return listagemQuestoes;
        }

        private Questao ObtemQuestaoSelecionada()
        {
            int numeroSelecionado = listagemQuestoes.ObtemNumeroQuestoesSelecionado();

            Questao QuestaoSelecionada = repositorioQuestao.SelecionarPorNumero(numeroSelecionado);

            return QuestaoSelecionada;
        }
    }
}
