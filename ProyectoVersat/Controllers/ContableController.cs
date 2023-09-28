using Dapper;
using Microsoft.AspNetCore.Mvc;
using ProyectoLibreriaVersat.DAL;
using ProyectoLibreriaVersat.Model;
using ProyectoVersat.Request;
using System.Data.SqlClient;


namespace ProyectoVersat.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ContableController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ContableController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpPost]
        public async Task<ActionResult<List<ResultadoContable>>> InformacionContable(List<RegistroContableRequest> oRequest)
        {
            try
            {
                string connection = _configuration.GetConnectionString("DefaultConnection");
                await new MovimientoDAL(connection).EliminarMovimientos();
                List<Movimiento> lstMovimientos = new List<Movimiento>();

                List<ResultadoContable> resultadoContables = new List<ResultadoContable>();

                foreach (RegistroContableRequest item in oRequest)
                {
                    lstMovimientos.Add(new Movimiento() { 
                        mov_fecha = item.fecha,
                        mov_concepto = item.concepto,
                        mov_valor = item.valor}
                    );                    
                }

                if (lstMovimientos.Any())
                {
                    var movimientoGuardado = await new MovimientoDAL(connection).GuardarMovimientos(lstMovimientos);
                    resultadoContables = await new MovimientoDAL(connection).ObtenerResultadoContable();
                }

                return Ok(resultadoContables);
            }
            catch (Exception ex)
            {
                var mensaje = "No se logró consultar información de la empresa dado el siguiente error: " + ex.Message;
                return BadRequest(mensaje);
            }
        }
    }
}