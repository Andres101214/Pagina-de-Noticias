using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class PersistenciaInternacional:IPersistenciaInternacional
    {
        //Singleton
        private static PersistenciaInternacional _instancia = null;
        private PersistenciaInternacional() { }
        public static PersistenciaInternacional GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaInternacional();

            return _instancia;
        }

        public void AltaInter(Internacional unaInter)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);

            SqlCommand _comando = new SqlCommand("CrearNoticiaInternacional ", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Codigo", unaInter.CodInt);
            _comando.Parameters.AddWithValue("@Pais", unaInter.Pais);
            _comando.Parameters.AddWithValue("@FechaPublicacion", unaInter.FechaPN);
            _comando.Parameters.AddWithValue("@Importancia", unaInter.Importancia);
            _comando.Parameters.AddWithValue("@Titulo", unaInter.Titulo);
            _comando.Parameters.AddWithValue("@Cuerpo", unaInter.CuerpoN);
            _comando.Parameters.AddWithValue("@NombreUsuario", unaInter.Usuar.NombreUsu);
        
          

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

                //Ejecuto comando de alta de la noticias inter en la transaccion
                _comando.Transaction = _miTransaccion;
                _comando.ExecuteNonQuery();

                //verifico si hay errores
                int _Codigo = Convert.ToInt32(_ParmRetorno.Value);
                if (_Codigo == -1)
                    throw new Exception("Noticia ya existente");
                else if (_Codigo == -2)
                    throw new Exception("Usuario invalido");
                else if (_Codigo == -3)
                    throw new Exception("Error no especificado");

                //Si llego aca es porque pude dar de alta a la noticia internacional

                //Genero alta
                foreach (Periodista unPer in unaInter.ListaPer)
                {
                    PersistenciaListPeriodista.Alta(unPer, unaInter.CodInt, _miTransaccion);
                }

                //Si llegue hasta aca es porque no hubo problemas 
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

        public void ModificarInter(Internacional unaInter)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand _Comando = new SqlCommand("ModificarNoticiaInternacional", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            _Comando.Parameters.AddWithValue("@Codigo", unaInter.CodInt);
            _Comando.Parameters.AddWithValue("@Pais", unaInter.Pais);
            _Comando.Parameters.AddWithValue("@FechaPublicacion", unaInter.FechaPN);
            _Comando.Parameters.AddWithValue("@Importancia", unaInter.Importancia);
            _Comando.Parameters.AddWithValue("@Titulo", unaInter.Titulo);
            _Comando.Parameters.AddWithValue("@Cuerpo", unaInter.CuerpoN);
            _Comando.Parameters.AddWithValue("@NombreUsuario", unaInter.Usuar.NombreUsu);



            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;
            _Comando.Parameters.Add(_Retorno);

            SqlTransaction _miTransaccion = null;

            try
            {
                _Conexion.Open();
                _miTransaccion = _Conexion.BeginTransaction();
                PersistenciaListPeriodista.EliminarPerNoticia(unaInter, _miTransaccion);

                _Comando.Transaction = _miTransaccion;
                _Comando.ExecuteNonQuery();

                int oAfectados = (int)_Comando.Parameters["@Retorno"].Value;

                if (oAfectados == -1)
                    throw new Exception("Ya Existe el código");
                else if (oAfectados == -2)
                    throw new Exception("Error en TRN");

                foreach (Periodista unPer in unaInter.ListaPer)
                {
                    PersistenciaListPeriodista.Alta(unPer, unaInter.CodInt, _miTransaccion);
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
        public Internacional Buscar(string CodInt)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion.Cnn);
            Internacional unaNot = null;

            SqlCommand _Comando = new SqlCommand("BuscarNoticiaInternacional", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;
            _Comando.Parameters.AddWithValue("@Codigo", CodInt);



            try
            {
                _Conexion.Open();
                SqlDataReader _Reader = _Comando.ExecuteReader();
                if (_Reader.HasRows)
                {
                    _Reader.Read();
                    unaNot = new Internacional(_Reader["Codigo"].ToString(), _Reader["Pais"].ToString(), Convert.ToDateTime(_Reader["FechaPublicacion"]), (int)_Reader["Importancia"], _Reader["Titulo"].ToString(), _Reader["Cuerpo"].ToString(), PersistenciaListPeriodista.CargoPeriodistas(_Reader["Codigo"].ToString()), PersistenciaUsuario.Getinstance().BuscarUsuario(_Reader["NombreUsuario"].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }
            finally
            {
                _Conexion.Close();
            }

            return unaNot;

        }
        public List<Internacional> ListarInter()
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);
            Internacional Not = null;
            List<Internacional> _lista = new List<Internacional>();
            List<Noticia> _listaN = new List<Noticia>();

            SqlCommand _comando = new SqlCommand("ListarInternacionalesPublicadas", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _Reader = _comando.ExecuteReader();
                if (_Reader.HasRows)
                {
                    while (_Reader.Read())
                    {
                        Not = new Internacional(_Reader["Codigo"].ToString(), _Reader["Pais"].ToString(), Convert.ToDateTime(_Reader["FechaPublicacion"]), (int)_Reader["Importancia"], _Reader["Titulo"].ToString(), _Reader["Cuerpo"].ToString(), PersistenciaListPeriodista.CargoPeriodistas(_Reader["Codigo"].ToString()), PersistenciaUsuario.Getinstance().BuscarUsuario(_Reader["NombreUsuario"].ToString()));
                        _lista.Add(Not);
                        _listaN.Add(Not);
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
        public List<Internacional> ListarInter5Dias()
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);
            Internacional Not = null;
            List<Internacional> _lista = new List<Internacional>();
            List<Noticia> _listaN = new List<Noticia>();

            SqlCommand _comando = new SqlCommand("ListarInternacionales5Dias", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _Reader = _comando.ExecuteReader();
                if (_Reader.HasRows)
                {
                    while (_Reader.Read())
                    {
                        Not = new Internacional(_Reader["Codigo"].ToString(), _Reader["Pais"].ToString(), Convert.ToDateTime(_Reader["FechaPublicacion"]), (int)_Reader["Importancia"], _Reader["Titulo"].ToString(), _Reader["Cuerpo"].ToString(), PersistenciaListPeriodista.CargoPeriodistas(_Reader["Codigo"].ToString()), PersistenciaUsuario.Getinstance().BuscarUsuario(_Reader["NombreUsuario"].ToString()));
                        _lista.Add(Not);
                        _listaN.Add(Not);
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
