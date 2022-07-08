using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieCenter.Data;
using MovieCenter.Connection;
using System.Data;
using System.Data.SqlClient;

namespace MovieCenter.Logic
{
    public class UsuarioBL
    {
        public List<Usuario> ConsultarUsuarios(ref string error)
        {
            List<Usuario> lista = new List<Usuario>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Usuario";
                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int idRol = Convert.ToInt32(dr["Rol"]);
                    string nombre = dr["nombre"].ToString();

                    TipoRol rol = new TipoRol(idRol, nombre);
                    Usuario usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(dr["id_Usuario"]);
                    usuario.NombreUsuario = dr["nombre_Usuario"].ToString();
                    usuario.Rol = rol;
                    lista.Add(usuario);
                }

            }
            catch (Exception e)
            {

                error = $"Error al consultar usuario {e.Message}";
            }

            finally
            {
                conn.Close();
                cmd.Parameters.Clear();
                cmd.Dispose();
                conn.Dispose();
                conn = null;
            }
            return lista;
        }


        public List<Usuario> ConsultarUsuariosSinCliente(ref string error)
        {
            List<Usuario> lista = new List<Usuario>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_UsuariosSinCliente";
                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(dr["id_Usuario"]);
                    usuario.NombreUsuario = dr["nombre_Usuario"].ToString();
                
                    lista.Add(usuario);
                }

            }
            catch (Exception e)
            {

                error = $"Error al consultar usuario {e.Message}";
            }

            finally
            {
                conn.Close();
                cmd.Parameters.Clear();
                cmd.Dispose();
                conn.Dispose();
                conn = null;
            }
            return lista;
        }

        private bool existeRegistro (Usuario usua)
        {

            string error = null;
            List<Usuario> lista = ConsultarUsuarios (ref error);

            bool condicion = lista.Exists( usuario => usuario.NombreUsuario == usua.NombreUsuario && usuario.Id != usua.Id);
            return condicion;
        }

        public void RegistrarUsuario(Usuario usuario, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                if (!existeRegistro(usuario))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Sp_Insertar_Usuario";

                    cmd.Parameters.Add("@id_Creado_Por", SqlDbType.Int);
                    cmd.Parameters["@id_Creado_Por"].Value = usuario.User;

                    cmd.Parameters.Add("@nombre_Usuario", SqlDbType.VarChar);
                    cmd.Parameters["@nombre_Usuario"].Value = usuario.NombreUsuario;

                    cmd.Parameters.Add("@rol", SqlDbType.Int);
                    cmd.Parameters["@rol"].Value = usuario.Rol.Id;

                    cmd.Parameters.Add("@clave", SqlDbType.VarChar);
                    cmd.Parameters["@clave"].Value = usuario.Clave;

                    cmd.Connection = conn;
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    error = "Registro ya existe";
                }


            }
            catch (Exception ex)
            {
                error = "Error general en el proceso InsertarUsuario. Detalle:" + ex.Message;
            }
            finally
            {
                conn.Close();
                cmd.Parameters.Clear();
                cmd.Dispose();
                conn.Dispose();
                conn = null;
            }
        }


        public void ModificarUsuario(Usuario usuario, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                if (!existeRegistro(usuario))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Sp_Modificar_Usuario";

                    cmd.Parameters.Add("@id_Usuario", SqlDbType.Int);
                    cmd.Parameters["@id_Usuario"].Value = usuario.Id;

                    cmd.Parameters.Add("@id_Modificado_Por", SqlDbType.Int);
                    cmd.Parameters["@id_Modificado_Por"].Value = usuario.User;

                    cmd.Parameters.Add("@nombre_Usuario", SqlDbType.VarChar);
                    cmd.Parameters["@nombre_Usuario"].Value = usuario.NombreUsuario;

                    cmd.Parameters.Add("@rol", SqlDbType.Int);
                    cmd.Parameters["@rol"].Value = usuario.Rol.Id;

                    cmd.Parameters.Add("@clave", SqlDbType.VarChar);
                    cmd.Parameters["@clave"].Value = usuario.Clave;

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    error = "Registro ya existe";
                }
            }
            catch (Exception ex)
            {
                error = "Error general en el proceso ModificarUsuario. Detalle: " + ex.Message;
            }
            finally
            {
                conn.Close();
                cmd.Parameters.Clear();
                cmd.Dispose();
                conn.Dispose();
                conn = null;
            }
        }
        public void EliminarUsuario(Usuario usuario, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            int result;

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Eliminar_Usuario";

                cmd.Parameters.Add("@id_Usuario", SqlDbType.Int);
                cmd.Parameters["@id_Usuario"].Value = usuario.Id;

                cmd.Parameters.Add("@id_Modificado_Por", SqlDbType.Int);
                cmd.Parameters["@id_Modificado_Por"].Value = usuario.User;

                cmd.Connection = conn;
                conn.Open();
                result = cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                error = "Error general en el proceso EliminarUsuario. Detalle: " + ex.Message;
            }
            finally
            {
                conn.Close();
                cmd.Parameters.Clear();
                cmd.Dispose();
                conn.Dispose();
                conn = null;
            }
        }
    }
}