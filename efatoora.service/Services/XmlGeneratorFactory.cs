using ZatcaCore.Contracts;
using ZatcaCore.XmlGenerators;

namespace Services.InvoiceService
{
    public static class XmlGeneratorFactory
    {
        public static IXmlGenerator GetXmlGenerator(InvoiceTypes invoiceType, bool isStandard)
        {
            IXmlGenerator _xmlGenerator = null;

            if (isStandard && invoiceType == InvoiceTypes.TAX_INVOICE)
            {
                _xmlGenerator = new StandardInvoiceXmlGenerator();
            }
            else if (isStandard && invoiceType == InvoiceTypes.CREDIT_NOTE)
            {
                _xmlGenerator = new StandardCreditNoteXmlGenerator();

            }
            else if (isStandard && invoiceType == InvoiceTypes.DEBIT_NOTE)
            {
                _xmlGenerator = new StandardDebitNoteXmlGenerator();

            }
            else if (!isStandard && invoiceType == InvoiceTypes.TAX_INVOICE)
            {
                _xmlGenerator = new SimplifiedInvoiceXmlGenerator();
            }
            else if (!isStandard && invoiceType == InvoiceTypes.CREDIT_NOTE)
            {
                _xmlGenerator = new SimplifiedCreditNoteXmlGenerator();

            }
            else if (!isStandard && invoiceType == InvoiceTypes.DEBIT_NOTE)
            {
                _xmlGenerator = new SimplifiedDebitNoteXmlGenerator();

            }
            return _xmlGenerator;
        }
    }
}
