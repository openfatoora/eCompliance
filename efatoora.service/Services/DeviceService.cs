using Agoda.IoC.Core;
using efatoora.Server.Contracts;
using efatoora.Server.Enums;
using efatoora.Server.Validators;
using efatoora.service.Contracts;
using efatoora.service.Data;
using efatoora.service.Data.Entities;
using System.Text;
using ZatcaCore.ApiClients;
using ZatcaCore.Contracts;

namespace efatoora.service.Services;

public interface IDeviceServices
{
    Task<ProdCsidOnboardingResponse> OnBoard(OnBoardContract onBoardingDetails);
    Task UpdateJsonKeys(KeysRequest keysRequest);
    Task<KeysResponse?> GetKeysAsync();

}
[RegisterPerRequest]
public class DeviceServices : IDeviceServices
{
    private readonly IZatcaUrlProviderService _zatcaUrlProviderService;
    private readonly IOnboardingTasks _onboardingTasks;
    private readonly IKeyRepository _keyRepository;
    private string _privateKey;
    public DeviceServices(IZatcaUrlProviderService zatcaUrlProviderService, IOnboardingTasks onboardingTasks, IKeyRepository keyRepository)
    {
        _zatcaUrlProviderService = zatcaUrlProviderService;
        _onboardingTasks = onboardingTasks;
        _keyRepository = keyRepository;
    }
    public async Task<ProdCsidOnboardingResponse> OnBoard(OnBoardContract onBoardingDetails)
    {
        OnBoardingContractValidator.IsOnBoardingDetailsValid(onBoardingDetails);

        var uniqueName = onBoardingDetails.DeviceName ?? Guid.NewGuid().ToString();

        Enum.TryParse(onBoardingDetails.SupportedInvoiceTypes, out SupportedInvoiceTypes invoiceType);

        if (!(invoiceType == SupportedInvoiceTypes.Both ||
              invoiceType == SupportedInvoiceTypes.Simplified ||
              invoiceType == SupportedInvoiceTypes.Standard))
        {
            throw new Exception("Invalid Supported Invoice Type");
        }

        BusinessDetail businessDetail = PopulateBusinessDetail(onBoardingDetails);
        var businessDetailId = businessDetail.UserId;

        var deviceGuid = Guid.NewGuid();

        Device deviceDetails = PopulateDeviceDetail(onBoardingDetails, uniqueName, businessDetailId, deviceGuid);

        ProdCsidOnboardingResponse prodTokenResponse = GenerateAndPersistCsrDetails(businessDetail, deviceDetails);

        KeysRequest updatedKeys = new KeysRequest
        {
            ProdBinaryToken = prodTokenResponse.BinarySecurityToken,
            Secret = prodTokenResponse.Secret,
            PrivateKey = _privateKey,

        };

        await UpdateJsonKeys(updatedKeys);

        return prodTokenResponse;
    }

    public async Task UpdateJsonKeys(KeysRequest keysRequest)
    {
        var currentKey = _keyRepository.GetKeys().Result.FirstOrDefault();

        if (currentKey == null)
        {
            Key key = new Key
            {
                PrivateKey = keysRequest.PrivateKey,
                BinaryToken = keysRequest.ProdBinaryToken,
                Secret = keysRequest.Secret,
                DateTime = DateTime.Now,
                Environment = "PROD"
            };

            await _keyRepository.Create(key);
            return;
        }
        else
        {
            currentKey.PrivateKey = keysRequest.PrivateKey;
            currentKey.BinaryToken = keysRequest.ProdBinaryToken;
            currentKey.Secret = keysRequest.Secret;
            currentKey.DateTime = DateTime.Now;
            currentKey.Environment = "PROD";

            await _keyRepository.Update(currentKey);
        }

    }
    private Device PopulateDeviceDetail(OnBoardContract onBoardingDetails, string uniqueName, long businessDetailId, Guid deviceGuid)
    {
        var deviceDetails = new Device()
        {
            UniqueNameOfUnit = $"{uniqueName}-{onBoardingDetails.Vat}-{onBoardingDetails.DeviceName}",
            UniqueIdentifier = $"1-{uniqueName}|2-{uniqueName}|3-{deviceGuid}",
            SupportedInvoiceTypes = onBoardingDetails.SupportedInvoiceTypes,
            OTP = onBoardingDetails.OTP, // validate otp with a validator
            UserId = 1,
            Name = onBoardingDetails.DeviceName,
            UUID = deviceGuid,
            Environment = onBoardingDetails.Environment,
            BusinessDetailId = businessDetailId
        };
        return deviceDetails;
    }

