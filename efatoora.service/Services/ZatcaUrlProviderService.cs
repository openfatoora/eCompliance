using Agoda.IoC.Core;
using ZatcaCore.Contracts;

namespace efatoora.service.Services;

public interface IZatcaUrlProviderService
{
    ZatcaUrl GetUrls(ZatcaEnvironment environment);
}

[RegisterSingleton]
public class ZatcaUrlProviderService : IZatcaUrlProviderService
{
    public ZatcaUrl GetUrls(ZatcaEnvironment environment)
    {
        if (environment == ZatcaEnvironment.Production)
        {
            return new ZatcaUrl()
            {
                Base = "https://gw-fatoora.zatca.gov.sa",
                ComplianceCsid = "e-invoicing/core/compliance",
                ProductionCsid = "/e-invoicing/core/production/csids",
                ComplianceApi = "/e-invoicing/core/compliance/invoices",
                Reporting = "/e-invoicing/core/invoices/reporting/single",
                Clearance = "/e-invoicing/core/invoices/clearance/single",
                Renewal = "/e-invoicing/core/production/csids"
            };
        }
        else if (environment == ZatcaEnvironment.Simulation)
        {
            return new ZatcaUrl()
            {
                Base = "https://gw-fatoora.zatca.gov.sa",
                ComplianceCsid = "e-invoicing/simulation/compliance",
                ProductionCsid = "/e-invoicing/simulation/production/csids",
                ComplianceApi = "/e-invoicing/simulation/compliance/invoices",
                Reporting = "/e-invoicing/simulation/invoices/reporting/single",
                Clearance = "/e-invoicing/simulation/invoices/clearance/single",
                Renewal = "/e-invoicing/simulation/production/csids"
            };
        }
        else
        {
            return new ZatcaUrl()
            {
                Base = "https://gw-fatoora.zatca.gov.sa",
                ComplianceCsid = "e-invoicing/developer-portal/compliance",
                ProductionCsid = "/e-invoicing/developer-portal/production/csids",
                ComplianceApi = "/e-invoicing/developer-portal/compliance/invoices",
                Reporting = "/e-invoicing/developer-portal/invoices/reporting/single",
                Clearance = "e-invoicing/developer-portal/invoices/clearance/single",
                Renewal = "/e-invoicing/developer-portal/production/csids"
            };
        }

    }
}
