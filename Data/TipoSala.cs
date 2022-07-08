using System;

namespace MovieCenter.Data
{
    [Serializable]
    public class TipoSala
    {
        public int Id { get; set; }
        public int Capacidad { get; set; }
        public string Clasificacion { get; set; }

        public TipoSala()
        {

        }
        public TipoSala(int id, int capacidad, string clasificacion)
        {
            this.Id = id;
            this.Capacidad = capacidad;
            this.Clasificacion = clasificacion;
        }


    }
}