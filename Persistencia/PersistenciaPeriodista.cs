using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    public class PersistenciaPeriodista:IPersistenciaPeriodista
    {
        //Singleton
        private static PersistenciaPeriodista _instancia = null;
        private PersistenciaPeriodista() { }
        public static PersistenciaPeriodista GetInstance()
        {
            if (_instancia == null)
                _instancia = new PersistenciaPeriodista();
            return _instancia;
        }
        public void AltaPer(Periodista unPer)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand _Comando = new SqlCommand("CrearPeriodista", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            _Comando.Parameters.AddWithValue("@Cedula", unPer.CI);
            _Comando.Parameters.AddWithValue("@Nombre", unPer.NombrePer);
            _Comando.Parameters.AddWithValue("@Email", unPer.Mail);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;
            _Comando.Parameters.Add(_Retorno);

            try
            {
                _Conexion.Open();
                _Comando.ExecuteNonQuery();

                int oAfectados = (int)_Comando.Parameters["@Retorno"].Value;

                if (oAfectados == -1)
                    throw new Exception("Ya Existe el Periodista");
                else if (oAfectados == -2)
                    throw new Exception("Error");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _Conexion.Close();
            }
        }
        public void ModificarPer(Periodista unPer)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand _Comando = new SqlCommand("ModificarPeriodista", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            _Comando.Parameters.AddWithValue("@Cedula", unPer.CI);
            _Comando.Parameters.AddWithValue("@Nombre", unPer.NombrePer);
            _Comando.Parameters.AddWithValue("@Email", unPer.Mail);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;
            _Comando.Parameters.Add(_Retorno);

            try
            {
                _Conexion.Open();
                _Comando.ExecuteNonQuery();

                int _oAfectados = (int)_Comando.Parameters["@Retorno"].Value;

                if (_oAfectados == -1)
                    throw new Exception("No Existe el periodista o está inactivo");
                else if (_oAfectados == -2)
                    throw new Exception("Error no especificado");

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _Conexion.Close();
            }
        }

        public void EliminarPer(Periodista unPer)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);

            SqlCommand _Comando = new SqlCommand("EliminarPeriodista", _cnn);
            _Comando.CommandType = CommandType.StoredProcedure;
            _Comando.Parameters.AddWithValue("@Cedula", unPer.CI);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;
            _Comando.Parameters.Add(_Retorno);

            try
            {
                _cnn.Open();
                _Comando.ExecuteNonQuery();
                if ((int)_Retorno.Value == -1)
                    throw new Exception("Cedula no existe");
                else if ((int)_Retorno.Value == -2)
                    throw new Exception("Error sin especificar");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
        }
        
        public Periodista BuscarPer(string CI)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);
            Periodista _unPer = null;

            SqlCommand _comando = new SqlCommand("BuscarPeriodistaActivo", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Cedula", CI);

            try
            {
                _cnn.Open();
                SqlDataReader _Reader = _comando.ExecuteReader();
                if (_Reader.HasRows)
                {
                    _Reader.Read();
                    _unPer = new Periodista(_Reader["Cedula"].ToString(), _Reader["Nombre"].ToString(), _Reader["Email"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
            return _unPer;
        }
        public List<Periodista> ListarPer()
        {
            List<Periodista> _lista = new List<Periodista>();
            SqlDataReader _Reader;

            SqlConnection _Conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand _Comando = new SqlCommand("ListadoSoloPeriodistas", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _Conexion.Open();
                _Reader = _Comando.ExecuteReader();

                if (_Reader.HasRows)
                {
                    while (_Reader.Read())
                    {
                        Periodista Per = new Periodista(_Reader["Cedula"].ToString(), _Reader["Nombre"].ToString(), _Reader["Email"].ToString());
                        _lista.Add(Per);
                    }
                }

                _Reader.Close();

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }
            finally
            {
                _Conexion.Close();
            }

            return _lista;
        }

        public List<Periodista> ListarTodosLosPeriodistas()
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);
            List<Periodista> _lista = new List<Periodista>();
            Periodista _unPer = null;

            SqlCommand _comando = new SqlCommand("TodosLosPers", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        //busco la noticia
                        Noticia _unaNot = null;
                        _unaNot = PersistenciaNacional.GetInstancia().Buscar((string)_lector["Codigo"]);
                        if (_unaNot == null)
                            _unaNot = PersistenciaInternacional.GetInstancia().Buscar((string)_lector["Codigo"]);

                        //creo el movimiento
                        _unPer = new Periodista(_lector["Cedula"].ToString(), _lector["Nombre"].ToString(), _lector["Email"].ToString());
                        _lista.Add(_unPer);
                    }
                }
                _lector.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
            return _lista;
        }

    }

}




