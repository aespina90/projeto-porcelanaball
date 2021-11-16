﻿using FluentValidation;

namespace PB.Domain.Validators
{
    public class FuncionarioModalidadeValidator : AbstractValidator<FuncionarioModalidade>
    {
        public FuncionarioModalidadeValidator()
        {
            RuleSet("insert", () =>
            {
                RuleFor(x => x.modalidade_codigo).NotEmpty().WithMessage("É necessário um código de modalidade.");
                RuleFor(x => x.funcionario_codigo).NotEmpty().WithMessage("É necessário um código de funcionário.");
            });

            RuleSet("update", () =>
            {
                RuleFor(x => x.codigo).NotEmpty().WithMessage("É necessário um código válido.");
                RuleFor(x => x.modalidade_codigo).NotEmpty().WithMessage("É necessário um código de modalidade.");
                RuleFor(x => x.funcionario_codigo).NotEmpty().WithMessage("É necessário um código de funcionário.");
            });
        }
    }
}
