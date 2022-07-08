using MovieCenter.Connection;
using MovieCenter.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MovieCenter.Logic
{
    public class MantenimientoBL
    {
        public List<Mantenimiento> ConsultarMantenimientos(ref string error)
        {
            List<Mantenimiento> lista = new List<Mantenimiento>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Mantenimientos";
                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Mantenimiento mante = new Mantenimiento();
                    mante.Id = Convert.ToInt32(dr["id_Mantenimiento"]);
                    mante.Nombre = dr["nombre"].ToString();

                    lista.Add(mante);
                }

            }
            catch (Exception e)
            {

                error = $"Error al consultar\n {e.Message}";
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