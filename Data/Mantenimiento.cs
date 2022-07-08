using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MovieCenter.Data
{
    [Serializable]
    public class Mantenimiento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Permiso> Permisos { get; set; }
    }
}