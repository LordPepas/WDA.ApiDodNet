using FluentValidation;
using WDA.ApiDotNet.Application.DTOs.PublishersDTO;

namespace WDA.ApiDotNet.Application.DTOs.Validations
{
    public class PublishersCreateDTOValidator : AbstractValidator<PublishersCreateDTO>
    {
        public PublishersCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome deve ser informado!")
                .MinimumLength(3).WithMessage("Mínimo 3 caracteres!")
                .MaximumLength(50).WithMessage("Máximo 50 caracteres!");
            RuleFor(x => x.City)
                .NotEmpty()
                .NotNull()
                .WithMessage("Cidade deve ser informado!")
                .MinimumLength(3).WithMessage("Mínimo 3 caracteres!")
                .MaximumLength(50).WithMessage("Máximo 50 caracteres!");
        }
    }
    public class PublishersDTOValidator : AbstractValidator<PublishersUpdateDTO>
    {
        public PublishersDTOValidator()
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
                .MaximumLength(50).WithMessage("Máximo 50 caracteres!");
            RuleFor(x => x.City)
                .NotEmpty()
                .NotNull()
                .WithMessage("Cidade deve ser informado!")
                .MinimumLength(3).WithMessage("Mínimo 3 caracteres!")
                .MaximumLength(50).WithMessage("Máximo 50 caracteres!");
        }
    }
}
