using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieCenter.Data
{
    public class Entrada
    {

        public int Id { get; set; }
        public int Usuario { get; set; }
        public decimal PrecioGeneral { get; set; }
        public decimal PrecioNino { get; set; }
        public decimal PrecioMayores { get; set; }
        public TipoSala Sala { get; set; }

        public Entrada()
        {

        }
        public Entrada(int id, decimal precioGen, decimal precioNino, decimal precioMayor, TipoSala sala, int usuario = 0)
        {
            Id = id;
            Usuario = usuario;
            PrecioGeneral = precioGen;
            PrecioNino = precioNino;
            PrecioMayores = precioMayor;
            Sala = sala;
        }

    }
}