    private BusinessDetail PopulateBusinessDetail(OnBoardContract onBoardingDetails)
    {
        return new BusinessDetail()
        {
            UserId = 1,
            UUID = Guid.NewGuid(),
            Name = onBoardingDetails.BusinessName,
            VatNumber = onBoardingDetails.Vat,
            AdditionalIdNumber = onBoardingDetails.AdditionalIdNumber,
            AdditionalIdType = onBoardingDetails.AdditionalIdType,
            AdditionalNo = onBoardingDetails.AdditionalNo,
            BuildingNumber = onBoardingDetails.BuildingNumber,
            City = onBoardingDetails.City,
            Country = onBoardingDetails.CountryCode,
            District = onBoardingDetails.District,
            GroupVatNumber = onBoardingDetails.GroupVatNumber,
            State = onBoardingDetails.State,
            ZipCode = onBoardingDetails.ZipCode,
            Industry = onBoardingDetails.Industry,
            AddressRegistered = $"{onBoardingDetails.BuildingNumber} {onBoardingDetails.StreetName} " +
                                             $"{onBoardingDetails.City} {onBoardingDetails.District}"
        };
    }

    private ProdCsidOnboardingResponse GenerateAndPersistCsrDetails(BusinessDetail businessDetail, Device deviceDetails)
    {
        var (csr, privateKey, publicKey) = CsrGenerator.Generate(deviceDetails, businessDetail);

        Enum.TryParse(deviceDetails.Environment, out ZatcaEnvironment environment);

        //TODO : handle prod url based on device 
        IComplianceCsrApiClient _complianceCsrApiClient = new ComplianceCsrApiClient(_zatcaUrlProviderService.GetUrls(environment));

        ComplianceCsrRequest complianceCsrRequest = new ComplianceCsrRequest(deviceDetails.OTP, csr);

        var complianceResponse = _complianceCsrApiClient.GetToken(complianceCsrRequest);

        var bytes = Encoding.UTF8.GetBytes(privateKey);
        var base64 = Convert.ToBase64String(bytes);
        _privateKey = base64;

        if (complianceResponse.DispositionMessage == "ISSUED")
        {
            deviceDetails.Status = nameof(OnBoardingStatus.ComplianceSuccess);
            deviceDetails.ComplianceBinaryToken = complianceResponse.BinarySecurityToken;
            deviceDetails.ComplianceSecret = complianceResponse.Secret;
            deviceDetails.ComplianceRequestId = complianceResponse.requestID.ToString();
            deviceDetails.Csr = csr;
        }
        else
        {
            deviceDetails.Status = nameof(OnBoardingStatus.ComplianceFail);
        }
        deviceDetails.privateKey = privateKey;
        deviceDetails.publicKey = publicKey;

        if (complianceResponse.DispositionMessage == "ISSUED")
        {
            return _onboardingTasks.Run(deviceDetails, businessDetail);
        }
        else
        {
            throw new Exception("Compliance Failed");
        }

    }

    public async Task<KeysResponse?> GetKeysAsync()
    {

        var key = (await _keyRepository.GetKeys()).FirstOrDefault();
        if (key == null)
        {
            return default;
        }

        return new KeysResponse
        {
            PrivateKey = key.PrivateKey,
            ProdBinaryToken = key.BinaryToken,
            Secret = key.Secret,
            UpdatedAt = key.DateTime.ToString(),
            Environment = key.Environment
        };


    }
}
