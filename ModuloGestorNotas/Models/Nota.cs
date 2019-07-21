using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModuloGestorNotas.Models
{
    public class Nota
    {
        public int Id { get; set; }

        public int PrimerParcial { get; set; }
        public int SegundoParcial { get; set; }
        public int ParcialFinal { get; set; }
        public int NotaTotal { get; set; }

        public int UsuariosPertenecenGrupoId { get; set; }
        public UsuariosPertenecenGrupo usuariosPertenecenGrupo { get; set; }

    }
}