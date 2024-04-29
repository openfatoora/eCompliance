using Agoda.IoC.Core;
using efatoora.service.Data.Entities;

namespace efatoora.service.Data
{
    public interface IInvoiceLogRepository
    {
        Task<InvoiceLog> AddInvoiceLog(InvoiceLog invoiceLog);
        Task DeleteInvoiceLogs(int id);
        Task DeleteInvoiceLogsByIds(IEnumerable<long> ids);
        Task<InvoiceLog> GetInvoiceLogById(int id);
        Task<IEnumerable<object>> GetInvoiceLogs(DateTime fromDate, DateTime toDate);
    }

    [RegisterPerRequest]

    public class InvoiceLogRepository : IInvoiceLogRepository
    {

        private readonly Repository _context;

        public InvoiceLogRepository(Repository context)
        {
            _context = context;
        }

        public async Task<InvoiceLog> AddInvoiceLog(InvoiceLog invoiceLog)
        {
            _context.InvoiceLogs.Add(invoiceLog);
            await _context.SaveChangesAsync();
            return invoiceLog;
        }

        public async Task<InvoiceLog> GetInvoiceLogById(int id)
        {
            return await _context.InvoiceLogs.FindAsync(id);
        }

        public async Task<IEnumerable<object>> GetInvoiceLogs(DateTime fromDate, DateTime toDate)
        {
            var invoiceLogs = _context.InvoiceLogs.Where(x => x.DateTime >= fromDate && x.DateTime <= toDate)
                .Select(x => new
                {
                    x.Id,
                    x.InvoiceNo,
                    x.OperationType,
                    x.Status,
                    x.DateTime
                })
                .ToList();
            return invoiceLogs;
        }

        public async Task DeleteInvoiceLogsByIds(IEnumerable<long> ids)
        {
            var invoiceLogs = _context.InvoiceLogs.Where(x => ids.Contains(x.Id));
            _context.InvoiceLogs.RemoveRange(invoiceLogs);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteInvoiceLogs(int id)
        {
            var invoiceLog = await GetInvoiceLogById(id);
            _context.InvoiceLogs.Remove(invoiceLog);
            await _context.SaveChangesAsync();
        }
    }
}
