using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieCenter.Data
{
    public class Venta
    {
        public int Butaca { get; set; }
        public int Cliente { get; set; }
        public int Sesion { get; set; }
        public decimal Precio { get; set; }
}
}