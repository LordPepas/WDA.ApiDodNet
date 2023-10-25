using FluentValidation;
using WDA.ApiDotNet.Application.Models.DTOs.RentalsDTO;

namespace WDA.ApiDotNet.Application.Models.DTOs.Validations
{
    public class RentalUpdateValidator : AbstractValidator<RentalsUpdateDTO>
    {
        public RentalUpdateValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id deve ser ser informado.");

            RuleFor(x => x.ReturnDate)
                .NotEmpty().WithMessage("Data de devolução deve ser informado!");
        }
    }
}
