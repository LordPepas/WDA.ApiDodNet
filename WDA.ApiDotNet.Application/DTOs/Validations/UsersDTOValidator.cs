using FluentValidation;

namespace WDA.ApiDotNet.Application.DTOs.Validations
{
    public class UsersCreateDTOValidator : AbstractValidator<UsersCreateDTO>
    {
        public UsersCreateDTOValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull()
               .WithMessage("Nome deve ser informado!")
               .MinimumLength(3).WithMessage("Mínimo 3 caracteres!")
               .MaximumLength(30).WithMessage("Máximo 30 caracteres!");
            RuleFor(x => x.City)
                .NotEmpty()
                .NotNull()
                .WithMessage("Cidade deve ser informado!")
                .MinimumLength(3).WithMessage("Mínimo 3 caracteres!")
                .MaximumLength(30).WithMessage("Máximo 50 caracteres!");
            RuleFor(x => x.Address)
                .NotEmpty()
                .NotNull()
                .WithMessage("Endereço deve ser informado!")
                .MinimumLength(3).WithMessage("Mínimo 3 caracteres!")
                .MaximumLength(30).WithMessage("Máximo 100 caracteres!");
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email deve ser informado!")
                .EmailAddress().WithMessage("Email inválido!");
        }
    }
    public class UsersDTOValidator : AbstractValidator<UsersUpdateDTO>
    {
        public UsersDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Id deve ser informado!");

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome deve ser informado!")
                .MinimumLength(3).WithMessage("Mínimo 3 caracteres!")
                .MaximumLength(30).WithMessage("Máximo 30 caracteres!");
            RuleFor(x => x.City)
                .NotEmpty()
                .NotNull()
                .WithMessage("Cidade deve ser informado!")
                .MinimumLength(3).WithMessage("Mínimo 3 caracteres!")
                .MaximumLength(30).WithMessage("Máximo 50 caracteres!");
            RuleFor(x => x.Address)
                .NotEmpty()
                .NotNull()
                .WithMessage("Endereço deve ser informado!")
                .MinimumLength(3).WithMessage("Mínimo 3 caracteres!")
                .MaximumLength(30).WithMessage("Máximo 100 caracteres!");
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email deve ser informado!")
                .EmailAddress().WithMessage("Informe um email válido");
        }
    }
}

