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
    public class MateriasController : Controller
    {
        // GET: Materias
        //[Authorize(Roles = "SuperAdmin")]
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles = "SuperAdmin")]
        public JsonResult Get(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                List<Materia> lstMaterias = new List<Materia>();
                lstMaterias = db.Materia.ToList();

                switch (jtSorting)
                {
                    case "Nombre ASC":
                        lstMaterias = lstMaterias.OrderBy(t => t.Nombre).ToList();
                        break;
                    case "Nombre DESC":
                        lstMaterias = lstMaterias.OrderByDescending(t => t.Nombre).ToList();
                        break;
                }

                lstMaterias = lstMaterias.Skip(jtStartIndex).Take(jtPageSize).ToList();
                int TotalRecords = db.Materia.Count();
                return Json(new { Result = "OK", Records = lstMaterias, TotalRecordCount = TotalRecords });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public JsonResult Create(Materia Model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                db.Materia.Add(Model);
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
        public JsonResult Edit(Materia Model)
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
                Materia materia = db.Materia.Find(ID);
                db.Materia.Remove(materia);
                db.SaveChanges();
                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        public JsonResult GetMaterias()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                List<Options> options = new List<Options>();
                List<Materia> lstMaterias = new List<Materia>();
                lstMaterias = db.Materia.ToList();
                foreach (var item in lstMaterias)
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

        //Obtenemos Grupos y Materias asignadas al Usurio con Rol=Profesor que lo consulte
        [Authorize(Roles = "Profesor")]
        [Route("Materias/GetMaterias/Profesor")]
        public JsonResult GetMateriasProfesor()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                List<Options> options = new List<Options>();
                var current_id = User.Identity.GetUserId();
                List<UsuariosPertenecenGrupo> gruposAsociadosAlProfesor = db.UsuariosPertenecenGrupos
                                                                            .Include(t => t.Grupo)
                                                                            .Where(t => t.UsuarioId == current_id).ToList();
                //Asignamos la materia a cada grupo
                foreach (var item in gruposAsociadosAlProfesor)
                {
                    item.Grupo.Materia = db.Materia.Where(t => t.Id == item.Grupo.MateriaId).FirstOrDefault();
                }
                //Lo colocamos en el modelo Options que sera reconocible para el JTable y demas Scripts
                foreach (var item in gruposAsociadosAlProfesor)
                {
                    options.Add(new Options { DisplayText = item.Grupo.Codigo+" - "+item.Grupo.Materia.Nombre, Value = item.GrupoId.ToString() });
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