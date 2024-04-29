using FluentValidation;
using ZatcaCore.Contracts;

namespace efatoora.service.Validators;

public class LineItemValidator : AbstractValidator<LineItem>
{
    public LineItemValidator()
    {

        RuleFor(x => x.Description).NotNull().NotEmpty();
        RuleFor(x => x.LinePrice).NotNull().NotEmpty();
        RuleFor(x => x.LineQuantity).NotNull().NotEmpty();
        RuleFor(x => x.LineNetAmount).NotNull().NotEmpty();
        RuleFor(x => x.LineAmountWithVat).NotNull().NotEmpty();
        RuleFor(x => x.TaxScheme).NotNull().NotEmpty().WithMessage(x => x.Description + " Tax Scheme is Empty");
        RuleFor(x => x.TaxSchemeId).NotNull().NotEmpty().WithMessage(x => x.Description + " Tax Scheme Id is Empty"); ;
        RuleFor(x => x.LineDiscountAmount).NotNull().NotEmpty();
        RuleFor(x => x.LineVatRate).NotNull().NotEmpty();
        RuleFor(x => x.LineVatAmount).NotNull().NotEmpty();
    }
}
