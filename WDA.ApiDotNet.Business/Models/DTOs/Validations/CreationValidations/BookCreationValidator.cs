﻿using FluentValidation;
using WDA.ApiDotNet.Business.Models.DTOs.BooksDTO;

namespace WDA.ApiDotNet.Business.Models.DTOs.Validations.CreationValidations
{
    public class BookCreationValidator : AbstractValidator<BooksCreateDTO>
    {
        public BookCreationValidator()
        {
            When(x => x != null, () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Nome deve ser informado.")
                    .Length(3, 50).WithMessage("Nome: Necessário entre 3 e 50 caracteres.");

                RuleFor(x => x.PublisherId)
                    .NotEmpty().WithMessage("Editora deve ser informado.")
                    .GreaterThanOrEqualTo(1).WithMessage("Editora deve ser informado.");

                RuleFor(x => x.Author)
                    .NotEmpty().WithMessage("Autor deve ser informado.")
                    .Length(3, 50).WithMessage("Autor: Necessário entre 3 e 50 caracteres.");

                RuleFor(x => x.Release)
                     .NotEmpty().WithMessage("Lançamento deve ser informado.")
                    .GreaterThanOrEqualTo(1).WithMessage("Lançamento deve ser informado.");

                RuleFor(x => x.Quantity)
                    .NotEmpty().WithMessage("Quantidade deve ser informada.")
                    .GreaterThanOrEqualTo(1).WithMessage("Quantidade deve ser informada.");
            });
        }
    }
}
