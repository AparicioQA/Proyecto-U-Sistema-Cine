using MovieCenter.Connection;
using MovieCenter.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MovieCenter.Logic
{
    public class PeliculaBL
    {
        public List<Pelicula> ConsultarPeliculas(ref string error)
        {
            List<Pelicula> lista = new List<Pelicula>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Peliculas";


                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Pelicula peli = new Pelicula();
                    peli.Id = Convert.ToInt32(dr["id_Pelicula"]);
                    peli.Nombre = dr["nombre"].ToString();
                    peli.Sinopsis = dr["sinopsis"].ToString();
                    peli.Fecha = Convert.ToDateTime(dr["fecha_Estreno"]);
                    peli.CategoriaP = new Categoria()
                    {
                        Id = Convert.ToInt32(dr["categoria"]),
                        Clasificacion = dr["clasificacion"].ToString()
                    };
                    peli.Imagen = dr["imagen"].ToString();
                    lista.Add(peli);
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

        public Pelicula ConsultarPelicula(Pelicula pelicula, ref string error)
        {

            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();
            Pelicula peli = new Pelicula();
            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Pelicula";

                cmd.Parameters.Add("@pelicula", SqlDbType.Int);
                cmd.Parameters["@pelicula"].Value = pelicula.Id;

                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    peli.Id = Convert.ToInt32(dr["id_Pelicula"]);
                    peli.Nombre = dr["nombre"].ToString();
                    peli.Sinopsis = dr["sinopsis"].ToString();
                    peli.Fecha = Convert.ToDateTime(dr["fecha_Estreno"]);
                    peli.CategoriaP = new Categoria()
                    {
                        Id = Convert.ToInt32(dr["categoria"]),
                        Clasificacion = dr["clasificacion"].ToString()
                    };
                    peli.Imagen = dr["imagen"].ToString();

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
            return peli;
        }
        public void ModificarPelicula(Pelicula peli, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Actualizar_Pelicula";

                cmd.Parameters.Add("@id_Pelicula", SqlDbType.Int);
                cmd.Parameters["@id_Pelicula"].Value = peli.Id;

                cmd.Parameters.Add("@id_Modificador", SqlDbType.Int);
                cmd.Parameters["@id_Modificador"].Value = peli.Usuario;

                cmd.Parameters.Add("@nombre", SqlDbType.VarChar);
                cmd.Parameters["@nombre"].Value = peli.Nombre;

                cmd.Parameters.Add("@sinopsis", SqlDbType.VarChar);
                cmd.Parameters["@sinopsis"].Value = peli.Sinopsis;

                cmd.Parameters.Add("@img", SqlDbType.VarChar);
                cmd.Parameters["@img"].Value = peli.Imagen;

                cmd.Parameters.Add("@fecha", SqlDbType.Date);
                cmd.Parameters["@fecha"].Value = peli.Fecha;

                cmd.Parameters.Add("@categoria", SqlDbType.Int);
                cmd.Parameters["@categoria"].Value = peli.CategoriaP.Id;
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                error = "Error general en el proceso Modificar. Detalle: " + ex.Message;
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

        public void InsertarPelicula(Pelicula peli, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Insertar_Pelicula";

                cmd.Parameters.Add("@id_Creador", SqlDbType.Int);
                cmd.Parameters["@id_Creador"].Value = peli.Usuario;

                cmd.Parameters.Add("@nombre", SqlDbType.VarChar);
                cmd.Parameters["@nombre"].Value = peli.Nombre;

                cmd.Parameters.Add("@sinopsis", SqlDbType.VarChar);
                cmd.Parameters["@sinopsis"].Value = peli.Sinopsis;

                cmd.Parameters.Add("@img", SqlDbType.VarChar);
                cmd.Parameters["@img"].Value = peli.Imagen;

                cmd.Parameters.Add("@fecha", SqlDbType.Date);
                cmd.Parameters["@fecha"].Value = peli.Fecha;

                cmd.Parameters.Add("@categoria", SqlDbType.Int);
                cmd.Parameters["@categoria"].Value = peli.CategoriaP.Id;
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                error = "Error general en el proceso Modificar. Detalle: " + ex.Message;
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
        public void EliminarPelicula(Pelicula peli, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            int result;

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Eliminar_Pelicula";

                cmd.Parameters.Add("@id_Pelicula", SqlDbType.Int);
                cmd.Parameters["@id_Pelicula"].Value = peli.Id;

                cmd.Parameters.Add("@id_Modificador", SqlDbType.Int);
                cmd.Parameters["@id_Modificador"].Value = peli.Usuario;

                cmd.Connection = conn;
                conn.Open();
                result = cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                error = "Error general en el proceso Eliminar. Detalle: " + ex.Message;
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