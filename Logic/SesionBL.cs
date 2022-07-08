using MovieCenter.Connection;
using MovieCenter.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace MovieCenter.Logic
{
    public class SesionBL
    {
        public List<Horario> ConsultarSesiones(int peli, int sala, ref string error)
        {
            List<Horario> lista = new List<Horario>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Sesion_Sala_Horarios";

                cmd.Parameters.Add("@id_Pelicula", SqlDbType.Int);
                cmd.Parameters["@id_Pelicula"].Value = peli;

                cmd.Parameters.Add("@sala", SqlDbType.Int);
                cmd.Parameters["@sala"].Value = sala;

                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Horario horario = new Horario();
                    horario.Id = Convert.ToInt32(dr["horario"]);
                    horario.Hora = TimeSpan.Parse(dr["hora"].ToString());
                    horario.Estado = Convert.ToBoolean(dr["estado"]);
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

        public void ModificarSesion(int peli, int sala, int horario, bool estado, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Actualizar_Sesion";

                cmd.Parameters.Add("@pelicula", SqlDbType.Int);
                cmd.Parameters["@pelicula"].Value = peli;

                cmd.Parameters.Add("@sala", SqlDbType.Int);
                cmd.Parameters["@sala"].Value = sala;

                cmd.Parameters.Add("@horario", SqlDbType.Int);
                cmd.Parameters["@horario"].Value = horario;

                cmd.Parameters.Add("@estado", SqlDbType.Bit);
                cmd.Parameters["@estado"].Value = estado;
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                error = "Error. Detalle: " + ex.Message;
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

        public List<Horario> ConsultarSesionesHorarios(int peli, int sala, ref string error)
        {
            List<Horario> lista = new List<Horario>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Pelicula_Horarios";

                cmd.Parameters.Add("@id_Pelicula", SqlDbType.Int);
                cmd.Parameters["@id_Pelicula"].Value = peli;

                cmd.Parameters.Add("@tipo_sala", SqlDbType.Int);
                cmd.Parameters["@tipo_sala"].Value = sala;

                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Horario horario = new Horario();
                    horario.Id = Convert.ToInt32(dr["horario"]);
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
        public List<TipoSala> ConsultarSesionesSalas(int peli, ref string error)
        {
            List<TipoSala> lista = new List<TipoSala>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Pelicula_Salas";

                cmd.Parameters.Add("@id_Pelicula", SqlDbType.Int);
                cmd.Parameters["@id_Pelicula"].Value = peli;

                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TipoSala sala = new TipoSala();
                    sala.Id = Convert.ToInt32(dr["id_Sala"]);
                    sala.Clasificacion = dr["clasificacion"].ToString();
                    sala.Capacidad = Convert.ToInt32(dr["capacidad"]);
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

        public Sesion Consultar(int tipoSala, int peli, int horario, ref string error)
        {
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();
            Sesion sesion = new Sesion();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Sesion";

                cmd.Parameters.Add("@pelicula", SqlDbType.Int);
                cmd.Parameters["@pelicula"].Value = peli;

                cmd.Parameters.Add("@tipo", SqlDbType.Int);
                cmd.Parameters["@tipo"].Value = tipoSala;

                cmd.Parameters.Add("@horario", SqlDbType.Int);
                cmd.Parameters["@horario"].Value = horario;


                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    sesion.Id = Convert.ToInt32(dr["sesion"]);
                    sesion.Ventas = Convert.ToInt32(dr["Ventas"]);
                    sesion.SalaId = Convert.ToInt32(dr["sala_Id"]);

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
            return sesion;
        }

    }
}