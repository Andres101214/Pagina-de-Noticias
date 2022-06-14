using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data;
using System.Data.SqlClient;

namespace Persistencia
{
    public class PersistenciaListPeriodista
    {

        internal static void Alta(Periodista unPer, string CodNot, SqlTransaction _pTransaccion)
        {
            SqlCommand _comando = new SqlCommand("AsignarPeriodista", _pTransaccion.Connection);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Codigo", CodNot);
            _comando.Parameters.AddWithValue("@Cedula", unPer.CI);
            SqlParameter _ParmRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _ParmRetorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_ParmRetorno);


            try
            {
                //Determino que debo trabajar con la misma transaccion
                _comando.Transaction = _pTransaccion;

                //Ejecuto comando
                _comando.ExecuteNonQuery();

                //Verifico errores
                int retorno = Convert.ToInt32(_ParmRetorno.Value);
                if (retorno == -1)
                    throw new Exception("Periodista Inactivo");
                else if (retorno == -2)
                    throw new Exception("Noticia ya existe");
                else if (retorno == -3)
                    throw new Exception("Periodista y Codigo ya estan asociados a esta noticia");
                else if (retorno == -4)
                    throw new Exception("Error no identificado");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        internal static void EliminarPerNoticia(Noticia unaNot, SqlTransaction _pTransaccion)
        {
            SqlCommand _comando = new SqlCommand("EliminarPeriodistaAsignado", _pTransaccion.Connection);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Codigo", unaNot.CodInt);
            SqlParameter _ParmRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _ParmRetorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_ParmRetorno);


            try
            {

                _comando.Transaction = _pTransaccion;

                //ejecuto comando
                _comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        internal static List<Periodista> CargoPeriodistas(string CodNot)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);

            SqlCommand _comando = new SqlCommand("CargarPeriodistasNoticia", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Codigo", CodNot);

            List<Periodista> _ListaPer = new List<Periodista>();

            try
            {
                //me conecto
                _cnn.Open();

                //ejecuto consulta
                SqlDataReader _lector = _comando.ExecuteReader();

                //verifico si hay Periodistas
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        _ListaPer.Add(new Periodista((string)_lector["Cedula"], (string)_lector["Nombre"], (string)_lector["Email"]));
                    }
                }

                _lector.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _cnn.Close();
            }

            return _ListaPer;
        }
    }
}