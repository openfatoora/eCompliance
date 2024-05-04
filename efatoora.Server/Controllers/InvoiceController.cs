using efatoora.Server.Enums;
using efatoora.service;
using efatoora.service.Data;
using efatoora.service.Data.Entities;
using efatoora.service.Services;
using efatoora.service.Validators;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZatcaCore.Contracts;

namespace efatoora.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController(ILogger<InvoiceController> logger, IInvoiceLogService invoiceLogService, IKeyRepository keyRepository, IZatcaUrlProviderService zatcaUrlProviderService) : ControllerBase
    {

        [Route("Generate")]
        [HttpPost]
        public async Task<ActionResult<EInvoiceSignerResponse>> GenerateInvoiceAsync([FromBody] InvoiceContract invoiceContract)
        {
            var invoiceLog = new InvoiceLog
            {
                InvoiceNo = invoiceContract.Id,
                DateTime = DateTime.Now,
                OperationType = InvoiceOperationType.Generation.ToString(),
                Input = JsonConvert.SerializeObject(invoiceContract)
            };
            try
            {
                InvoiceContractValidator.IsInvoiceDetailsValid(invoiceContract);
                var invoiceGenerator = new InvoiceGenerator(keyRepository);
                var invoiceResponse = invoiceGenerator.GenerateAsync(invoiceContract);
                invoiceLog.Output = JsonConvert.SerializeObject(invoiceResponse);
                invoiceLog.Status = InvoiceLogStatus.Success.ToString();
                await invoiceLogService.AddInvoiceLog(invoiceLog);
                return Ok(invoiceResponse);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while generating invoice");
                invoiceLog.Output = JsonConvert.SerializeObject(ex);
                invoiceLog.Status = InvoiceLogStatus.Success.ToString();
                await invoiceLogService.AddInvoiceLog(invoiceLog);
                return BadRequest(ex.Message);
            }


        }

        [Route("/v1/Invoice/Report")]
        [HttpPost]

        public ActionResult<InvoiceReportingResponse> Report(InvoiceContract data)
        {
            try
            {
                InvoiceContractValidator.IsInvoiceDetailsValid(data);
                var invoiceServices = new InvoiceServices(keyRepository, zatcaUrlProviderService);
                var result = invoiceServices.Report(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("/v1/Invoice/Clear")]
        [HttpPost]

        public ActionResult<InvoiceClearanceResponse> Clear(InvoiceContract data)
        {
            try
            {
                InvoiceContractValidator.IsInvoiceDetailsValid(data);
                var invoiceServices = new InvoiceServices(keyRepository, zatcaUrlProviderService);
                var result = invoiceServices.Clear(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
