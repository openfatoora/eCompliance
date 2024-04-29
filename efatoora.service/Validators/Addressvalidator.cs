using FluentValidation;
using ZatcaCore.Contracts;

namespace efatoora.service.Validators;

public class Addressvalidator : AbstractValidator<Address>
{
    public Addressvalidator()
    {
        RuleFor(x => x.ZipCode);
        RuleFor(x => x.Country).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.StreetName).NotEmpty();
        RuleFor(x => x.BuildingNumber).NotEmpty();
        RuleFor(x => x.Country).NotEmpty();
        RuleFor(x => x.AdditionalNo).NotEmpty();
        RuleFor(x => x.District).NotEmpty();
    }
}

