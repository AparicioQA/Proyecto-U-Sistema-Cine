using MovieCenter.Connection;
using MovieCenter.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace MovieCenter.Logic
{
    public class HorarioBL
    {
        public List<Horario> ConsultarHorarios(ref string error)
        {
            List<Horario> lista = new List<Horario>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Horario";


                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Horario horario = new Horario();
                    horario.Id = Convert.ToInt32(dr["id_Horario"]);
                    horario.Hora = TimeSpan.Parse(dr["hora"].ToString());

                    lista.Add(horario);
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