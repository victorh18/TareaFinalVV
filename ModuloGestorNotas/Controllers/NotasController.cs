using Microsoft.AspNet.Identity;
using ModuloGestorNotas.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ModuloGestorNotas.Controllers
{
    public class NotasController : Controller
    {
        enum Rol
        {
            Admin = 1,
            Profesor = 2,
            Estudiante = 3
        }
        // GET: Notas
        #region EstudiantesVenNotas
        [Authorize(Roles = "Estudiante")]
        [Route("Notas/Estudiante")]
        public ActionResult IndexEstudiantes()
        {
            return View();
        }

        [Authorize(Roles = "Estudiante")]
        [Route("Notas/Get")]
        public JsonResult GetMateriasEstudiante(int Nombre = 0, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                //Declaramos la variable a enviar
                List<NotasEstudiantes> notasEstudiantes = new List<NotasEstudiantes>();

                //Declaramos la variable que va a contener los grupos del Estudiante 
                List<UsuariosPertenecenGrupo> gruposAsociadoAlEstudiante = new List<UsuariosPertenecenGrupo>();

                //UserId actual del estudiante
                var current_id = User.Identity.GetUserId();

                //Grupos en donde se encuentra el estudiante
                gruposAsociadoAlEstudiante = db.UsuariosPertenecenGrupos
                                                .Include(t => t.Grupo)
                                                .Where(t => t.UsuarioId == current_id).ToList();

                //validamos si existe una consulta a un periodo especifico
                bool existeConsulta = ((Nombre == 0) ? false : true);

                
                //Recolectamos la informacion de Materias y Nota de los estudiantes
                foreach (var item in gruposAsociadoAlEstudiante)
                {
                    item.Nota = db.Nota.Where(t => t.Id == item.NotaId).FirstOrDefault();
                    item.Grupo.Materia = db.Materia.Where(t => t.Id == item.Grupo.MateriaId).FirstOrDefault();
                    item.Grupo.Periodo = db.Periodo.Where(t => t.Id == item.Grupo.PeriodoId).FirstOrDefault();
                }

                //En caso de que se haya buscado mediante el filtro de busqueda, las sentencias de mas abajo lo harán
                if (existeConsulta)
                {
                    gruposAsociadoAlEstudiante.RemoveAll(t => t.Grupo.PeriodoId != Nombre);
                }

                //Asignamos los Datos al ModeloNotasEstudiante
                foreach (var item in gruposAsociadoAlEstudiante)
                {
                    string NotaA = (item.Nota.NotaTotal > 89) ? "A" : null;
                    string NotaB = ((item.Nota.NotaTotal < 90) && (item.Nota.NotaTotal > 79)) ? "B" : null;
                    string NotaC = ((item.Nota.NotaTotal < 80) && (item.Nota.NotaTotal > 69)) ? "C" : null;
                    string NotaF = (item.Nota.NotaTotal < 70) ? "F" : null;
                    notasEstudiantes.Add(new NotasEstudiantes
                    {
                        Id = (int)item.NotaId,
                        Nombre = item.Grupo.Periodo.Codigo,
                        GrupoMateria = item.Grupo.Codigo + " - " + item.Grupo.Materia.Nombre,
                        PrimerParcial = item.Nota.PrimerParcial,
                        SegundoParcial = item.Nota.SegundoParcial,
                        ParcialFinal = item.Nota.ParcialFinal,
                        NotaTotal = item.Nota.NotaTotal,
                        Literal = NotaA + NotaB + NotaC + NotaF,
                    });
                }


                int TotalRecords = notasEstudiantes.Count();
                notasEstudiantes = notasEstudiantes.Skip(jtStartIndex).Take(jtPageSize).ToList();
                //return Json(new { Result = "OK", Records = lstMaterias, TotalRecordCount = TotalRecords });

                return Json(new { Result = "OK", Records = notasEstudiantes, TotalRecordCount = TotalRecords }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        #region ProfesoresAsignanNotas
        [Authorize(Roles = "Profesor")]
        [Route("Notas/Profesor")]
        public ActionResult IndexProfesores()
        {
            return View();
        }

        [Authorize(Roles = "Profesor")]
        [Route("Notas/Estudiantes/Get")]
        public JsonResult GetMateriasProfesor(string Nombre = "", int GrupoMateria = 0, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                //Declaramos la variable a enviar
                List<NotasEstudiantes> notasEstudiantes = new List<NotasEstudiantes>();
                
                //Declaramos la variable que va a contener los estudiantes de un grupo 
                List<UsuariosPertenecenGrupo> estudiantesAsociadosAlGrupo = new List<UsuariosPertenecenGrupo>();
 
                //UserId actual del profesor
                var current_id = User.Identity.GetUserId();
                
                //Grupos en donde se encuentra el profesor
                List<UsuariosPertenecenGrupo> gruposAsociadosAlProfesor = db.UsuariosPertenecenGrupos
                                                                            .Include(t => t.Grupo)
                                                                            .Where(t => t.UsuarioId == current_id).ToList();

                //validamos si existe una consulta con un grupo especifico
                bool existeConsulta = ((GrupoMateria == 0) ? false: true);

                //Recolectamos estudiantes del mismo grupo del profesor
                foreach (var item in gruposAsociadosAlProfesor)
                {
                    int Consulta = (GrupoMateria == 0) ? item.GrupoId: GrupoMateria;

                    var personasEnGrupoActual = db.UsuariosPertenecenGrupos
                                                .Where(t => t.GrupoId == Consulta)
                                                .ToList();
                    foreach (var varItem in personasEnGrupoActual)
                    {
                        estudiantesAsociadosAlGrupo.Add(varItem);

                    }
                    if (existeConsulta)
                    {
                        break;
                    }
                }

                //Eliminamos al profesor de la lista
                estudiantesAsociadosAlGrupo.RemoveAll(t => t.UsuarioId == current_id);

                //Recolectamos la informacion de Materias y Nota de los estudiantes
                foreach (var item in estudiantesAsociadosAlGrupo)
                {
                    item.Nota = db.Nota.Where(t=> t.Id == item.NotaId).FirstOrDefault();
                    item.Grupo.Materia = db.Materia.Where(t => t.Id == item.Grupo.MateriaId).FirstOrDefault();
                }

                //Asignamos los Datos al ModeloNotasEstudiante
                foreach (var item in estudiantesAsociadosAlGrupo)
                {
                    string NotaA = (item.Nota.NotaTotal > 89) ? "A" : null;
                    string NotaB = ((item.Nota.NotaTotal < 90) && (item.Nota.NotaTotal > 79)) ? "B" : null;
                    string NotaC = ((item.Nota.NotaTotal < 80) && (item.Nota.NotaTotal > 69)) ? "C" : null;
                    string NotaF = (item.Nota.NotaTotal < 70) ? "F" : null;
                    notasEstudiantes.Add(new NotasEstudiantes {
                        Id = (int)item.NotaId,
                        Nombre = db.Users.Where(t => t.Id == item.UsuarioId).FirstOrDefault().Nombre+" "+ db.Users.Where(t => t.Id == item.UsuarioId).FirstOrDefault().Apellido,
                        GrupoMateria = item.Grupo.Codigo+" - "+item.Grupo.Materia.Nombre,
                        PrimerParcial = item.Nota.PrimerParcial,
                        SegundoParcial = item.Nota.SegundoParcial,
                        ParcialFinal = item.Nota.ParcialFinal,
                        NotaTotal = item.Nota.NotaTotal,
                        Literal = NotaA+NotaB+NotaC+NotaF,
                    });
                }

                //En caso de que se haya buscado mediante el filtro de busqueda, las sentencias de mas abajo lo harán
                if (Nombre != "")
                {
                    notasEstudiantes = notasEstudiantes.Where(t => t.Nombre.Contains(Nombre)).ToList();
                }

                int TotalRecords = notasEstudiantes.Count();
                notasEstudiantes = notasEstudiantes.Skip(jtStartIndex).Take(jtPageSize).ToList();
                //return Json(new { Result = "OK", Records = lstMaterias, TotalRecordCount = TotalRecords });

                return Json(new { Result = "OK", Records = notasEstudiantes, TotalRecordCount = TotalRecords }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Profesor")]
        [Route("Notas/Estudiantes/Edit")]
        public JsonResult Edit(NotasEstudiantes Model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                Nota data = new Nota()
                {
                    Id = Model.Id,
                    PrimerParcial = ((Model.PrimerParcial < 0) || (Model.PrimerParcial > 35)) ? 0 : Model.PrimerParcial,
                    SegundoParcial = ((Model.SegundoParcial < 0) || (Model.SegundoParcial > 35)) ? 0 : Model.SegundoParcial,
                    ParcialFinal = ((Model.ParcialFinal < 0) || (Model.ParcialFinal > 35)) ? 0 : Model.ParcialFinal
                };
                data.NotaTotal = data.PrimerParcial + data.SegundoParcial + data.ParcialFinal;
                db.Entry(data).State = EntityState.Modified;

                db.SaveChanges();
                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion
    }
}