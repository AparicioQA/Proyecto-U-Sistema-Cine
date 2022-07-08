using System.Configuration;

namespace MovieCenter.Connection
{
    public class ConexionBD
    {
        public string ObtenerCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["connDB"].ConnectionString.ToString();

        }
    }
}