using MovieCenter.Connection;
using MovieCenter.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace MovieCenter.Logic
{
    public class TipoSalaBL
    {
        public List<TipoSala> ConsultarTiposSala(ref string error)
        {
            List<TipoSala> lista = new List<TipoSala>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Tipo_Sala";
                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    int idSala = Convert.ToInt32(dr["id_Sala"]);
                    string calificacion = dr["clasificacion"].ToString();
                    int capacidad = Convert.ToInt32(dr["capacidad"]);
                    TipoSala sala = new TipoSala(idSala, capacidad, calificacion);
                    lista.Add(sala);
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