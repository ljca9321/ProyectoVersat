using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLibreriaVersat.Model
{
    public class Movimiento
    {
        public int mov_id { get; set; }
        public DateTime mov_fecha { get; set; }
        public string mov_concepto { get; set; } = string.Empty;
        public decimal mov_valor;
    }
}
