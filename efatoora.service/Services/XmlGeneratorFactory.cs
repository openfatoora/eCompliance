using ZatcaCore.Contracts;
using ZatcaCore.XmlGenerators;

namespace efatoora.service.Services;

public static class XmlGeneratorFactory
{
    public static IXmlGenerator GetXmlGenerator(InvoiceTypes invoiceType)
    {
        IXmlGenerator _xmlGenerator = null;

        if (invoiceType == InvoiceTypes.TAX_INVOICE)
        {
            _xmlGenerator = new SimplifiedInvoiceXmlGenerator();
        }
        else if (invoiceType == InvoiceTypes.CREDIT_NOTE)
        {
            _xmlGenerator = new SimplifiedCreditNoteXmlGenerator();

        }
        else if (invoiceType == InvoiceTypes.DEBIT_NOTE)
        {
            _xmlGenerator = new SimplifiedDebitNoteXmlGenerator();

        }
        return _xmlGenerator;
    }
}
