using System;

namespace MovieCenter.Data
{
    [Serializable]
    public class Sesion
    {
        public int Id { get; set; }
        public int Ventas { get; set; }
        public int SalaId { get; set; }
    }
}