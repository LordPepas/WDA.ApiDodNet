using FluentValidation;
using WDA.ApiDotNet.Business.Models.DTOs.PublishersDTO;

namespace WDA.ApiDotNet.Business.Models.DTOs.Validations.CreationValidations
{
    public class PublisherCreationValidator : AbstractValidator<PublishersCreateDTO>
    {
        public PublisherCreationValidator()
        {
            RuleFor(x => x.Name)
                   .NotEmpty().WithMessage("Name deve ser informado.")
                   .Length(3, 50).WithMessage("Name: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.City)
                   .NotEmpty().WithMessage("City deve ser informado.")
                   .Length(3, 50).WithMessage("City: Necessário entre 3 e 50 caracteres.");
        }
    }
}
