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

                switch (jtSorting)
                {
                    case "Materia ASC":
                        lstGrupos = lstGrupos.OrderBy(t => t.Materia).ToList();
                        break;
                    case "Materia DESC":
                        lstGrupos = lstGrupos.OrderByDescending(t => t.Materia).ToList();
                        break;
                    case "Seccion ASC":
                        lstGrupos = lstGrupos.OrderBy(t => t.Seccion).ToList();
                        break;
                    case "Seccion DESC":
                        lstGrupos = lstGrupos.OrderByDescending(t => t.Seccion).ToList();
                        break;
                }

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