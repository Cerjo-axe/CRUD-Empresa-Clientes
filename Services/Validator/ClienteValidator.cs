using DTO;
using FluentValidation;

namespace Services.Validator;

public class ClienteValidator: AbstractValidator<ClienteDTO>
{
    public ClienteValidator()
    {
        RuleFor(c=>c.Id).NotEmpty();
        RuleFor(c=>c.Nome).NotEmpty();
        RuleFor(c=>c.Site).NotEmpty();
        RuleFor(c=>c.SegmentoId).NotEmpty();
    }
}
