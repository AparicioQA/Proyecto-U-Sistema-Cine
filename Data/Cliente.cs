using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieCenter.Data
{
    [Serializable]
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Telefono { get; set; }
        public int Puntos { get; set; }
        public int Canjes { get; set; }
        public Usuario UsuarioP { get; set; }
        public int User { get; set; }
       
    }
}