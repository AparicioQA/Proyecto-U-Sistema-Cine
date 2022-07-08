namespace MovieCenter.Data
{
    public class Promocion
    {

        public int Id { get; set; }
        public int Usuario { get; set; }
        public decimal Precio { get; set; }
        public string Dia { get; set; }
        public TipoSala Sala { get; set; }

        public Promocion()
        {

        }
        public Promocion(int id, decimal precio, string dia, TipoSala sala, int usuario = 0)
        {
            Id = id;
            Usuario = usuario;
            Precio = precio;
            Dia = dia;
            Sala = sala;
        }

    }
}