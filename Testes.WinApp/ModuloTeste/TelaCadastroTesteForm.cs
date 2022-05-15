using FluentValidation.Results;
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
using Testes.Dominio.ModuloMateria;
using Testes.Dominio.ModuloQuestao;
using Testes.Dominio.ModuloTeste;
using Testes.Infra;

namespace Testes.WinApp.ModuloTeste
{
    public partial class TelaCadastroTesteForm : Form
    {
        List<Materia> materias;
        List<Disciplina> disciplinas;
        List<Questao> questoes;
        public TelaCadastroTesteForm(IRepositorioMateria repositorioMateria, IRepositorioDisciplina repositorioDisciplina, IRepositorioQuestao repositorioQuestao)
        {
            InitializeComponent();
            materias = repositorioMateria.SelecionarTodos();
            disciplinas = repositorioDisciplina.SelecionarTodos();
            questoes = repositorioQuestao.SelecionarTodos();
            foreach (var item in materias)
            {

                comboBoxMateria.Items.Add(item.Nome);

            }
            foreach (var item in disciplinas)
            {

                comboBoxDisciplina.Items.Add(item.Nome);

            }
            ;
        }

        private Teste teste;

        public Teste Teste
        {
            get
            {
                return teste;
            }
            set
            {
                teste = value;

                txtTitulo.Text = teste.Titulo;
                comboBoxMateria.SelectedItem = teste.Materia;
                comboBoxDisciplina.SelectedItem = teste.Disciplina;
                txtQtdQuestoes.Text = teste.NumeroQuestoes.ToString();
            }
        }

        public Func<Teste, ValidationResult> GravarRegistro { get; set; }

        private void btnInserir_Click(object sender, EventArgs e)
        {

            Teste.Titulo = txtTitulo.Text;
            foreach (var item in materias)
            {

                if (item.Nome.Equals(comboBoxMateria.SelectedItem))
                    Teste.Materia = item;

            }
            foreach (var item in disciplinas)
            {

                if (item.Nome.Equals(comboBoxDisciplina.SelectedItem))
                    Teste.Disciplina = item;

            }

            Teste.NumeroQuestoes = Convert.ToInt32(txtQtdQuestoes.Text);

            Teste.Data = DateTime.Now;

            Teste.Questoes = ObterQuestoes();


            ValidationResult resultadoValidacao = GravarRegistro(Teste);

            if (resultadoValidacao.IsValid == false)
            {
                string primeiroErro = resultadoValidacao.Errors[0].ErrorMessage;

                DialogResult = DialogResult.None;
            }
        }

        private List<Questao> ObterQuestoes()
        {
            var rnd = new Random();
            var randomized = questoes.OrderBy(item => rnd.Next());
            List<Questao> questoesTeste = new List<Questao>();
            int contador = 0;

            foreach (var item in randomized)
            {

                if (item.Materia == Teste.Materia)
                {

                    questoesTeste.Add(item);
                    contador++;

                }

                if (contador == Teste.NumeroQuestoes)
                    break;

            }

            return questoesTeste;
        }
    }
}
