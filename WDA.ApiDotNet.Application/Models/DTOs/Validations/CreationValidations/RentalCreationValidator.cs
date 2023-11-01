using FluentValidation;
using WDA.ApiDotNet.Application.Models.DTOs.RentalsDTO;

namespace WDA.ApiDotNet.Business.Models.DTOs.Validations.CreationValidations
{
    public class RentalCreationValidator : AbstractValidator<RentalsCreateDTO>
    {
        public RentalCreationValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty().WithMessage("Livro deve ser informado.")
                .GreaterThanOrEqualTo(1).WithMessage("Livro deve ser informado.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Usuário deve ser informado.")
                .GreaterThanOrEqualTo(1).WithMessage("Usuário deve ser informado.");

            //RuleFor(x => x.RentalDate)
            //    .NotEmpty().WithMessage("Data de aluguel deve ser informado.");

            RuleFor(x => x.PrevisionDate)
                .NotEmpty().WithMessage("Data de previsão deve ser informado.");

        }
    }
}
