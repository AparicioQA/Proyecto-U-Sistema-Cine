using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MovieCenter.Data
{
    [Serializable]
    public class Sala
    {
        public int Id { get; set; }
        public TipoSala Tipo { get; set; }
    }
}