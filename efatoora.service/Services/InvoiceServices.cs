using System.Text;
using System.Xml;
using efatoora.service.Data;
using efatoora.service.Services;
using ZatcaCore;
using ZatcaCore.ApiClients;
using ZatcaCore.Contracts;
using ZatcaCore.ESigner;

namespace efatoora.service
{
    public interface IInvoiceServices
    {
        Task<InvoiceReportingResponse> Report(InvoiceContract invoiceContract); // Added Task<>
    }

    public class InvoiceServices : IInvoiceServices
    {
        private readonly IKeyRepository keyRepository; // Added keyRepository field
        private readonly IZatcaUrlProviderService zatcaUrlProviderService; // Fixed naming here

        public InvoiceServices(IKeyRepository keyRepository, IZatcaUrlProviderService zatcaUrlProviderService)
        {
            this.keyRepository = keyRepository;
            this.zatcaUrlProviderService = zatcaUrlProviderService; // Fixed naming here
        }
        public async Task<InvoiceReportingResponse> Report(InvoiceContract invoiceContract) // Updated method signature
        {
            Enum.TryParse(invoiceContract.InvoiceType, out InvoiceTypes invoiceType);
            Enum.TryParse(invoiceContract.InvoiceTypeCode, out InvoiceTypeCodes invoiceTypeCode);
            if (invoiceTypeCode == InvoiceTypeCodes.Standard)
            {
                throw new Exception("Invalid Document Type Code");
            }
            var _xmlGenerator = XmlGeneratorFactory.GetXmlGenerator(invoiceType);

            var xmlBytes = _xmlGenerator.Generate(invoiceContract);
            string generatedXML = Encoding.UTF8.GetString(xmlBytes);

            XmlDocument xml = new XmlDocument()
            {
                PreserveWhitespace = true
            };

            IHashGenerator hashGenerator = new HashGenerator();
            IDigitalSignartureGenerator digitalSignartureGenerator = new DigitalSignartureGenerator();
            IQrGenerator qrGenerator = new QrGenerator();
            xml.LoadXml(generatedXML);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Use the XmlDocument.Save method to save the XML data to the MemoryStream
                xml.Save(memoryStream);

                // Convert the MemoryStream to a byte array
                byte[] byteArray = memoryStream.ToArray();

            }

            var key = (await keyRepository.GetKeys()).First();
            Enum.TryParse(key.Environment, out ZatcaEnvironment environment);

            var eInvoiceSignerResponse = new EInvoiceSigner(hashGenerator, digitalSignartureGenerator, qrGenerator,
                DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"))
                .SignDocument(xml, key?.BinaryToken, key?.PrivateKey);

            string invoiceHash = hashGenerator.Generate(xml);
            var referenceId = invoiceContract.ReferenceId = Guid.NewGuid();

            var resultAPiCall = new
            {
                invoiceHash = invoiceHash,
                uuid = referenceId,
                invoice = Convert.ToBase64String(Encoding.UTF8.GetBytes(eInvoiceSignerResponse.Xml))

            };
            var zatcaUrl = zatcaUrlProviderService.GetUrls(environment);
            IReportingApiClient _reportingInvoiceApiClient = new ReportingApiClient(zatcaUrl);
            InvoiceReportingResponse? reportingResponse = null;
            try
            {
                reportingResponse = _reportingInvoiceApiClient.ReportInvoice(new InvoiceReportingRequest(
               resultAPiCall,
               key?.BinaryToken,
               key?.PrivateKey
               ));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return reportingResponse;

        }
    }
}
