using Agoda.IoC.Core;
using efatoora.service.Data;
using efatoora.service.Data.Entities;

namespace efatoora.service.Services
{

    public interface IInvoiceLogService
    {
        Task AddInvoiceLog(InvoiceLog invoiceLog);
        Task<IEnumerable<object>> GetInvoiceLogs(DateTime fromDate, DateTime toDate);
    }
    [RegisterPerRequest]
    public class InvoiceLogService : IInvoiceLogService
    {
        private readonly IInvoiceLogRepository _invoiceLogRepository;

        public InvoiceLogService(IInvoiceLogRepository invoiceLogRepository)
        {
            _invoiceLogRepository = invoiceLogRepository;
        }

        public async Task AddInvoiceLog(InvoiceLog invoiceLog)
        {
            await _invoiceLogRepository.AddInvoiceLog(invoiceLog);
        }

        public async Task<IEnumerable<object>> GetInvoiceLogs(DateTime fromDate, DateTime toDate)
        {
            return await _invoiceLogRepository.GetInvoiceLogs(fromDate, toDate);
        }
    }
}
