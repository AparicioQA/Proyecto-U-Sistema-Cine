using MovieCenter.Connection;
using MovieCenter.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace MovieCenter.Logic
{
    public class VentaBL
    {
        public List<Butaca> ConsultarButacasVendidas(Sesion sesion, ref string error)
        {
            List<Butaca> lista = new List<Butaca>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Ventas";

                cmd.Parameters.Add("@sesion", SqlDbType.Int);
                cmd.Parameters["@sesion"].Value = sesion.Id;

                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Butaca butaca = new Butaca();
                    butaca.Id = Convert.ToInt32(dr["butaca"]);
                    lista.Add(butaca);
                }

            }
            catch (Exception e)
            {

                error = $"Error al consultar{e.Message}";
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

        public void InsertarVenta(Venta venta, ref string error)
        {
            ConexionBD conexion = new ConexionBD();
            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Insertar_Venta";

                cmd.Parameters.Add("@cliente", SqlDbType.Int);
                cmd.Parameters["@cliente"].Value = venta.Cliente;

                cmd.Parameters.Add("@sesion", SqlDbType.Int);
                cmd.Parameters["@sesion"].Value = venta.Sesion;

                cmd.Parameters.Add("@butaca", SqlDbType.Int);
                cmd.Parameters["@butaca"].Value = venta.Butaca;

                cmd.Parameters.Add("@precio", SqlDbType.Money);
                cmd.Parameters["@precio"].Value = venta.Precio;

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
    }
}