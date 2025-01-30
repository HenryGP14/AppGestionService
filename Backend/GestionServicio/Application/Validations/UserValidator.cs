using Application.Dtos.Request;
using FluentValidation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Application.Validations
{
    public class UserValidator : AbstractValidator<UserRequest>
    {
        private readonly GenericValidator _validations = new GenericValidator();

        public UserValidator()
        {
            RuleFor(user => user.Username)
            .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
            .Must(_validations.ValidateUsername)
            .WithMessage("El nombre de usuario debe tener entre 8 y 20 caracteres, incluir letras y al menos un número.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .Must(_validations.ValidatePassword)
                .WithMessage("La contraseña debe tener entre 8 y 30 caracteres, incluir al menos una letra mayúscula y un número.");

            RuleFor(user => user.Rolid)
                .NotEmpty().Must(num => num != 0).WithMessage("Debe asignar un rol al usuario.");
        }
    }
}
