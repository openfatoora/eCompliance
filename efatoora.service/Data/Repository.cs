using efatoora.service.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace efatoora.service.Data
{
    public class Repository : DbContext
    {
        public Repository(DbContextOptions<Repository> options) : base(options)
        {
        }

        public DbSet<InvoiceLog> InvoiceLogs { get; set; }
        public DbSet<Key> Keys { get; set; }
    }
}
