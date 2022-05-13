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
using Testes.WinApp.ModuloQuestao;

namespace Testes.WinApp.ModuloDisciplina
{
    public partial class TelaCadastroDisciplinaForm : Form
    {
        public TelaCadastroDisciplinaForm()
        {
            InitializeComponent();
        }

        private Disciplina disciplina;

        public Disciplina Disciplina
        {
            get
            {
                return disciplina;
            }
            set
            {
                disciplina = value;

                txtNome.Text = disciplina.Nome;
            }
        }

        public Func<Disciplina, ValidationResult> GravarRegistro { get; set; }

        private void btnInserir_Click_1(object sender, EventArgs e)
        {

            disciplina.Nome = txtNome.Text;

            ValidationResult resultadoValidacao = GravarRegistro(disciplina);

            if (resultadoValidacao.IsValid == false)
            {
                string primeiroErro = resultadoValidacao.Errors[0].ErrorMessage;

                //TelaPrincipalForm.Instancia.AtualizarRodape(primeiroErro);

                DialogResult = DialogResult.None;
            }

        }
    }
}
