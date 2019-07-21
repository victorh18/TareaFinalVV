using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModuloGestorNotas.Models
{
    public class UsuariosPertenecenGrupo
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }

    }
}