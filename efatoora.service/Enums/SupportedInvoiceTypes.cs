using System.ComponentModel;

namespace efatoora.Server.Enums;

public enum SupportedInvoiceTypes
{
    Standard = 1000,
    Simplified = 0100,
    Both = 1100,
}

public enum OnBoardingStatus
{
    [Description("Compliance Fail")]
    ComplianceFail,

    [Description("Compliance Success")]
    ComplianceSuccess,

    [Description("Production Token Fail")]
    ProductionTokenFail,

    [Description("Production Token Success")]
    ProductionTokenSuccess,
}



public enum  InvoiceOperationType
{
    Generation,
    Reporting,
    Clearance
}

public enum InvoiceLogStatus
{
    Success,
    Fail
}
