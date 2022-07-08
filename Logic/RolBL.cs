using MovieCenter.Connection;
using MovieCenter.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace MovieCenter.Logic
{
    public class RolBL
    {
        public List<Rol> ConsultarRoles(ref string error)
        {
            List<Rol> lista = new List<Rol>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Roles";
                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Rol rl = new Rol();
                    rl.Id = Convert.ToInt32(dr["id_Rol"]);
                    rl.Nombre = dr["nombre"].ToString();

                    lista.Add(rl);
                }

            }
            catch (Exception e)
            {

                error = $"Error al consultar\n{e.Message}";
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

        private bool existeRegistro(Rol rl)
        {

            string error = null;
            List<Rol> lista = ConsultarRoles(ref error);

            bool condicion = lista.Exists(rol => rol.Nombre.Equals(rl.Nombre) && rol.Id != rl.Id);
            return condicion;
        }

        public void RegistrarRol(Rol rl, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                if (!existeRegistro(rl))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Sp_RegistroRol";

                    cmd.Parameters.Add("@id_Creador", SqlDbType.Int);
                    cmd.Parameters["@id_Creador"].Value = rl.Usuario;

                    cmd.Parameters.Add("@nombre_rol", SqlDbType.VarChar);
                    cmd.Parameters["@nombre_rol"].Value = rl.Nombre;

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
                error = "Error general en el proceso InsertarRol. Detalle: \n" + ex.Message;
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

        public void ModificarRol(Rol rol, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                if (!existeRegistro(rol))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Sp_Actualizar_Rol";

                    cmd.Parameters.Add("@id_Rol", SqlDbType.Int);
                    cmd.Parameters["@id_Rol"].Value = rol.Id;

                    cmd.Parameters.Add("@id_Modificador", SqlDbType.Int);
                    cmd.Parameters["@id_Modificador"].Value = rol.Usuario;

                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar);
                    cmd.Parameters["@nombre"].Value = rol.Nombre;


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
                error = "Error general en el proceso ModificarRol. Detalle: " + ex.Message;
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

        public void EliminarRol(Rol rol, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            int result;

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Eliminar_Rol";

                cmd.Parameters.Add("@id_Rol", SqlDbType.Int);
                cmd.Parameters["@id_Rol"].Value = rol.Id;

                cmd.Parameters.Add("@id_Modificador", SqlDbType.Int);
                cmd.Parameters["@id_Modificador"].Value = rol.Usuario;

                cmd.Connection = conn;
                conn.Open();
                result = cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                error = "Error general en el proceso EliminarRol. Detalle: " + ex.Message;
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