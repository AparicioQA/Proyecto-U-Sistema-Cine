namespace MovieCenter.Data
{
    public class Permiso
    {
        public int Id { get; set; }
        public Accion AccionP { get; set; }
        public bool Estado { get; set; }
        public int Usuario { get; set; }

    }
}