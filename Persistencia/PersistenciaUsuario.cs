using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;


namespace Persistencia
{
    internal class PersistenciaUsuario:IPersistenciaUsuario
    {
        //Singleton
        private static PersistenciaUsuario _instancia = null;
        private PersistenciaUsuario() { }
        public static PersistenciaUsuario Getinstance()
        {
            if (_instancia == null)
                _instancia = new PersistenciaUsuario();

            return _instancia;
        }
        public Usuario Logueo(string UNom, string UPass)
        {

            //Variables
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);
            SqlCommand _comando = new SqlCommand("Logueo", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            Usuario unUsu = null;

            //Parametros
            _comando.Parameters.AddWithValue("@NombreUsuario", UNom);
            _comando.Parameters.AddWithValue("@Contraseña", UPass);

            try
            {
                _cnn.Open();

                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    _lector.Read();

                    string _nomusu = (string)_lector["NombreUsuario"];
                    string _passusu = (string)_lector["Contraseña"];
                    unUsu = new Usuario(_nomusu, _passusu);
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

            return unUsu;
        }
        public void Alta(Usuario unUsuario)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);

            SqlCommand _comando = new SqlCommand("CrearEmpleado", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@NombreUsuario", unUsuario.NombreUsu);
            _comando.Parameters.AddWithValue("@Contraseña", unUsuario.Contraseña);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();
                if ((int)_retorno.Value == -1)
                    throw new Exception("Ya existe el usuario");
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
        public Usuario BuscarUsuario(string NomUsu)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);
            Usuario _unUsuario = null;

            SqlCommand _comando = new SqlCommand("BuscarEmpleado", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@NombreUsuario", NomUsu);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    _unUsuario = new Usuario(NomUsu, (string)_lector["Contraseña"]);
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
            return _unUsuario;
        }
        //public void ModificarUsu(Usuario unUsu)
        //{
        //    SqlConnection _Conexion = new SqlConnection(Conexion.Cnn);
        //    SqlCommand _Comando = new SqlCommand("ModificarUsuario", _Conexion);
        //    _Comando.CommandType = CommandType.StoredProcedure;

        //    _Comando.Parameters.AddWithValue("@NombreUsuario", unUsu.NombreUsu);
        //    _Comando.Parameters.AddWithValue("@Contraseña", unUsu.Contraseña);

        //    SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
        //    _Retorno.Direction = ParameterDirection.ReturnValue;
        //    _Comando.Parameters.Add(_Retorno);

        //    try
        //    {
        //        _Conexion.Open();
        //        _Comando.ExecuteNonQuery();

        //        int oAfectados = (int)_Comando.Parameters["@Retorno"].Value;

        //        if (oAfectados == -1)
        //            throw new Exception("No Existe el Usuario - No se Modifica");

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _Conexion.Close();
        //    }
        //}
        //public void BajaUsu(Usuario unUsu)
        //{
        //    //Comando a ejecutar
        //    SqlConnection _Conexion = new SqlConnection(Conexion.Cnn);
        //    SqlCommand _Comando = new SqlCommand("BajaUsuario", _Conexion);
        //    _Comando.CommandType = CommandType.StoredProcedure;

        //    SqlParameter nombreusu = new SqlParameter("@NombreUsuario", unUsu.NombreUsu);

        //    SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
        //    _Retorno.Direction = ParameterDirection.ReturnValue;

        //    _Comando.Parameters.Add(nombreusu);
        //    _Comando.Parameters.Add(_Retorno);

        //    try
        //    {
        //        _Conexion.Open();
        //        _Comando.ExecuteNonQuery();

        //        int oAfectados = (int)_Comando.Parameters["@Retorno"].Value;

        //        if (oAfectados == -1)
        //            throw new Exception("No existe el Usuario- No se Elimina");
        //        else if (oAfectados == -2)
        //            throw new Exception("Error");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _Conexion.Close();
        //    }
        //}


    }
}

