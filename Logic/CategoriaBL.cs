using MovieCenter.Connection;
using MovieCenter.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace MovieCenter.Logic
{
    public class CategoriaBL
    {
        public List<Categoria> ConsultarCategorias(ref string error)
        {
            List<Categoria> lista = new List<Categoria>();
            ConexionBD conexion = new ConexionBD();

            SqlConnection conn = new SqlConnection(conexion.ObtenerCadenaConexion());
            SqlCommand cmd = new SqlCommand();

            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Consultar_Categoria";


                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    Categoria categoria = new Categoria();
                    categoria.Id = Convert.ToInt32(dr["id_Categoria"]);
                    categoria.Clasificacion = dr["clasificacion"].ToString();
                    lista.Add(categoria);
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