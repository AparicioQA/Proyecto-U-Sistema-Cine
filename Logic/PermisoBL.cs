using MovieCenter.Connection;
using MovieCenter.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace MovieCenter.Logic
{
    public class PermisoBL
    {
        public List<Permiso> ConsultarPermisos(Mantenimiento mante, Rol rol, ref string error)
        {
            List<Permiso> lista = new List<Permiso>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Permisos_Rol";

                cmd.Parameters.Add("@rol", SqlDbType.Int);
                cmd.Parameters["@rol"].Value = rol.Id;

                cmd.Parameters.Add("@mantenimiento", SqlDbType.Int);
                cmd.Parameters["@mantenimiento"].Value = mante.Id;

                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Permiso permiso = new Permiso();

                    permiso.Id = Convert.ToInt32(dr["id_rol_permiso"]);
                    permiso.Estado = Convert.ToBoolean(dr["estado"]);
                    permiso.AccionP = new Accion();
                    permiso.AccionP.Id = Convert.ToInt32(dr["id_Accion"]);
                    permiso.AccionP.Nombre = dr["Accion"].ToString();
                    lista.Add(permiso);
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

        public List<Permiso> ConsultarPermisos2(Mantenimiento mante, TipoRol rol, ref string error)
        {
            List<Permiso> lista = new List<Permiso>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Permisos_Rol";

                cmd.Parameters.Add("@rol", SqlDbType.Int);
                cmd.Parameters["@rol"].Value = rol.Id;

                cmd.Parameters.Add("@mantenimiento", SqlDbType.Int);
                cmd.Parameters["@mantenimiento"].Value = mante.Id;

                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Permiso permiso = new Permiso();

                    permiso.Id = Convert.ToInt32(dr["id_rol_permiso"]);
                    permiso.Estado = Convert.ToBoolean(dr["estado"]);
                    permiso.AccionP = new Accion();
                    permiso.AccionP.Id = Convert.ToInt32(dr["id_Accion"]);
                    permiso.AccionP.Nombre = dr["Accion"].ToString();
                    lista.Add(permiso);
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


        public void ModificarPermiso(Permiso permiso, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Actualizar_Permisos_Rol";

                cmd.Parameters.Add("@id_Permiso", SqlDbType.Int);
                cmd.Parameters["@id_Permiso"].Value = permiso.Id;

                cmd.Parameters.Add("@id_Modificador", SqlDbType.Int);
                cmd.Parameters["@id_Modificador"].Value = permiso.Usuario;

                cmd.Parameters.Add("@estado", SqlDbType.Bit);
                cmd.Parameters["@estado"].Value = permiso.Estado;


                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                error = "Error general en el proceso ModificarPermiso. Detalle: " + ex.Message;
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