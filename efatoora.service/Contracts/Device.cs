using System.ComponentModel.DataAnnotations.Schema;

namespace efatoora.service.Contracts;

public class Device
{
    public long UserId { get; set; }
    public string Name { get; set; }
    public Guid UUID { get; set; }
    [ForeignKey("BusinessDetail")]
    public long BusinessDetailId { get; set; }
    //public virtual BusinessDetail BusinessDetail { get; set; }
    public string OTP { get; set; }
    public string UniqueNameOfUnit { get; set; }
    public string UniqueIdentifier { get; set; }
    public string SupportedInvoiceTypes { get; set; }
    public string Status { get; set; }
    public string Environment { get; set; }
    public string ComplianceRequestId { get; set; }
    public string ComplianceBinaryToken { get; set; }
    public string ComplianceSecret { get; set; }
    //public string ProdRequestId { get; set; }
    //public string ProdBinaryToken { get; set; }
    //public string ProdSecret { get; set; }
    public string Csr { get; set; }
    public string privateKey { get; set; }
    public string publicKey { get; set; }
}
