using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLibreriaVersat.Model
{
    public class ResultadoContable
    {
        public string año { get; set; } = string.Empty;
        public decimal activosFijos { get; set; }
        public decimal activosCorrientes { get; set; }
        public decimal totalActivos { get; set; }
        public decimal pasivosCorrientes { get; set; }
        public decimal pasivosLargoPlazo { get; set; }
        public decimal totalPasivos { get; set; }
        public decimal patrimonio { get; set; }
        public decimal ingresosTotales { get; set; }
        public decimal gastosTotales { get; set; }
        public decimal liquidez { get; set; }
        public decimal rentabilidad { get; set; }
    }
    
}
