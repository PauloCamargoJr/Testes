using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testes.Dominio.Compartilhado;

namespace Testes.Dominio.ModuloDisciplina
{
    public class Disciplina : EntidadeBase<Disciplina>
    {

        public string Nome { get; set; }

        public override void Atualizar(Disciplina registro)
        {
            


        }

        public override string ToString()
        {
            return $"Número: {Numero}, Nome: {Nome}";
        }

    }
}
