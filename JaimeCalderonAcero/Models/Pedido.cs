using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaimeCalderonAcero.Models
{
    public class Pedido
    {
        public string CodPedido { get; set; }
        public DateTime FechEntrega { get; set; }
        public string FormaEnvio { get; set; }
        public int Importe { get; set; }
    }
}
