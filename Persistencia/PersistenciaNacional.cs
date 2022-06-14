using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;


namespace Persistencia
{
    internal class PersistenciaNacional:IPersistenciaNacional
    {
        //Singleton
        private static PersistenciaNacional _instancia = null;
        private PersistenciaNacional() { }
        public static PersistenciaNacional GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaNacional();

            return _instancia;
        }

        public void AltaNac(Nacional unaNac)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);

            SqlCommand _comando = new SqlCommand("CrearNoticiaNacional ", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Codigo", unaNac.CodInt);
            _comando.Parameters.AddWithValue("@CodigoSecc", unaNac.Secc.CodSec);
            _comando.Parameters.AddWithValue("@FechaPublicacion", unaNac.FechaPN);
            _comando.Parameters.AddWithValue("@Importancia", unaNac.Importancia);
            _comando.Parameters.AddWithValue("@Titulo", unaNac.Titulo);
            _comando.Parameters.AddWithValue("@Cuerpo", unaNac.CuerpoN);
            _comando.Parameters.AddWithValue("@NombreUsuario", unaNac.Usuar.NombreUsu);
            


            SqlParameter _ParmRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _ParmRetorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_ParmRetorno);

            SqlTransaction _miTransaccion = null;

            try
            {
                //Conecto a la bd
                _cnn.Open();

                //Determino que voy a trabajar en una unica transaccion
                _miTransaccion = _cnn.BeginTransaction();

                //Ejecuto comando de alta
                _comando.Transaction = _miTransaccion;
                _comando.ExecuteNonQuery();

                //verifico si hay errores
                int _Codigo = Convert.ToInt32(_ParmRetorno.Value);
                if (_Codigo == -1)
                    throw new Exception("Noticia ya existente");
                else if (_Codigo == -2)
                    throw new Exception("Usuario invalido");
                else if (_Codigo == -3)
                    throw new Exception("Seccion incorrecta");
                else if (_Codigo == -4)
                    throw new Exception("Error no especificado");

                //si llego aca es pq pude dar de alta a la noticia

                //genero alta
                foreach (Periodista unPer in unaNac.ListaPer)
                {
                    PersistenciaListPeriodista.Alta(unPer, unaNac.CodInt, _miTransaccion);
                }

                //si llegue aca es pq no hubo problemas
                _miTransaccion.Commit();
            }
            catch (Exception ex)
            {
                _miTransaccion.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }

        }

        public void ModificarNac(Nacional unaNac)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand _Comando = new SqlCommand("ModificarNoticiaNacional", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            _Comando.Parameters.AddWithValue("@Codigo", unaNac.CodInt);
            _Comando.Parameters.AddWithValue("@CodigoSecc", unaNac.Secc.CodSec);
            _Comando.Parameters.AddWithValue("@FechaPublicacion", unaNac.FechaPN);
            _Comando.Parameters.AddWithValue("@Importancia", unaNac.Importancia);
            _Comando.Parameters.AddWithValue("@Titulo", unaNac.Titulo);
            _Comando.Parameters.AddWithValue("@Cuerpo", unaNac.CuerpoN);
            _Comando.Parameters.AddWithValue("@NombreUsuario", unaNac.Usuar.NombreUsu);



            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;
            _Comando.Parameters.Add(_Retorno);

            SqlTransaction _miTransaccion = null;
            try
            {
                _Conexion.Open();
                _miTransaccion = _Conexion.BeginTransaction();
                PersistenciaListPeriodista.EliminarPerNoticia(unaNac, _miTransaccion);

                _Comando.Transaction = _miTransaccion;
                _Comando.ExecuteNonQuery();

                int oAfectados = (int)_Comando.Parameters["@Retorno"].Value;

                if (oAfectados == -1)
                    throw new Exception("Ya Existe el código");
                else if (oAfectados == -2)
                    throw new Exception("No existe la sección");
                else if (oAfectados == -3)
                    throw new Exception("Codigo incorrecto o Seccion dada de baja");


                foreach (Periodista unPer in unaNac.ListaPer)
                {
                    PersistenciaListPeriodista.Alta(unPer, unaNac.CodInt, _miTransaccion);
                }

                _miTransaccion.Commit();
            }

            catch (Exception ex)
            {
                _miTransaccion.Rollback();
                throw ex;
            }
            finally
            {
                _Conexion.Close();
            }
        }
        public Nacional Buscar(string CodInt)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);
            Nacional unaNot = null;

            SqlCommand _Comando = new SqlCommand("BuscarNoticiaNacional", _cnn);
            _Comando.CommandType = CommandType.StoredProcedure;
            _Comando.Parameters.AddWithValue("@Codigo", CodInt);



            try
            {
                _cnn.Open();
                SqlDataReader _Reader = _Comando.ExecuteReader();
                if (_Reader.HasRows)
                {
                    _Reader.Read();
                    unaNot = new Nacional(_Reader["Codigo"].ToString(), PersistenciaSeccion.Getinstance().BuscarSec(_Reader["CodigoSecc"].ToString()), Convert.ToDateTime(_Reader["FechaPublicacion"]), (int)_Reader["Importancia"], _Reader["Titulo"].ToString(), _Reader["Cuerpo"].ToString(), PersistenciaListPeriodista.CargoPeriodistas(_Reader["Codigo"].ToString()), PersistenciaUsuario.Getinstance().BuscarUsuario(_Reader["NombreUsuario"].ToString()));
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }
            finally
            {
                _cnn.Close();
            }

            return unaNot;
        }
        public List<Nacional> ListarNac()
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);
            Nacional Not = null;
            List<Nacional> _lista = new List<Nacional>();

            SqlCommand _comando = new SqlCommand("ListarNacionalesPublicadas", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _Reader = _comando.ExecuteReader();
                if (_Reader.HasRows)
                {
                    while (_Reader.Read())
                    {
                         Not = new Nacional(_Reader["Codigo"].ToString(), PersistenciaSeccion.Getinstance().BuscarSec(_Reader["CodigoSecc"].ToString()), Convert.ToDateTime(_Reader["FechaPublicacion"]), (int)_Reader["Importancia"], _Reader["Titulo"].ToString(), _Reader["Cuerpo"].ToString(), PersistenciaListPeriodista.CargoPeriodistas(_Reader["Codigo"].ToString()), PersistenciaUsuario.Getinstance().BuscarUsuario(_Reader["NombreUsuario"].ToString()));
                        _lista.Add(Not);
                    }
                }
                _Reader.Close();
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
        public List<Nacional> ListarNac5Dias()
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);
            Nacional Not = null;
            List<Nacional> _lista = new List<Nacional>();

            SqlCommand _comando = new SqlCommand("ListarNacionales5Dias", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _Reader = _comando.ExecuteReader();
                if (_Reader.HasRows)
                {
                    while (_Reader.Read())
                    {
                        Not = new Nacional(_Reader["Codigo"].ToString(), PersistenciaSeccion.Getinstance().BuscarSec(_Reader["CodigoSecc"].ToString()), Convert.ToDateTime(_Reader["FechaPublicacion"]), (int)_Reader["Importancia"], _Reader["Titulo"].ToString(), _Reader["Cuerpo"].ToString(), PersistenciaListPeriodista.CargoPeriodistas(_Reader["Codigo"].ToString()), PersistenciaUsuario.Getinstance().BuscarUsuario(_Reader["NombreUsuario"].ToString()));
                        _lista.Add(Not);
                    }
                }
                _Reader.Close();
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
