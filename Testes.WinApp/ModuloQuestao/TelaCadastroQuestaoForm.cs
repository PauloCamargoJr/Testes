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
using Testes.Dominio.ModuloMateria;
using Testes.Dominio.ModuloQuestao;
using Testes.Infra;

namespace Testes.WinApp.ModuloQuestao
{
    public partial class TelaCadastroQuestaoForm : Form
    {
        List<Materia> materias;
        AlternativaQuestao alternativa;

        public TelaCadastroQuestaoForm(IRepositorioMateria repositorioMateria)
        {
            InitializeComponent();

            materias = repositorioMateria.SelecionarTodos();
            alternativa = new AlternativaQuestao();
            foreach (var item in materias)
            {

                comboBoxMateria.Items.Add(item.Nome);

            }
        }

        private Questao questao;

        public Questao Questao
        {
            get
            {
                return questao;
            }
            set
            {
                questao = value;

                txtEnunciado.Text = questao.Enunciado;
                comboBoxMateria.SelectedItem = questao.Materia;
                //txtAlternnativas.Text = alternativa.Enunciado;
                //checkBoxCorreta.Checked = alternativa.AlternativaCorreta;

            }
        }

        public Func<Questao, ValidationResult> GravarRegistro { get; set; }

        private void btnInserir_Click(object sender, EventArgs e)
        {


        }

        private void btnInserir_Click_1(object sender, EventArgs e)
        {

            Questao.Enunciado = txtEnunciado.Text;
            foreach (var item in materias)
            {

                if (item.Nome.Equals(comboBoxMateria.SelectedItem))
                    questao.Materia = item;

            }

            ValidationResult resultadoValidacao = GravarRegistro(Questao);

            if (resultadoValidacao.IsValid == false)
            {
                string primeiroErro = resultadoValidacao.Errors[0].ErrorMessage;

                DialogResult = DialogResult.None;
            }
        }

        private void btnAdicionarAlternativa_Click(object sender, EventArgs e)
        {

            AlternativaQuestao alternativa = new AlternativaQuestao();
            

            alternativa.Enunciado = txtAlternnativas.Text;
            alternativa.AlternativaCorreta = checkBoxCorreta.Checked;
            
            if(questao.Alternativas != null)
            {

                questao.Alternativas.Add(alternativa);
                txtAlternnativas.Clear();

            }
        }
    }
}
