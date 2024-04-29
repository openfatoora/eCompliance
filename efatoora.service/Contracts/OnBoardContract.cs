using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace efatoora.Server.Contracts;

public class OnBoardContract
{
    [Required]
    [DefaultValue("123456")]
    public string OTP { get; set; }
    [Required]
    [DefaultValue("Unique Name")]
    public string DeviceName { get; set; }
    [Required]
    [DefaultValue("Infinite")]
    public string BusinessName { get; set; }
    [Required]
    [DefaultValue("SA")]
    public string CountryCode { get; set; }
    [Required]
    [DefaultValue("0100")]
    public string SupportedInvoiceTypes { get; set; }
    [Required]
    [DefaultValue("Software")]
    public string Industry { get; set; }
    [Required]
    [DefaultValue("OTH")]
    public string AdditionalIdType { get; set; }
    [Required]
    [DefaultValue("1234")]
    public string AdditionalIdNumber { get; set; }
    [Required]
    [DefaultValue("310175397400003")]
    public string Vat { get; set; }
    [DefaultValue("310175397400003")]
    public string GroupVatNumber { get; set; }
    [Required]
    [DefaultValue("street name")]
    public string StreetName { get; set; }
    [Required]
    [DefaultValue("1234")]
    public string AdditionalNo { get; set; }
    [Required]
    [DefaultValue("1234")]
    public string BuildingNumber { get; set; }
    [Required]
    [DefaultValue("Riyadh")]
    public string City { get; set; }
    [Required]
    [DefaultValue("Riyadh")]
    public string State { get; set; }
    [Required]
    [DefaultValue("12345")]
    public string ZipCode { get; set; }
    [Required]
    [DefaultValue("fgffA")]
    public string District { get; set; }

    [Required]
    [DefaultValue("SandBox")]
    public string Environment { get; set; }
}
