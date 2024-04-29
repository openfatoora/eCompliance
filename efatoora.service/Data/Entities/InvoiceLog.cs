using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace efatoora.service.Data.Entities
{
    public class InvoiceLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string InvoiceNo { get; set; }
        public string OperationType { get; set; }
        public string Status { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public DateTime DateTime { get; set; }

    }
}
