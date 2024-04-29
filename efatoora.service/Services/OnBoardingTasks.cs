using Agoda.IoC.Core;
using efatoora.Server.Enums;
using efatoora.service.Contracts;
using Serilog;
using ZatcaCore.ApiClients;
using ZatcaCore.Compliance;
using ZatcaCore.Contracts;

namespace efatoora.service.Services;

public interface IOnboardingTasks
{
    ProdCsidOnboardingResponse Run
        (Device device, BusinessDetail businessDetail);

}

[RegisterPerRequest]
public class OnboardingTasks : IOnboardingTasks
{
    private readonly ICheckCompliances _checkCompliances;
    private readonly IZatcaUrlProviderService _zatcaUrlProviderService;


    public OnboardingTasks(ICheckCompliances checkCompliances, IZatcaUrlProviderService zatcaUrlProviderService)
    {
        _checkCompliances = checkCompliances;
        _zatcaUrlProviderService = zatcaUrlProviderService;
    }

    public ProdCsidOnboardingResponse Run(Device device, BusinessDetail businessDetail)
    {
        Enum.TryParse(device.SupportedInvoiceTypes, out SupportedInvoiceTypes invoiceType);
        Enum.TryParse(device.Environment, out ZatcaEnvironment environment);

        var sellerDetails = new Seller()
        {
            AdditionalIdType = businessDetail.AdditionalIdType,
            AdditionalIdNumber = businessDetail.AdditionalIdNumber,
            Address = new Address()
            {
                StreetName = businessDetail.StreetName,
                BuildingNumber = businessDetail.BuildingNumber,
                City = businessDetail.City,
                District = businessDetail.District,
                Country = businessDetail.Country,
                State = businessDetail.State,
                ZipCode = businessDetail.ZipCode,
                AdditionalNo = businessDetail.AdditionalNo,
            },
            Name = businessDetail.Name,
            VatNumber = businessDetail.VatNumber
        };

        var zatcaUrl = _zatcaUrlProviderService.GetUrls(environment);
        try
        {

            if (invoiceType == SupportedInvoiceTypes.Simplified || invoiceType == SupportedInvoiceTypes.Both)
            {
                var simplifiedInvoiceResponse = _checkCompliances.CheckSimplifiedInvoice(zatcaUrl, device.ComplianceBinaryToken, device.privateKey, sellerDetails, device.ComplianceSecret);
                var simplifiedCreditNoteResponse = _checkCompliances.CheckSimplifiedCreditNote(zatcaUrl, device.ComplianceBinaryToken, device.privateKey, sellerDetails, device.ComplianceSecret);
                var simplifiedDebitNoteResponse = _checkCompliances.CheckSimplifiedDebitNote(zatcaUrl, device.ComplianceBinaryToken, device.privateKey, sellerDetails, device.ComplianceSecret);
            }

            var response = GetProductionToken(zatcaUrl, device);

            return response;

        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            throw;
        }


    }

    private ProdCsidOnboardingResponse GetProductionToken(ZatcaUrl zatcaUrl, Device device)
    {
        //TODO : handle sandbox
        IProdCsidApiClient _prodCsidApiClient = new ProdCsidApiClient(zatcaUrl);

        ProdCsidOnboardingRequest prodCsidOnboardingRequest = new ProdCsidOnboardingRequest(device.ComplianceRequestId, device.ComplianceBinaryToken, device.ComplianceSecret);

        return _prodCsidApiClient.GetToken(prodCsidOnboardingRequest);

    }
}
