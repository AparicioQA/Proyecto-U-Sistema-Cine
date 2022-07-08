using MovieCenter.Connection;
using MovieCenter.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace MovieCenter.Logic
{
    public class SalaBL
    {
        public List<Sala> ConsultarSalas(ref string error)
        {
            List<Sala> lista = new List<Sala>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Salas";


                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Sala sala = new Sala();
                    sala.Id = Convert.ToInt32(dr["id_Sala"]);
                    sala.Tipo = new TipoSala();
                    sala.Tipo.Id = Convert.ToInt32(dr["tipo"]);
                    sala.Tipo.Clasificacion = dr["clasificacion"].ToString();
                    lista.Add(sala);
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
    }
}