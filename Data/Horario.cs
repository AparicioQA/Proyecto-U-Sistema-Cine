using System;

namespace MovieCenter.Data
{
    public class Horario
    {
        public int Id { get; set; }
        public TimeSpan Hora { get; set; }
        public bool Estado { get; set; }
    }
}