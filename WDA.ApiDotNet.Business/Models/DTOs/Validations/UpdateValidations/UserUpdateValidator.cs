﻿using FluentValidation;
using WDA.ApiDotNet.Business.Models.DTOs.UsersDTO;

namespace WDA.ApiDotNet.Business.Models.DTOs.Validations.UpdateValidations
{
    public class UserUpdateValidator : AbstractValidator<UsersUpdateDTO>
    {
        public UserUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id deve ser informado!");

            RuleFor(x => x.Name)
                   .NotEmpty().WithMessage("Nome deve ser informado.")
                   .Length(3, 50).WithMessage("Nome: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.City)
                   .NotEmpty().WithMessage("Cidade deve ser informado.")
                   .Length(3, 50).WithMessage("Cidade: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.Address)
                   .NotEmpty().WithMessage("Endereço deve ser informado.")
                   .Length(3, 50).WithMessage("Endreço: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.Email)
                   .NotEmpty().WithMessage("Email deve ser informado.")
                   .EmailAddress().WithMessage("Email inválido.");
        }
    }
}

