using DTO;
using FluentValidation;

namespace Services.Validator;

public class SegmentoValidator : AbstractValidator<SegmentoDTO>
{
    public SegmentoValidator()
    {
        RuleFor(c=>c.Id).NotEmpty();
        RuleFor(c=>c.Nome).NotEmpty();
        RuleFor(c=>c.Descricao).MaximumLength(50);
    }
}
