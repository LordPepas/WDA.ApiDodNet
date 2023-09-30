using FluentValidation;
using WDA.ApiDotNet.Application.DTOs.BooksDTO;

namespace WDA.ApiDotNet.Application.DTOs.Validations
{
    public class BooksCreateDTOValidator : AbstractValidator<BooksCreateDTO>
    {
        public BooksCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome deve ser informado!")
                .MinimumLength(3).WithMessage("Mínimo 3 caracteres!")
                .MaximumLength(50).WithMessage("Máximo 50 caracteres!");
            RuleFor(x => x.PublisherId)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(1)
                .WithMessage("Editora deve ser informado!");
            RuleFor(x => x.Author)
                .NotEmpty()
                .NotNull()
                .WithMessage("Autor deve ser informado!")
                .MinimumLength(3).WithMessage("Mínimo 3 caracteres!")
                .MaximumLength(50).WithMessage("Máximo 50 caracteres!");
            RuleFor(x => x.Launch)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lançamento deve ser informado!");
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .NotNull()
                .WithMessage("Quantidade deve ser informado!")
                .GreaterThanOrEqualTo(1).WithMessage("Quantidade minima UM!");
        }
    }
    public class BooksDTOValidator : AbstractValidator<BooksUpdateDTO>
    {
        public BooksDTOValidator()

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
            RuleFor(x => x.PublisherId)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(1)
                .WithMessage("Editora deve ser informado!");
            RuleFor(x => x.Author)
                .NotEmpty()
                .NotNull()
                .WithMessage("Autor deve ser informado!")
                .MinimumLength(3).WithMessage("Mínimo 3 caracteres!")
                .MaximumLength(50).WithMessage("Máximo 50 caracteres!");
            RuleFor(x => x.Launch)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lançamento deve ser informado!");
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .NotNull()
                .WithMessage("Quantidade deve ser informado!")
                .GreaterThanOrEqualTo(1).WithMessage("Quantidade minima UM!");
        }
    }
}
