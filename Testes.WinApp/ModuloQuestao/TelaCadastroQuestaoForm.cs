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
        public TelaCadastroQuestaoForm(DataContext dataContext)
        {
            InitializeComponent();
            materias = dataContext.Materias;
            foreach (var item in materias)
            {

                comboBoxMateria.Items.Add(item.Nome);

            }
            ;

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

                //TelaPrincipalForm.Instancia.AtualizarRodape(primeiroErro);


                DialogResult = DialogResult.None;
            }

        }
    }
}
