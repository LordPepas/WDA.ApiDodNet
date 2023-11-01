using FluentValidation;
using WDA.ApiDotNet.Application.Models.DTOs.PublishersDTO;

namespace WDA.ApiDotNet.Business.Models.DTOs.Validations.UpdateValidations
{
    public class PublisherUpdateValidator : AbstractValidator<PublishersUpdateDTO>
    {
        public PublisherUpdateValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id deve ser ser informado.");

            RuleFor(x => x.Name)
                   .NotEmpty().WithMessage("Name deve ser informado.")
                   .Length(3, 50).WithMessage("Name: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.City)
                   .NotEmpty().WithMessage("City deve ser informado.")
                   .Length(3, 50).WithMessage("City: Necessário entre 3 e 50 caracteres.");
        }
    }
}
