using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModuloGestorNotas.Models
{
    public class Grupo
    {
        public int Id { get; set; }

        public int SeccionId { get; set; }
        public Seccion Seccion { get; set; }

        public int MateriaId { get; set; }
        public Materia Materia { get; set; }

    }
}