using ModuloGestorNotas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModuloGestorNotas.Controllers
{
    public class SeleccionController : Controller
    {
        // GET: Seleccion
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetMaterias()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                List<Materia> materias = db.Materia.ToList();
                return Json(new { Result = "OK", Records = materias, TotalRecordCount = materias.Count });
            }
            catch (Exception)
            {
                return Json(new { Result = "ERROR", Message = "KERNEL PANIC!" });
            }
        }

        public JsonResult GetSecciones()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                return Json(new { Result = "OK", Records = db.Seccion.ToList() });
            }
            catch (Exception)
            {
                return Json(new { Result = "ERROR", Message = "KERNET PANIC TO THE TENFOLD!" });
            }
        }
    }
}