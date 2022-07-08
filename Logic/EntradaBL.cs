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
    public class EntradaBL
    {
        public List<Entrada> ConsultarEntradas(ref string error)
        {
            List<Entrada> lista = new List<Entrada>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Entradas";
                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int id = Convert.ToInt32(dr["id_Entrada"]);
                    decimal precioGen = Convert.ToDecimal(dr["precio_General"]);
                    decimal precioNino = Convert.ToDecimal(dr["precio_Nino"]);
                    decimal precioMayor = Convert.ToDecimal(dr["precio_Mayores"]);

                    int idSala = Convert.ToInt32(dr["sala"]);
                    string calificacion = dr["clasificacion"].ToString();
                    int capacidad = Convert.ToInt32(dr["capacidad"]);

                    TipoSala sala = new TipoSala(idSala, capacidad, calificacion);
                    Entrada entrada = new Entrada(id, precioGen, precioNino, precioMayor, sala);
                    lista.Add(entrada);
                }

            }
            catch (Exception e)
            {

                error = $"Error al consultar entradas {e.Message}";
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
        private bool existeRegistro(Entrada entra)
        {

            string error = null;
            List<Entrada> lista = ConsultarEntradas (ref error);

            bool condicion = lista.Exists(entrada => entrada.Sala.Id == entra.Sala.Id  
            && entrada.Id != entra.Id);
            return condicion;
        }
        public void RegistrarEntrada (Entrada entrada, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                if (!existeRegistro(entrada))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Sp_Insertar_Entrada";

                    cmd.Parameters.Add("@id_Creado_Por", SqlDbType.Int);
                    cmd.Parameters["@id_Creado_Por"].Value = entrada.Usuario;

                    cmd.Parameters.Add("@precio_General", SqlDbType.Money);
                    cmd.Parameters["@precio_General"].Value = entrada.PrecioGeneral;

                    cmd.Parameters.Add("@precio_Nino", SqlDbType.Money);
                    cmd.Parameters["@precio_Nino"].Value = entrada.PrecioNino;

                    cmd.Parameters.Add("@precio_Mayores", SqlDbType.Money);
                    cmd.Parameters["@precio_Mayores"].Value = entrada.PrecioMayores;

                    cmd.Parameters.Add("@sala", SqlDbType.Int);
                    cmd.Parameters["@sala"].Value = entrada.Sala.Id;
                    cmd.Connection = conn;
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    error = "Registro ya existe";
                }


            }
            catch (Exception ex)
            {
                error = "Error general en el proceso InsertarEntrada. Detalle:" + ex.Message;
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
        public void ModificarEntrada(Entrada entrada, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                if (!existeRegistro(entrada))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Sp_Modificar_Entrada";

                    cmd.Parameters.Add("@id_entrada", SqlDbType.Int);
                    cmd.Parameters["@id_entrada"].Value = entrada.Id;

                    cmd.Parameters.Add("@id_Modificado_Por", SqlDbType.Int);
                    cmd.Parameters["@id_Modificado_Por"].Value = entrada.Usuario;

                    cmd.Parameters.Add("@precio_General", SqlDbType.Money);
                    cmd.Parameters["@precio_General"].Value = entrada.PrecioGeneral;

                    cmd.Parameters.Add("@precio_Nino", SqlDbType.Money);
                    cmd.Parameters["@precio_Nino"].Value = entrada.PrecioNino;

                    cmd.Parameters.Add("@precio_Mayores", SqlDbType.Money);
                    cmd.Parameters["@precio_Mayores"].Value = entrada.PrecioMayores;

                    cmd.Parameters.Add("@sala", SqlDbType.Int);
                    cmd.Parameters["@sala"].Value = entrada.Sala.Id;
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    error = "Registro ya existe";
                }
            }
            catch (Exception ex)
            {
                error = "Error general en el proceso ModificarEntrada. Detalle: " + ex.Message;
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
        public void EliminarEntrada(Entrada entrada, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            int result;

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Eliminar_Entrada";

                cmd.Parameters.Add("@id_entrada", SqlDbType.Int);
                cmd.Parameters["@id_entrada"].Value = entrada.Id;

                cmd.Parameters.Add("@id_Modificado_Por", SqlDbType.Int);
                cmd.Parameters["@id_Modificado_Por"].Value = entrada.Usuario;

                cmd.Connection = conn;
                conn.Open();
                result = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                error = "Error general en el proceso EliminarEntrada. Detalle: " + ex.Message;
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
