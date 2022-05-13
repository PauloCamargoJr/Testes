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
using Testes.Infra;
using Testes.WinApp.ModuloDisciplina;
using Testes.WinApp.ModuloQuestao;

namespace Testes.WinApp.ModuloMateria
{
    public partial class TelaCadastroMateriaForm : Form
    {

        List<Disciplina> disciplinas;
        public TelaCadastroMateriaForm(DataContext dataContext)
        {
            InitializeComponent();
            disciplinas = dataContext.Disciplinas;
            foreach (var item in disciplinas)
            {

                comboBoxDisicplina.Items.Add(item.Nome);

            }
            ;

        }

        private Materia materia;

        public Materia Materia
        {
            get
            {
                return materia;
            }
            set
            {
                materia = value;

                txtNome.Text = materia.Nome;
                comboBoxDisicplina.SelectedItem = materia.Disciplina;
                comboBoxSerie.SelectedItem = materia.Serie;
            }
        }

        public Func<Materia, ValidationResult> GravarRegistro { get; set; }

        private void btnInserir_Click_1(object sender, EventArgs e)
        {

            

        }

        private void btnInserir_Click(object sender, EventArgs e)
        {

            Materia.Nome = txtNome.Text;
            foreach (var item in disciplinas)
            {

                if (item.Nome.Equals(comboBoxDisicplina.SelectedItem))
                    materia.Disciplina = item;

            }
            Materia.Serie = (SerieEnum)comboBoxSerie.SelectedIndex;

            ValidationResult resultadoValidacao = GravarRegistro(Materia);

            if (resultadoValidacao.IsValid == false)
            {
                string primeiroErro = resultadoValidacao.Errors[0].ErrorMessage;

                //TelaPrincipalForm.Instancia.AtualizarRodape(primeiroErro);

                DialogResult = DialogResult.None;
            }

        }
    }
}
