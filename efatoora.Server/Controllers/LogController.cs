using efatoora.service.Services;
using Microsoft.AspNetCore.Mvc;

namespace efatoora.Server.Controllers
{
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly IInvoiceLogService invoiceLogService;

        public LogController(IInvoiceLogService invoiceLogService)
        {
            this.invoiceLogService = invoiceLogService;
        }

        [Route("Log/GetInvoiceLogs")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<object>> GetInvoiceLogsAsync(DateTime fromDate, DateTime toDate)
        {
            var result =   await invoiceLogService.GetInvoiceLogs(fromDate,toDate);
            return Ok(result);
        }
    }
}
