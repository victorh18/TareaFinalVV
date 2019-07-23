﻿using ModuloGestorNotas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModuloGestorNotas.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class MateriasController : Controller
    {
        // GET: Materias
        public ActionResult Index()
        {
            return View();
        }

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

    }
}