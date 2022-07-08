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
    public class TipoRolBL
    {
        public List<TipoRol> ConsultarTiposRol(ref string error)
        {
            List<TipoRol> lista = new List<TipoRol>();
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

                    int idRol = Convert.ToInt32(dr["id_Rol"]);
                    string nombre = dr["Nombre"].ToString();
                    TipoRol rol = new TipoRol(idRol, nombre);
                    lista.Add(rol);
                }

            }
            catch (Exception e)
            {

                error = $"Ocurrio un error\nDetalle:\n${e}";
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
    }
}