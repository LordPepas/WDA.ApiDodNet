using FluentValidation;
using WDA.ApiDotNet.Application.Models.DTOs.BooksDTO;

namespace WDA.ApiDotNet.Application.Models.DTOs.Validations
{
        public class BooksCreateDTOValidator : AbstractValidator<BooksCreateDTO>
        {
            public BooksCreateDTOValidator()
            {
                When(x => x != null, () =>
                {
                    RuleFor(x => x.Name)
                        .NotEmpty().WithMessage("Name não informado.")
                        .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.");

                    RuleFor(x => x.Author)
                        .NotEmpty().WithMessage("Author não informado.")
                        .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.");

                    RuleFor(x => x.PublisherId)
                        .NotEmpty().WithMessage("PublisherId não informado.")
                        .GreaterThanOrEqualTo(1).WithMessage("Editora não informado.");

                    RuleFor(x => x.Quantity)
                        .NotEmpty().WithMessage("Quantidade não informada.")
                        .GreaterThanOrEqualTo(1).WithMessage("Quantidade não informada.");

                    RuleFor(x => x.Launch)
                         .NotEmpty().WithMessage("PublisherId não informado.")
                        .GreaterThanOrEqualTo(1).WithMessage("Editora não informado.");
                });
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
