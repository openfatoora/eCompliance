using efatoora.Server.Utils;
using FluentValidation;
using ZatcaCore.Contracts;

namespace efatoora.service.Validators;

public class InvoiceContractValidator : AbstractValidator<InvoiceContract>
{
    public InvoiceContractValidator()
    {
        RuleFor(x => x.InvoiceType).NotEmpty().NotNull();
        RuleFor(x => x.InvoiceTypeCode).NotEmpty().NotNull();
        RuleFor(x => x.ReferenceId).NotEmpty().NotNull();
        RuleFor(x => x.IssueDate).NotEmpty().NotNull();
        RuleFor(x => x.IssueTime).NotEmpty().NotNull();
        RuleFor(x => x.PreviousHash).NotEmpty().NotNull();
        RuleFor(x => x.Seller).NotEmpty().NotNull().SetValidator(new SellerValidator());
        //RuleFor(x => x.Buyer).NotEmpty().NotNull().SetValidator(new BuyerValidator())
        //.When(x => (x.Buyer != null && x.InvoiceTypeCode == nameof(InvoiceTypeCodes.Standard)));
        RuleForEach(x => x.LineItems).NotEmpty().NotNull()
            .SetValidator(new LineItemValidator());
        RuleFor(x => x.LineItems).NotEmpty().NotNull();
        RuleFor(x => x.TotalAmountWithoutVat).NotEmpty().NotNull();
        //RuleFor(x => x.TotalTaxableAmount).NotEmpty().NotNull();
        RuleFor(x => x.TotalVatAmount).NotEmpty().NotNull();
        RuleFor(x => x.TotalAmountWithVat).NotEmpty().NotNull();
        RuleFor(x => x.TotalDiscountAmount).NotEmpty().NotNull();
        RuleFor(x => x.TaxCategory).NotEmpty().NotNull();
        RuleFor(x => x.SupplyDate).NotEmpty().NotNull();
        RuleFor(x => x.LastestSupplyDate).NotEmpty().NotNull();
        RuleFor(x => x.InvoiceCurrencyCode).NotEmpty().NotNull();
        RuleFor(x => x.TaxCurrencyCode).NotEmpty().NotNull();
        RuleFor(x => x.Note).NotNull().NotEmpty().SetValidator(new NoteValidator()).When(x => IsNote(x));

        //RuleFor(x => x).Must(ValidateTotals).WithMessage("Invalid Total");
    }
    public static void IsInvoiceDetailsValid(InvoiceContract invoiceContract)
    {
        InvoiceContractValidator detailsValidator = new InvoiceContractValidator();
        var result = detailsValidator.Validate(invoiceContract);

        if (!result.IsValid)
        {
            ErrorHandler.HandleValidationErrors(result.Errors);
        }

    }


    private static bool IsNote(InvoiceContract invoiceContract)
    {
        Enum.TryParse(invoiceContract.InvoiceType, out InvoiceTypes invoiceType);

        if (invoiceType == InvoiceTypes.TAX_INVOICE)
        {
            return false;
        }
        return true;
    }

}
