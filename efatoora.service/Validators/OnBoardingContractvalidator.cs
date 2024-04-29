using efatoora.Server.Contracts;
using efatoora.Server.Utils;
using FluentValidation;

namespace efatoora.Server.Validators;

public class OnBoardingContractValidator : AbstractValidator<OnBoardContract>
{
    public OnBoardingContractValidator()
    {
        RuleFor(x => x.OTP).NotEmpty().Length(6).Matches("^[0-9]*$");
        RuleFor(x => x.DeviceName).NotEmpty();
        RuleFor(x => x.BusinessName).NotEmpty();
        RuleFor(x => x.CountryCode).Length(2);
        RuleFor(x => x.SupportedInvoiceTypes).NotEmpty();
        RuleFor(x => x.Industry).NotEmpty();
        RuleFor(x => x.AdditionalIdType).NotEmpty();
        RuleFor(x => x.AdditionalIdNumber).NotEmpty();
        RuleFor(x => x.Vat).NotEmpty().Length(15).Must(VatNumberValidator);
        RuleFor(x => x.StreetName).NotEmpty();
        RuleFor(x => x.AdditionalNo).NotEmpty();
        RuleFor(x => x.BuildingNumber).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.State).NotEmpty();
        RuleFor(x => x.ZipCode).NotEmpty(); //TODO: seller zipcode must be 5 digits -> warning
        RuleFor(x => x.District).NotEmpty();
        RuleFor(x => x.Environment).NotEmpty();
    }

    private bool VatNumberValidator(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length != 15)
        {
            return false;
        }

        if (value[0] != '3' || value[14] != '3')
        {
            return false;
        }

        for (int i = 1; i < 9; i++)
        {
            if (!char.IsDigit(value[i]))
            {
                return false;
            }
        }

        return true;
    }

    public static void IsOnBoardingDetailsValid(OnBoardContract onBoardingDetails)
    {
        OnBoardingContractValidator detailsValidator = new OnBoardingContractValidator();
        var result = detailsValidator.Validate(onBoardingDetails);

        if (!result.IsValid)
        {
            ErrorHandler.HandleValidationErrors(result.Errors);
        }

    }
}
