using ModuloGestorNotas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ModuloGestorNotas.Controllers
{
    //[Authorize(Roles = "SuperAdmin")]
    //[Authorize(Roles = "Profesor")]
    //[Authorize(Roles = "Estudiante")]
    public class GruposController : Controller
    {
        enum Rol
        {
            Admin = 1,
            Profesor = 2,
            Estudiante = 3
        }

        enum SolicitudInscripcion
        {
            Desinscripcion = 0,
            Inscripcion = 1
        }

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
                var groupExists = db.Grupo.Any(x => x.MateriaId.Equals(Model.MateriaId) &&
                                                    x.PeriodoId.Equals(Model.PeriodoId) &&
                                                    x.SeccionId.Equals(Model.SeccionId) &&
                                                    x.Codigo.Equals(Model.Codigo));

                if (groupExists)
                {
                    return Json(new { Result = "ERROR", Message = "Grupo Existente" });

                }

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
                            EstadoSeleccion = checkIsRegisteredInGroup? ((int)SolicitudInscripcion.Inscripcion).ToString() : ((int)SolicitudInscripcion.Desinscripcion).ToString(),
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
                var groupOg = db.Grupo.SingleOrDefault(x => x.Id.Equals(Model.Id));
                if (groupOg == null)
                {
                    return Json(new {Result = "ERROR", Message = "Grupo no Existente"});
                }
                
                UsuariosPertenecenGrupo upg_actual = db.UsuariosPertenecenGrupos.ToList()
                                            .Where(t => t.UsuarioId == User.Identity.GetUserId())
                                            .Where(t => t.GrupoId == Model.Id)
                                            .Select(t => t).FirstOrDefault();

                bool checkIsRegisteredInGroup = (upg_actual == null) ? false : true;
                if (checkIsRegisteredInGroup && (Model.EstadoSeleccion == ((int)SolicitudInscripcion.Desinscripcion).ToString()))
                {
                    Nota nota = db.Nota.Find(db.UsuariosPertenecenGrupos.Where(t => t.GrupoId == upg_actual.GrupoId).FirstOrDefault().NotaId);

                    if (nota != null)
                    {
                        db.Nota.Remove(nota);
                        
                    }

                    db.UsuariosPertenecenGrupos.Remove(upg_actual);
                    db.SaveChanges();

                }
                else if(!checkIsRegisteredInGroup && (Model.EstadoSeleccion == ((int)SolicitudInscripcion.Inscripcion).ToString()))
                {
                    var gruposEstudiante = db.UsuariosPertenecenGrupos.Include(a => a.Grupo.Materia)
                        .Where(x => x.UsuarioId.Equals(current_id)).ToList();

                    if (gruposEstudiante.Any(x => x.Grupo.Materia.Nombre.Equals(groupOg.Materia.Nombre)))
                    {
                        return Json(new { Result = "ERROR", Message = "Materia Previamente Inscrita" });
                    }

                    Nota nota = new Nota () { PrimerParcial = 0, SegundoParcial = 0, ParcialFinal = 0, NotaTotal = 0 };
                    db.Nota.Add(nota);
                    db.SaveChanges();
                    db.UsuariosPertenecenGrupos.Add(new UsuariosPertenecenGrupo { GrupoId = Model.Id, UsuarioId = current_id, NotaId = nota.Id});
                    db.SaveChanges();
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

        [Route("Grupos/Asignacion")]
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult administradoresAsignanProfesores()
        {
            return View();
        }

        [Route("Grupos/Asignacion/Get")]
        [Authorize(Roles = "SuperAdmin")]
        public JsonResult getAdministradoresAsignanProfesores(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                List<Seleccion> lstSeleccion = new List<Seleccion>();
                //Buscamos los Usuarios con Rol de Profesor
                IdentityRole Profesores = db.Roles.Include(t=> t.Users).Where(t => t.Id == ((int)Rol.Profesor).ToString()).FirstOrDefault();

                foreach (var grupo in db.Grupo.Include(t => t.Materia).Include(t => t.Periodo).Include(t => t.Seccion).ToList())
                {
                    lstSeleccion.Add(
                           new Seleccion
                           {
                               Id = grupo.Id,
                               Grupo = grupo.Codigo,
                               EstadoSeleccion = "1",
                               Materia = grupo.Materia.Nombre,
                               Periodo = grupo.Periodo.Codigo,
                               Seccion = grupo.Seccion.Nombre
                           });
                }

                foreach (var profesor in Profesores.Users)
                {
                    foreach (var grupo in db.Grupo.Include(t => t.Materia).Include(t => t.Periodo).Include(t => t.Seccion).ToList())
                    {
                        bool checkIsRegisteredInGroup = (db.UsuariosPertenecenGrupos
                                                .Where(t => t.UsuarioId == profesor.UserId)
                                                .Where(t => t.GrupoId == grupo.Id)
                                                .Select(t => t).FirstOrDefault() == null) ? false : true;
                        if (checkIsRegisteredInGroup)
                        {
                            lstSeleccion.RemoveAll(t => t.Id == grupo.Id);
                            lstSeleccion.Add(
                            new Seleccion
                            {
                                Id = grupo.Id,
                                //Si el profesor existe en un grupo, se registrara su nombre, de lo contrario, no
                                EstadoSeleccion = checkIsRegisteredInGroup ? db.Users.Where(t => t.Id == profesor.UserId).FirstOrDefault().Id: null,
                                Grupo = grupo.Codigo,
                                Materia = grupo.Materia.Nombre,
                                Periodo = grupo.Periodo.Codigo,
                                Seccion = grupo.Seccion.Nombre
                        });

                        }
                        else
                        {
                            //Do Nothing
                        }

                    }
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
        [Route("Grupos/Asignacion/Edit")]
        [Authorize(Roles = "SuperAdmin")]
        public JsonResult editAdministradoresAsignanProfesores(Seleccion Model)
        {
            //En este contexto Model.EstadoSeleccion posee el id del profesor
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                if(Model.EstadoSeleccion == "1")
                {
                    //Buscamos los Usuarios con Rol de Profesor
                    IdentityRole Profesores = db.Roles.Include(t => t.Users).Where(t => t.Id == ((int)Rol.Profesor).ToString()).FirstOrDefault();
                    foreach (var item in Profesores.Users)
                    {
                        UsuariosPertenecenGrupo upg_actual = db.UsuariosPertenecenGrupos.ToList()
                                            .Where(t => t.UsuarioId == item.UserId)
                                            .Where(t => t.GrupoId == Model.Id)
                                            .Select(t => t).FirstOrDefault();

                        if (upg_actual != null)
                        {
                            db.UsuariosPertenecenGrupos.Remove(upg_actual);
                            db.SaveChanges();
                            break;
                        }
                    }
                }
                else
                {
                    bool checkTeacherExistInGroup = (db.UsuariosPertenecenGrupos
                                                        .Where(t => t.UsuarioId == Model.EstadoSeleccion)
                                                        .Where(t => t.GrupoId == Model.Id)
                                                        .FirstOrDefault() == null)? false: true;
                    if(checkTeacherExistInGroup)
                    {
                        try
                        {
                            //Si no se ha modificado, el programa ira al catch 
                            db.Entry(new UsuariosPertenecenGrupo { UsuarioId = Model.EstadoSeleccion, GrupoId = Model.Id }).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        catch (Exception)
                        {
                            //Do nothig
                        }
                    }
                    else if(!checkTeacherExistInGroup)
                    {
                        db.UsuariosPertenecenGrupos.Add(new UsuariosPertenecenGrupo { UsuarioId = Model.EstadoSeleccion, GrupoId = Model.Id });
                        db.SaveChanges();
                    }
                }/**/
                /*db.UsuariosPertenecenGrupos.Add(new UsuariosPertenecenGrupo { UsuarioId = "28c43a6c-41ad-4d0a-8f5c-b60a435ad29c", GrupoId = 17 });
                db.SaveChanges();*/
                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

    }
}