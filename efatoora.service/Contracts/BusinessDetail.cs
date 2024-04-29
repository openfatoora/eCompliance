namespace efatoora.service.Contracts;

public class BusinessDetail
{
    public Guid UUID { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; }
    //public string Name_AR { get; set; }
    public string AdditionalIdType { get; set; }
    public string AdditionalIdNumber { get; set; }
    public string VatNumber { get; set; }
    public string GroupVatNumber { get; set; }
    public string StreetName { get; set; }
    //public string StreetName_AR { get; set; }
    public string AdditionalNo { get; set; }
    public string BuildingNumber { get; set; }
    public string City { get; set; }
    //public string City_AR { get; set; }
    public string State { get; set; }
    //public string State_AR { get; set; }
    public string ZipCode { get; set; }
    public string District { get; set; }
    public string Country { get; set; }
    public string Industry { get; set; }
    public string AddressRegistered { get; set; }

    //public ICollection<Device> Devices { get; set; }
}
