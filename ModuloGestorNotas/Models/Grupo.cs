using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModuloGestorNotas.Models
{
    public class Grupo
    {
        public int Id { get; set; }

        public string Codigo { get; set; }
        public int SeccionId { get; set; }
        public Seccion Seccion { get; set; }

        public int MateriaId { get; set; }
        public Materia Materia { get; set; }

        public int PeriodoId { get; set; }
        public Periodo Periodo { get; set; }

    }

    public class Seleccion
    {
        public int Id { get; set; }
        public string EstadoSeleccion { get; set; }
        public string Grupo { get; set; }
        public string Seccion { get; set; }
        public string Materia { get; set; }
        public string Periodo { get; set; }

    }
}