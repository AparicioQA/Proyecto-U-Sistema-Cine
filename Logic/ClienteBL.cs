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
    public class ClienteBL
    {
        public List<Cliente> ConsultarClientes(ref string error)
        {
            List<Cliente> lista = new List<Cliente>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Cliente";
                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.Id = Convert.ToInt32(dr["cedula"]);
                    cliente.Nombre = dr["nombre"].ToString();
                    cliente.Apellido1 = dr["apellido1"].ToString();
                    cliente.Apellido2 = dr["apellido2"].ToString();
                    cliente.Correo = dr["correo"].ToString();
                    cliente.FechaNacimiento = Convert.ToDateTime(dr["fecha_Nacimiento"]);
                    cliente.Telefono = Convert.ToInt32(dr["telefono"]);
                    cliente.Puntos = Convert.ToInt32(dr["puntos"]);
                    cliente.Canjes = Convert.ToInt32(dr["canjes"]);

                    cliente.UsuarioP = new Usuario();
                    cliente.UsuarioP.Id = Convert.ToInt32(dr["usuario"]);
                    cliente.UsuarioP.NombreUsuario = dr["nombre"].ToString();

                    lista.Add(cliente);
                }

            }
            catch (Exception e)
            {

                error = $"Error al consultar clientes {e.Message}";
            }
            return lista;
        }
        private bool existeRegistro(Cliente client)
        {
            string error = null;
            List<Cliente> lista = ConsultarClientes(ref error);

            bool condicion = lista.Exists(cliente => cliente.Id == client.Id);
            return condicion;
        }

        public void RegistrarCliente(Cliente cliente, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                if (!existeRegistro(cliente))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Sp_Insertar_Cliente";

                    cmd.Parameters.Add("@id_Creado_Por", SqlDbType.Int);
                    cmd.Parameters["@id_Creado_Por"].Value = cliente.User;

                    cmd.Parameters.Add("@usuario", SqlDbType.Int);
                    cmd.Parameters["@usuario"].Value = cliente.UsuarioP.Id;

                    cmd.Parameters.Add("@cedula", SqlDbType.Int);
                    cmd.Parameters["@cedula"].Value = cliente.Id;

                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar);
                    cmd.Parameters["@nombre"].Value = cliente.Nombre;

                    cmd.Parameters.Add("@apellido1", SqlDbType.VarChar);
                    cmd.Parameters["@apellido1"].Value = cliente.Apellido1;

                    cmd.Parameters.Add("@apellido2", SqlDbType.VarChar);
                    cmd.Parameters["@apellido2"].Value = cliente.Apellido2;

                    cmd.Parameters.Add("@correo", SqlDbType.VarChar);
                    cmd.Parameters["@correo"].Value = cliente.Correo;

                    cmd.Parameters.Add("@fecha_Na", SqlDbType.Date);
                    cmd.Parameters["@fecha_Na"].Value = cliente.FechaNacimiento;

                    cmd.Parameters.Add("@telefono", SqlDbType.Int);
                    cmd.Parameters["@telefono"].Value = cliente.Telefono;

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
                error = "Error general en el proceso InsertarCliente. Detalle:" + ex.Message;
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

        public Cliente Login(Usuario user, ref string error)
        {
            List<Usuario> lista = new List<Usuario>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();
            Cliente cliente = new Cliente();
            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Login";

                cmd.Parameters.Add("@usuario", SqlDbType.VarChar);
                cmd.Parameters["@usuario"].Value = user.NombreUsuario;

                cmd.Parameters.Add("@clave", SqlDbType.VarChar);
                cmd.Parameters["@clave"].Value = user.Clave;

                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    int idRol = Convert.ToInt32(dr["Rol"]);
                    string nombre = dr["nombre_Rol"].ToString();

                    TipoRol rol = new TipoRol(idRol, nombre);
                   
                    cliente.Id = Convert.ToInt32(dr["cedula"]);
                    cliente.Nombre = dr["nombre"].ToString();
                    cliente.Apellido1 = dr["apellido1"].ToString();
                    cliente.Apellido2 = dr["apellido2"].ToString();
                    cliente.Correo = dr["correo"].ToString();
                    cliente.FechaNacimiento = Convert.ToDateTime(dr["fecha_Nacimiento"]);
                    cliente.Telefono = Convert.ToInt32(dr["telefono"]);
                    cliente.Puntos = Convert.ToInt32(dr["puntos"]);
                    cliente.Canjes = Convert.ToInt32(dr["canjes"]);

                    cliente.UsuarioP = new Usuario();
                    cliente.UsuarioP.Id = Convert.ToInt32(dr["usuario"]);
                    cliente.UsuarioP.NombreUsuario = dr["nombre_Usuario"].ToString();
                    cliente.UsuarioP.Rol = rol;
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
            return cliente;
        }

        public void ModificarCliente(Cliente cliente, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Modificar_Cliente";

                cmd.Parameters.Add("@cedula", SqlDbType.Int);
                cmd.Parameters["@cedula"].Value = cliente.Id;

                cmd.Parameters.Add("@id_Modificado_Por", SqlDbType.Int);
                    cmd.Parameters["@id_Modificado_Por"].Value = cliente.User;

                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar);
                    cmd.Parameters["@nombre"].Value = cliente.Nombre;

                    cmd.Parameters.Add("@apellido1", SqlDbType.VarChar);
                    cmd.Parameters["@apellido1"].Value = cliente.Apellido1;

                    cmd.Parameters.Add("@apellido2", SqlDbType.VarChar);
                    cmd.Parameters["@apellido2"].Value = cliente.Apellido2;

                    cmd.Parameters.Add("@correo", SqlDbType.VarChar);
                    cmd.Parameters["@correo"].Value = cliente.Correo;

                    cmd.Parameters.Add("@fecha_Na", SqlDbType.Date);
                    cmd.Parameters["@fecha_Na"].Value = cliente.FechaNacimiento;

                    cmd.Parameters.Add("@telefono", SqlDbType.Int);
                    cmd.Parameters["@telefono"].Value = cliente.Telefono;

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
             
            }
            catch (Exception ex)
            {
                error = $"Error general en el proceso ModificarCliente. Detalle: {ex.Message}";
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

        public void EliminarCliente(Cliente cliente, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            int result;

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Eliminar_Cliente";

                cmd.Parameters.Add("@cedula", SqlDbType.Int);
                cmd.Parameters["@cedula"].Value = cliente.Id;

                cmd.Parameters.Add("@id_Modificado_Por", SqlDbType.Int);
                cmd.Parameters["@id_Modificado_Por"].Value = cliente.User;

                cmd.Connection = conn;
                conn.Open();
                result = cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                error = "Error general en el proceso Eliminar. Detalle: " + ex.Message;
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


        public void ActualizarPuntaje(Cliente cliente, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Actualizar_Puntaje";

                cmd.Parameters.Add("@cedula", SqlDbType.Int);
                cmd.Parameters["@cedula"].Value = cliente.Id;

                cmd.Parameters.Add("@puntos", SqlDbType.Int);
                cmd.Parameters["@puntos"].Value = cliente.Puntos;

                cmd.Parameters.Add("@canjes", SqlDbType.Int);
                cmd.Parameters["@canjes"].Value = cliente.Canjes;

                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                error = $"Error general en el proceso ModificarCliente. Detalle: {ex.Message}";
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