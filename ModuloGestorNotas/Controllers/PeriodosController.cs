using ModuloGestorNotas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModuloGestorNotas.Controllers
{
    public class PeriodosController : Controller
    {
        // GET: Periodos
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                List<Periodo> lstPeriodos = new List<Periodo>();
                lstPeriodos = db.Periodo.ToList();

                switch (jtSorting)
                {
                    case "Codigo ASC":
                        lstPeriodos = lstPeriodos.OrderBy(t => t.Codigo).ToList();
                        break;
                    case "Codigo DESC":
                        lstPeriodos = lstPeriodos.OrderByDescending(t => t.Codigo).ToList();
                        break;
                }

                lstPeriodos = lstPeriodos.Skip(jtStartIndex).Take(jtPageSize).ToList();
                int TotalRecords = db.Periodo.Count();
                return Json(new { Result = "OK", Records = lstPeriodos, TotalRecordCount = TotalRecords });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Create(Periodo Model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                Model.Codigo = Model.Anio + "-C" + Model.Cuatrimestre;
                db.Periodo.Add(Model);
                db.SaveChanges();
                return Json(new { Result = "OK", Record = Model }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Edit(Periodo Model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                Model.Codigo = Model.Anio + "-C" + Model.Cuatrimestre;
                db.Entry(Model).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Result = "OK", Record = Model }, JsonRequestBehavior.AllowGet);
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
                Periodo periodo = db.Periodo.Find(ID);
                db.Periodo.Remove(periodo);
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