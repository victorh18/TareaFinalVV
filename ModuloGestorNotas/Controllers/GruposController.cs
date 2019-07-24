using ModuloGestorNotas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ModuloGestorNotas.Controllers
{
    //[Authorize(Roles = "SuperAdmin")]
    //[Authorize(Roles = "Profesor")]
    //[Authorize(Roles = "Estudiante")]
    public class GruposController : Controller
    {
        #region Grupos
        // GET: Grupos
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        public JsonResult Get(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                List<Grupo> lstGrupos = new List<Grupo>();
                lstGrupos = db.Grupo.ToList();
                var materias = db.Materia.ToList();
                var secciones = db.Seccion.ToList();
                var periodos = db.Periodo.ToList();

                #region Switch Cases for Criteria Property Order
                switch (jtSorting)
                {
                    case "Codigo ASC":
                        lstGrupos = lstGrupos.OrderBy(t => t.Codigo).ToList();
                        break;
                    case "Codigo DESC":
                        lstGrupos = lstGrupos.OrderByDescending(t => t.Codigo).ToList();
                        break;
                    case "MateriaId ASC":
                        var materiasASC = new List<Grupo>();
                        foreach (var item in lstGrupos)
                        {
                            var materia = materias.Where(t => t.Id == item.Materia.Id).Select(t => t).FirstOrDefault();
                            item.Materia = materia;
                            materiasASC.Add(item);
                        }
                        lstGrupos = materiasASC.OrderBy(t => t.Materia.Nombre).ToList();
                        break;
                    case "MateriaId DESC":
                        var materiasDESC = new List<Grupo>();
                        foreach (var item in lstGrupos)
                        {
                            var materia = materias.Where(t => t.Id == item.Materia.Id).Select(t => t).FirstOrDefault();
                            item.Materia = materia;
                            materiasDESC.Add(item);
                        }
                        lstGrupos = materiasDESC.OrderByDescending(t => t.Materia.Nombre).ToList(); ;
                        break;
                    case "SeccionId ASC":
                        var seccionASC = new List<Grupo>();
                        foreach (var item in lstGrupos)
                        {
                            var materia = materias.Where(t => t.Id == item.Materia.Id).Select(t => t).FirstOrDefault();
                            item.Materia = materia;
                            seccionASC.Add(item);
                        }
                        lstGrupos = seccionASC.OrderBy(t => t.Seccion.Nombre).ToList();
                        break;
                    case "SeccionId DESC":
                        var seccionDESC = new List<Grupo>();
                        foreach (var item in lstGrupos)
                        {
                            var materia = materias.Where(t => t.Id == item.Materia.Id).Select(t => t).FirstOrDefault();
                            item.Materia = materia;
                            seccionDESC.Add(item);
                        }
                        lstGrupos = seccionDESC.OrderByDescending(t => t.Seccion.Nombre).ToList();
                        break;
                    case "PeriodoId ASC":
                        var periodoASC = new List<Grupo>();
                        foreach (var item in lstGrupos)
                        {
                            var materia = materias.Where(t => t.Id == item.Materia.Id).Select(t => t).FirstOrDefault();
                            item.Materia = materia;
                            periodoASC.Add(item);
                        }
                        lstGrupos = periodoASC.OrderBy(t => t.Periodo.Codigo).ToList();
                        break;
                    case "PeriodoId DESC":
                        var periodoDESC = new List<Grupo>();
                        foreach (var item in lstGrupos)
                        {
                            var materia = materias.Where(t => t.Id == item.Materia.Id).Select(t => t).FirstOrDefault();
                            item.Materia = materia;
                            periodoDESC.Add(item);
                        }
                        lstGrupos = periodoDESC.OrderByDescending(t => t.Periodo.Codigo).ToList();
                        break;
                }
                #endregion

                lstGrupos = lstGrupos.Skip(jtStartIndex).Take(jtPageSize).ToList();
                int TotalRecords = db.Grupo.Count();
                return Json(new { Result = "OK", Records = lstGrupos, TotalRecordCount = TotalRecords });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public JsonResult Create(Grupo Model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                db.Grupo.Add(Model);
                db.SaveChanges();
                return Json(new { Result = "OK", Record = Model }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public JsonResult Edit(Grupo Model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                db.Entry(Model).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public JsonResult Delete(int ID)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                Grupo grupo = db.Grupo.Find(ID);
                db.Grupo.Remove(grupo);
                db.SaveChanges();
                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion

        #region Estudiantes Seleccionan Grupos
        [Route("Grupos/Seleccion")]
        [Authorize(Roles = "Estudiante")]
        public ActionResult estudiantesSeleccionanGrupos()
        {
            return View();
        }

        [Route("Grupos/Seleccion/Get")]
        [Authorize(Roles = "Estudiante")]
        public JsonResult getEstudiantesSeleccionanGrupos(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                var current_id = User.Identity.GetUserId();
                List<Seleccion> lstSeleccion = new List<Seleccion>();

                foreach (var item in db.Grupo.ToList())
                {
                    bool checkIsRegisteredInGroup = (db.UsuariosPertenecenGrupos.ToList()
                                                .Where(t => t.UsuarioId == User.Identity.GetUserId())
                                                .Where(t => t.GrupoId == item.Id)
                                                .Select(t => t).FirstOrDefault() == null)? false: true;
                    lstSeleccion.Add(
                        new Seleccion
                        {
                            Id = item.Id,
                            EstadoSeleccion = checkIsRegisteredInGroup? 1.ToString(): 0.ToString(),
                            Grupo = item.Codigo,
                            Materia = db.Materia.ToList().Where(t => t.Id == item.MateriaId).Select(t => t).FirstOrDefault().Nombre,
                            Periodo = db.Periodo.ToList().Where(t => t.Id == item.PeriodoId).Select(t => t).FirstOrDefault().Codigo,
                            Seccion = db.Seccion.ToList().Where(t => t.Id == item.SeccionId).Select(t => t).FirstOrDefault().Nombre
                        }); 
                }

                switch (jtSorting)
                {
                    case "Materia ASC":
                        lstSeleccion = lstSeleccion.OrderBy(t => t.Materia).ToList();
                        break;
                    case "Materia DESC":
                        lstSeleccion = lstSeleccion.OrderByDescending(t => t.Materia).ToList(); ;
                        break;
                }

                lstSeleccion = lstSeleccion.Skip(jtStartIndex).Take(jtPageSize).ToList();
                int TotalRecords = db.Grupo.Count();
                return Json(new { Result = "OK", Records = lstSeleccion, TotalRecordCount = TotalRecords }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("Grupos/Seleccion/Edit")]
        [Authorize(Roles = "Estudiante")]
        public JsonResult editEstudiantesSeleccionanGrupos(Seleccion Model)
        {
            var current_id = User.Identity.GetUserId();
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                UsuariosPertenecenGrupo upg = new UsuariosPertenecenGrupo();
                UsuariosPertenecenGrupo upg_actual = db.UsuariosPertenecenGrupos.ToList()
                                            .Where(t => t.UsuarioId == User.Identity.GetUserId())
                                            .Where(t => t.GrupoId == Model.Id)
                                            .Select(t => t).FirstOrDefault();
                bool checkIsRegisteredInGroup = (upg_actual == null) ? false : true;
                if (checkIsRegisteredInGroup && (Model.EstadoSeleccion == "0"))
                {
                    Nota nota = db.Nota.Find(db.UsuariosPertenecenGrupos.Where(t => t.GrupoId == upg_actual.GrupoId).FirstOrDefault().NotaId);
                    db.Nota.Remove(nota);
                    db.UsuariosPertenecenGrupos.Remove(upg_actual);
                    db.SaveChanges();
                }
                else if(!checkIsRegisteredInGroup && (Model.EstadoSeleccion == "1"))
                {
                    Nota nota = new Nota () { PrimerParcial = 0, SegundoParcial = 0, ParcialFinal = 0, NotaTotal = 0 };
                    db.Nota.Add(nota);
                    db.SaveChanges();
                    db.UsuariosPertenecenGrupos.Add(new UsuariosPertenecenGrupo { GrupoId = Model.Id, UsuarioId = current_id, NotaId = nota.Id});
                    db.SaveChanges();
                }
                else
                {
                    //Console.WriteLine("Do Nothing");
                }

                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Administradores Asignan Profesores a Grupos

        #endregion

    }
}