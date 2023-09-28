using ProyectoLibreriaVersat.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ProyectoLibreriaVersat.DAL
{
    public class MovimientoDAL
    {
        private readonly string _cadenaConexion;

        public MovimientoDAL(string cadenaconexion)
        {
            _cadenaConexion = cadenaconexion;
        }

        public async Task<int> GuardarMovimientos(List<Movimiento> lstMovimientos)
        {
            try
            {
                int respuesta = 0;
                foreach (Movimiento item in lstMovimientos)
                {
                    using (var connection = new SqlConnection(_cadenaConexion))
                    {
                        connection.Open();
                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                respuesta = await connection.ExecuteScalarAsync<int>("con_movimiento_add_edit", new
                                {
                                    item.mov_fecha,
                                    item.mov_concepto,
                                    item.mov_valor
                                }, commandType: CommandType.StoredProcedure, transaction: transaction);

                                transaction.Commit();

                            }
                            catch (Exception)
                            {
                                transaction.Rollback();
                                connection.Close();
                                throw;
                            }
                        }
                    }
                }

                return respuesta;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<bool> EliminarMovimientos()
        {
            try
            {
                bool respuesta = true;
                using (var connection = new SqlConnection(_cadenaConexion))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            await connection.ExecuteScalarAsync<int>("con_movimiento_delete", commandType: CommandType.StoredProcedure, transaction: transaction);
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            respuesta = false;
                            transaction.Rollback();
                            connection.Close();
                            throw;
                        }
                    }
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ResultadoContable>> ObtenerResultadoContable()
        {
            using (IDbConnection connection = new SqlConnection(_cadenaConexion))
            {
                return (List<ResultadoContable>)await connection.QueryAsync<ResultadoContable>("con_analisis_movimiento_contable", null, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
