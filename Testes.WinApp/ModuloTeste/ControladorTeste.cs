using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testes.Dominio.ModuloDisciplina;
using Testes.Dominio.ModuloMateria;
using Testes.Dominio.ModuloQuestao;
using Testes.Dominio.ModuloTeste;
using Testes.Infra;
using Testes.WinApp.Compartilhado;

namespace Testes.WinApp.ModuloTeste
{
    public class ControladorTeste : ControladorBase
    {

        private readonly IRepositorioTeste repositorioTeste;
        private ListagemTestesControl listagemTestes;
        IRepositorioMateria repositorioMateria;
        IRepositorioDisciplina repositorioDisciplina;
        IRepositorioQuestao repositorioQuestao;

        public ControladorTeste(IRepositorioTeste repositorio, IRepositorioMateria _repositorioMateria, IRepositorioDisciplina _repositorioDisciplina, IRepositorioQuestao _repositorioQuestao)
        {

            repositorioTeste = repositorio;
            repositorioMateria = _repositorioMateria;
            repositorioDisciplina = _repositorioDisciplina;
            repositorioQuestao = _repositorioQuestao;

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

            TelaCadastroTesteForm tela = new TelaCadastroTesteForm(repositorioMateria, repositorioDisciplina, repositorioQuestao);

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
            TelaCadastroTesteForm tela = new TelaCadastroTesteForm(repositorioMateria, repositorioDisciplina, repositorioQuestao);

            tela.Teste = new Teste();

            tela.GravarRegistro = repositorioTeste.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTestes();
            }
        }

        public override void Duplicar()
        {
            Teste TesteSelecionada = ObtemTesteSelecionada();

            if (TesteSelecionada == null)
            {
                MessageBox.Show("Selecione uma despesa primeiro",
                    "Edição de Despesas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroTesteForm tela = new TelaCadastroTesteForm(repositorioMateria, repositorioDisciplina, repositorioQuestao);

            tela.Teste = TesteSelecionada;

            tela.GravarRegistro = repositorioTeste.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTestes();
            }
        }

        public override void GerarPDF()
        {

            Teste TesteSelecionado = ObtemTesteSelecionada();

            string nomeArquivo = @"C:\Windows\Temp" + @"\teste.pdf";
            FileStream arquivoPDF = new FileStream(nomeArquivo, FileMode.Create);
            Document doc = new Document(PageSize.A4);
            PdfWriter escritorPDF = PdfWriter.GetInstance(doc, arquivoPDF);

            string dados = "";

            Paragraph paragrafo = new Paragraph(dados, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 14, (int)System.Drawing.FontStyle.Regular));
            paragrafo.Alignment = Element.ALIGN_CENTER;
            paragrafo.Add(TesteSelecionado.Titulo + "\n\n");
            paragrafo.Add("Disciplina: " + TesteSelecionado.Disciplina.Nome + "\n\n");
            paragrafo.Add("Assunto: " + TesteSelecionado.Materia.Nome + "\n\n");
            paragrafo.Add("Data: " + TesteSelecionado.Data.ToShortDateString() + "\n\n");

            int qtdQuestoes = TesteSelecionado.Questoes.Count;

            Paragraph paragrafo2 = new Paragraph(dados, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 14, (int)System.Drawing.FontStyle.Regular));
            paragrafo2.Alignment = Element.ALIGN_LEFT;

            for (int i = 0; i < qtdQuestoes; i++)
            {

                int j = i + 1;
                paragrafo2.Add("Questão " + j + " ) " + TesteSelecionado.Questoes[i].Enunciado + "\n\n");

                for(int k = 0; k < TesteSelecionado.Questoes[i].Alternativas.Count; k++)
                {

                    paragrafo2.Add("() " + TesteSelecionado.Questoes[i].Alternativas[k].Enunciado + "\n\n");

                }

            }

            doc.Open();
            doc.Add(paragrafo);
            doc.Add(paragrafo2);
            doc.Close();

        }

        private void CarregarTestes()
        {
            List<Teste> Teste = repositorioTeste.SelecionarTodos();

            listagemTestes.AtualizarRegistros(Teste);
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxTeste();
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
