using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace efatoora.service.Data.Entities
{
    public class Key
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string PrivateKey { get; set; }
        public string BinaryToken { get; set; }
        public string Secret { get; set; }
        public DateTime DateTime { get; set; }
        public string Environment { get; set; }
    }
}
