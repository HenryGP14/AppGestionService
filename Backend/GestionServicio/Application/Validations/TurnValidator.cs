using Domain.Entities;
using FluentValidation;

namespace Application.Validations
{
    public class TurnValidator : AbstractValidator<TurnAttention>
    {
        private readonly GenericValidator _validations = new GenericValidator();

        public TurnValidator()
        {
            RuleFor(turn => turn.Description)
                .NotEmpty().WithMessage("La descripción del turno es obligatoria.")
                .Must(_validations.ValidateTurnDescription!).WithMessage("La descripción del turno debe tener 2 letras mayúsculas seguidas de 4 números.");
        }
    }
}
