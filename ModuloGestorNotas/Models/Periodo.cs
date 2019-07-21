using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModuloGestorNotas.Models
{
    public class Periodo
    {
        public int Id { get; set; }

        public string Codigo { get; set; }
        public int Anio { get; set; }
        public int Cuatrimestre { get; set; }

    }
}