﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes.Dominio.ModuloQuestao
{
    public class ValidadorQuestao : AbstractValidator<Questao>
    {
        public ValidadorQuestao()
        {
            RuleFor(x => x.Enunciado)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Materia)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Alternativas)
                .NotNull()
                .NotEmpty();
        }
    }
}
