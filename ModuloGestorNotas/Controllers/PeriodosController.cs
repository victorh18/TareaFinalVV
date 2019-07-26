using Microsoft.AspNet.Identity;
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
        [Authorize(Roles = "SuperAdmin")]
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
        [Authorize(Roles = "SuperAdmin")]
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
        [Authorize(Roles = "SuperAdmin")]
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

        [Authorize(Roles = "SuperAdmin")]
        public JsonResult GetPeriodos()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                List<Options> options = new List<Options>();
                List<Periodo> lstPeriodos = new List<Periodo>();
                lstPeriodos = db.Periodo.ToList();
                foreach (var item in lstPeriodos)
                {
                    options.Add(new Options { DisplayText = item.Codigo, Value = item.Id.ToString() });
                }
                return Json(new { Result = "OK", Options = options }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //Obtenemos Grupos y Materias asignadas al Usurio con Rol=Profesor que lo consulte
        [Authorize(Roles = "Estudiante")]
        [Route("Periodos/GetPeriodos/Estudiante")]
        public JsonResult GetPeriodosEstudiante()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                List<Options> options = new List<Options>();
                List<Periodo> periodos = new List<Periodo>();
                var current_id = User.Identity.GetUserId();
                List<UsuariosPertenecenGrupo> gruposAsociadosAlEstudiante = db.UsuariosPertenecenGrupos
                                                                            .Include(t => t.Grupo)
                                                                            .Where(t => t.UsuarioId == current_id).ToList();

                //Asignamos la periodos a cada grupo
                foreach (var item in gruposAsociadosAlEstudiante)
                {
                    if(periodos.Where(t => t.Id == item.Grupo.Periodo.Id).FirstOrDefault() != null)
                    {
                        periodos.Remove(periodos.Where(t => t.Id == item.Grupo.Periodo.Id).FirstOrDefault());    
                    }
                    periodos.Add(db.Periodo.Where(t => t.Id == item.Grupo.PeriodoId).FirstOrDefault());
                }

                //Lo colocamos en el modelo Options que sera reconocible para el JTable y demas Scripts
                foreach (var item in periodos)
                {
                    options.Add(new Options { DisplayText = item.Codigo, Value = item.Id.ToString() });
                }
                return Json(new { Result = "OK", Options = options }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}