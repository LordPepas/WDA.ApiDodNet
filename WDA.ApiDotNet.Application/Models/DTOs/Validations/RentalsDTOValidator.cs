using FluentValidation;
using WDA.ApiDotNet.Application.Models.DTOs.RentalsDTO;

namespace WDA.ApiDotNet.Application.Models.DTOs.Validations
{
    public class RentalsCreateDTOValidator : AbstractValidator<RentalsCreateDTO>
    {
        public RentalsCreateDTOValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Livro deve ser informado!")
                .GreaterThanOrEqualTo(1).WithMessage("Livro deve ser informado!");
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Usuário deve ser informado!")
                .GreaterThanOrEqualTo(1).WithMessage("Usuário deve ser informado!");
            RuleFor(x => x.RentalDate)
                .NotEmpty()
                .NotNull()
                .WithMessage("Data de aluguel deve ser informado!");
            RuleFor(x => x.PrevisionDate)
                .NotEmpty()
                .NotNull()
                .WithMessage("Data de previsão deve ser informado!");

        }
    }
    public class RentalsUpdateDTOValidator : AbstractValidator<RentalsUpdateDTO>
    {
        public RentalsUpdateDTOValidator()
        {
            RuleFor(x => x.ReturnDate)
                .NotEmpty()
                .NotNull()
                .WithMessage("Data de devolução deve ser informado!");
        }
    }
}
