using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efatoora.service.Contracts;

public class KeysResponse
{
    public string PrivateKey { get; set; }
    public string ProdBinaryToken { get; set; }
    public string Secret { get; set; }
    public string UpdatedAt { get; set; }
    public string Environment { get; set; }
}