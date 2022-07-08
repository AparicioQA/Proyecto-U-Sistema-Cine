using MovieCenter.Connection;
using MovieCenter.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace MovieCenter.Logic
{
    public class ButacaBL
    {

        public List<Butaca> ConsultarButacas(TipoSala sala, ref string error)
        {
            List<Butaca> lista = new List<Butaca>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Butacas_Sala";

                cmd.Parameters.Add("@capacidad", SqlDbType.Int);
                cmd.Parameters["@capacidad"].Value = sala.Capacidad;

                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Butaca butaca = new Butaca();
                    butaca.Id = Convert.ToInt32(dr["id_asiento"]);
                    butaca.Columna = Convert.ToInt32(dr["columna"]);
                    butaca.Fila = Convert.ToChar(dr["fila"]);

                    lista.Add(butaca);
                }

            }
            catch (Exception e)
            {

                error = $"Error al consultar: {e.Message}";
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