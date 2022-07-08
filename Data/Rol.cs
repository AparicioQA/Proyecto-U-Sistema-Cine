using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieCenter.Data
{
    [Serializable]
    public class Rol
    {
        public string Nombre { get; set; }
        public int Id { get; set; }
        public int Usuario { get; set; }

    }
}