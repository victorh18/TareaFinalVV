using ModuloGestorNotas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModuloGestorNotas.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SeccionController : Controller
    {
        // GET: Seccion
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                List<Seccion> lstSecciones = new List<Seccion>();
                lstSecciones = db.Seccion.ToList();

                switch (jtSorting)
                {
                    case "Nombre ASC":
                        lstSecciones = lstSecciones.OrderBy(t => t.Nombre).ToList();
                        break;
                    case "Nombre DESC":
                        lstSecciones = lstSecciones.OrderByDescending(t => t.Nombre).ToList();
                        break;
                }

                lstSecciones = lstSecciones.Skip(jtStartIndex).Take(jtPageSize).ToList();
                int TotalRecords = db.Seccion.Count();
                return Json(new { Result = "OK", Records = lstSecciones, TotalRecordCount = TotalRecords });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Create(Seccion Model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                db.Seccion.Add(Model);
                db.SaveChanges();
                return Json(new { Result = "OK", Record = Model }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Edit(Seccion Model)
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
                Seccion seccion = db.Seccion.Find(ID);
                db.Seccion.Remove(seccion);
                db.SaveChanges();
                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public JsonResult GetSecciones()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                List<Options> options = new List<Options>();
                List<Seccion> lstSecciones = new List<Seccion>();
                lstSecciones = db.Seccion.ToList();
                foreach (var item in lstSecciones)
                {
                    options.Add(new Options { DisplayText = item.Nombre, Value = item.Id.ToString() });
                }
                return Json(new { Result = "OK", Options = options }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}