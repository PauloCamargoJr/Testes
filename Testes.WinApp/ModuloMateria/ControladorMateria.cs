using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testes.Dominio.ModuloMateria;
using Testes.Infra;
using Testes.WinApp.Compartilhado;

namespace Testes.WinApp.ModuloMateria
{
    public class ControladorMateria : ControladorBase
    {

        private readonly IRepositorioMateria repositorioMateria;
        private ListagemMateriasControl listagemMaterias;
        DataContext dataContext;

        public ControladorMateria(IRepositorioMateria repositorio, DataContext dataContext)
        {

            repositorioMateria = repositorio;
            this.dataContext = dataContext;

        }

        public override void Editar()
        {

            Materia MateriaSelecionada = ObtemMateriaSelecionada();

            if (MateriaSelecionada == null)
            {
                MessageBox.Show("Selecione uma despesa primeiro",
                    "Edição de Despesas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroMateriaForm tela = new TelaCadastroMateriaForm(dataContext);

            tela.Materia = MateriaSelecionada;

            tela.GravarRegistro = repositorioMateria.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarMaterias();
            }

        }

        public override void Excluir()
        {

            Materia MateriaSelecionada = ObtemMateriaSelecionada();

            if (MateriaSelecionada == null)
            {
                MessageBox.Show("Selecione uma diciplina primeiro",
                "Exclusão de Materia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a Materia?",
                "Exclusão de Materia", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioMateria.Excluir(MateriaSelecionada);
                CarregarMaterias();
            }

        }

        public override void Inserir()
        {
            TelaCadastroMateriaForm tela = new TelaCadastroMateriaForm(dataContext);

            tela.Materia = new Materia();

            tela.GravarRegistro = repositorioMateria.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarMaterias();
            }
        }

        private void CarregarMaterias()
        {
            List<Materia> Materia = repositorioMateria.SelecionarTodos();

            listagemMaterias.AtualizarRegistros(Materia);
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            throw new NotImplementedException();
        }

        public override UserControl ObtemListagem()
        {
            if (listagemMaterias == null)
                listagemMaterias = new ListagemMateriasControl();

            CarregarMaterias();

            return listagemMaterias;
        }

        private Materia ObtemMateriaSelecionada()
        {
            int numeroSelecionado = listagemMaterias.ObtemNumeroMateriaSelecionado();

            Materia MateriaSelecionada = repositorioMateria.SelecionarPorNumero(numeroSelecionado);

            return MateriaSelecionada;
        }
    }
}
