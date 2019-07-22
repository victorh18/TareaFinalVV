using ModuloGestorNotas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModuloGestorNotas.Controllers
{
    public class GruposController : Controller
    {
        // GET: Grupos
        public ActionResult Index()
        {
            return View();
        }

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


    }
}