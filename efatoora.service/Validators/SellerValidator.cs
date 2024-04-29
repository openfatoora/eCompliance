using FluentValidation;
using ZatcaCore.Contracts;

namespace efatoora.service.Validators;

public class SellerValidator : AbstractValidator<Seller>
{
    public SellerValidator()
    {
        RuleFor(x => x.AdditionalIdNumber).NotEmpty();
        RuleFor(x => x.AdditionalIdType).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.VatNumber).NotEmpty();
        RuleFor(x => x.Address).NotEmpty().NotNull()
            .SetValidator(new Addressvalidator()).When(x => x.Address != null);
    }

}
