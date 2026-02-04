using FluentValidation;
using GeradorListaAssados.Engine.Models;

namespace GeradorListaAssados.Engine.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("O Id do produto não pode ser nulo.")
            .NotEmpty().WithMessage("O Id do produto não pode ser vazio.");

        RuleFor(x => x.Name)
            .NotNull().WithMessage("O Nome do produto não pode ser nulo.")
            .NotEmpty().WithMessage("O Nome do produto não pode ser vazio.")
            .MaximumLength(200);

        RuleFor(x => x.Price)
            .NotNull().WithMessage("O Preço do produto não pode ser nulo.")
            .NotEmpty().WithMessage("O Preço do produto não pode ser vazio.")
            .GreaterThan(0);

        RuleFor(x => x.Quantity)
            .NotNull().WithMessage("A Quantidade do produto não pode ser nulo.")
            .NotEmpty().WithMessage("A Quantidade do produto não pode ser vazio.")
            .GreaterThan(0);

        RuleFor(x => x.Index)
            .NotNull().WithMessage("O Indice do produto não pode ser nulo.")
            .NotEmpty().WithMessage("O Indice do produto não pode ser vazio.")
            .GreaterThan(0);

        RuleFor(x => x.HexCodeColor)
            .NotNull().WithMessage("A Cor de fundo produto não pode ser nulo.")
            .NotEmpty().WithMessage("A Cor de fundo do produto não pode ser vazio.")
            .Must((hexColor) =>
            {
                return hexColor.StartsWith('#') 
                    && (hexColor.Length == 7);
            }).WithMessage("A Cor de fundo deve estar no formato hexadecimal.");
    }
}
