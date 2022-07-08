using MovieCenter.Connection;
using MovieCenter.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MovieCenter.Logic
{
    public class PromocionBL
    {
        public List<Promocion> ConsultarPromociones(ref string error)
        {
            List<Promocion> lista = new List<Promocion>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Promociones";
                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int id = Convert.ToInt32(dr["id_Promosion"]);
                    string dia = dr["dia"].ToString();
                    decimal precio = Convert.ToDecimal(dr["precio"]);

                    int idSala = Convert.ToInt32(dr["sala"]);
                    string calificacion = dr["clasificacion"].ToString();
                    int capacidad = Convert.ToInt32(dr["capacidad"]);

                    TipoSala sala = new TipoSala(idSala, capacidad, calificacion);
                    Promocion promo = new Promocion(id, precio, dia, sala);

                    lista.Add(promo);
                }

            }
            catch (Exception e)
            {

                error = $"Error al consultar promociones {e.Message}";
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

        private bool existeRegistro(Promocion promo)
        {

            string error = null;
            List<Promocion> lista = ConsultarPromociones(ref error);

            bool condicion = lista.Exists(promocion => promocion.Sala.Id == promo.Sala.Id && promocion.Dia == promo.Dia && promocion.Id != promo.Id);
            return condicion;
        }

        public void RegistrarPromocion(Promocion promocion, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                if (!existeRegistro(promocion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Sp_Insertar_Promociones";

                    cmd.Parameters.Add("@id_Creador", SqlDbType.Int);
                    cmd.Parameters["@id_Creador"].Value = promocion.Usuario;

                    cmd.Parameters.Add("@precio", SqlDbType.Money);
                    cmd.Parameters["@precio"].Value = promocion.Precio;

                    cmd.Parameters.Add("@dia", SqlDbType.VarChar);
                    cmd.Parameters["@dia"].Value = promocion.Dia;

                    cmd.Parameters.Add("@sala", SqlDbType.Int);
                    cmd.Parameters["@sala"].Value = promocion.Sala.Id;
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
                error = "Error general en el proceso InsertarPromocion. Detalle: \n" + ex.Message;
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

        public void ModificarPromocion(Promocion promocion, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                if (!existeRegistro(promocion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Sp_Actualizar_Promocion";

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = promocion.Id;


                    cmd.Parameters.Add("@id_Modificador", SqlDbType.Int);
                    cmd.Parameters["@id_Modificador"].Value = promocion.Usuario;

                    cmd.Parameters.Add("@precio", SqlDbType.Money);
                    cmd.Parameters["@precio"].Value = promocion.Precio;

                    cmd.Parameters.Add("@dia", SqlDbType.VarChar);
                    cmd.Parameters["@dia"].Value = promocion.Dia;

                    cmd.Parameters.Add("@sala", SqlDbType.Int);
                    cmd.Parameters["@sala"].Value = promocion.Sala.Id;
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
                error = "Error general en el proceso ModificarPromocion. Detalle: " + ex.Message;
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

        public void EliminarPromocion(Promocion promocion, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            int result;

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Eliminar_Promocion";

                cmd.Parameters.Add("@promocion", SqlDbType.Int);
                cmd.Parameters["@promocion"].Value = promocion.Id;

                cmd.Parameters.Add("@id_Modificador", SqlDbType.Int);
                cmd.Parameters["@id_Modificador"].Value = promocion.Usuario;

                cmd.Connection = conn;
                conn.Open();
                result = cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                error = "Error general en el proceso EliminarPromocion. Detalle: " + ex.Message;
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