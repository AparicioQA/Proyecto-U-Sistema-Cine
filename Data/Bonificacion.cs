using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieCenter.Data
{
    public class Bonificacion
    {
        public int Id { get; set; }
        public int Usuario { get; set; }
        public int  Puntos { get; set; }
        public int Canjes { get; set; }
      

    }
}