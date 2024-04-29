using FluentValidation;
using ZatcaCore.Contracts;

namespace efatoora.service.Validators;

public class NoteValidator : AbstractValidator<Note>
{
    public NoteValidator()
    {
        RuleFor(x => x.InvoiceNo).NotEmpty().NotNull();
        RuleFor(x => x.Reason).NotEmpty().NotNull();

    }
}
