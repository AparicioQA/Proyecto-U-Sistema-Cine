using System;

namespace MovieCenter.Data
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Sinopsis { get; set; }
        public DateTime Fecha { get; set; }
        public Categoria CategoriaP { get; set; }
        public int Usuario { get; set; }
        public string Imagen { get; set; }
    }
}