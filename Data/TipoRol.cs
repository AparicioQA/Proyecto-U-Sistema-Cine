using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieCenter.Data
{
    [Serializable]
    public class TipoRol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Mantenimiento> Mantenimientos { get; set; }
        
        public TipoRol()
        {

        }

        public TipoRol(int id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;

        }
    }
}