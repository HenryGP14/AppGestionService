using Application.Dtos.Request;
using FluentValidation;

namespace Application.Validations
{
    public class ClientValidator : AbstractValidator<ClientRequest>
    {
        public readonly GenericValidator _validations = new GenericValidator();

        public ClientValidator()
        {
            RuleFor(client => client.Name)
                .NotEmpty().NotNull().WithMessage("El nombre del cliente es obligatorio.");

            RuleFor(client => client.Lastname)
                .NotEmpty().NotNull().WithMessage("El apellido del cliente es obligatorio.");

            RuleFor(client => client.Lastname)
                .NotEmpty().NotNull().WithMessage("El correo del cliente es obligatorio.")
                .EmailAddress().WithMessage("El correo del cliente debe tener un formato correcto");

            RuleFor(client => client.Identification)
                .NotEmpty().WithMessage("La identificación del cliente es obligatorio.")
                .Must(_validations.ValidateIdentification!)
                .WithMessage("La identificación debe tener entre 10 y 13 dígitos y contener solo números.");

            RuleFor(client => client.Address)
                .NotEmpty().WithMessage("La dirección del cliente es obligatoria.")
                .Must(_validations.ValidateAddress!)
                .WithMessage("La dirección debe tener entre 20 y 100 caracteres.");

            RuleFor(client => client.Referenceaddress)
                .NotEmpty().WithMessage("La referencia de la dirección del cliente es obligatoria.")
                .Must(_validations.ValidateAddressReference!)
                .WithMessage("La referencia de la dirección debe tener entre 20 y 100 caracteres.");

            RuleFor(client => client.Phonenumber)
                .NotEmpty().WithMessage("El número de teléfono del cliente es obligatorio.")
                .Must(_validations.ValidatePhoneNumber!)
                .WithMessage("El número de teléfono debe tener 10 dígitos, empezar con '09' y contener solo números.");

        }
    }
}
