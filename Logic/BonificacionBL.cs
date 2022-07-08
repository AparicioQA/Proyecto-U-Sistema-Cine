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
    public class BonificacionBL
    {
        public Bonificacion ConsultarBonificacion(ref string error)
        {
            Bonificacion bonificacion = new Bonificacion();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Bonificacion";
                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    
                    bonificacion.Id = Convert.ToInt32(dr["id_Condiciones_Bonificacion"]);
                    bonificacion.Puntos = Convert.ToInt32(dr["puntos"]);
                    bonificacion.Canjes = Convert.ToInt32(dr["canjes"]);

                }

            }
            catch (Exception e)
            {

                error = $"Error al consultar {e.Message}";
            }

            finally
            {
                conn.Close();
                cmd.Parameters.Clear();
                cmd.Dispose();
                conn.Dispose();
                conn = null;
            }
            return bonificacion;
        }

        public void ModificarBonificacion(Bonificacion bonificacion, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
   
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Sp_Modificar_Bonificacion";

                    cmd.Parameters.Add("@id_condiciones", SqlDbType.Int);
                    cmd.Parameters["@id_condiciones"].Value = bonificacion.Id;

                    cmd.Parameters.Add("@id_Modificado_Por", SqlDbType.Int);
                    cmd.Parameters["@id_Modificado_Por"].Value = bonificacion.Usuario;

                    cmd.Parameters.Add("@puntos", SqlDbType.Int);
                    cmd.Parameters["@puntos"].Value = bonificacion.Puntos;

                    cmd.Parameters.Add("@canjes", SqlDbType.Int);
                    cmd.Parameters["@canjes"].Value = bonificacion.Canjes;

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
             
            }
            catch (Exception ex)
            {
                error = "Error general en el proceso ModificarBonificación. Detalle: " + ex.Message;
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