using System;
using System.Data.Entity;
using System.Linq;
using ModuloGestorNotas.Models;
using ModuloGestorNotas.Controllers;
using System.Web.Mvc;
using ModuloGestorNotasTestProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModuloGestorNotasTestProject
{
    [TestClass]
    public class GroupTests
    {
        [TestMethod]
        public void InsertsGroups()
        {
            //var l = new ModuloGestorNotas.Controllers.GruposController();
            //var result = l.estudiantesSeleccionanGrupos() as ViewResult;
            //Assert.AreEqual("estudiantesSeleccionanGrupos", result.ViewName);
            
            GruposController controller = new GruposController();
            ApplicationDbContext db = new ApplicationDbContext();
            Grupo grupoPrueba = new Grupo() { Codigo= "PRUEBA", MateriaId = 1, SeccionId = 3, PeriodoId = 3};
            //db.Grupo.Add(grupoPrueba);
            db.SaveChanges();
            controller.Create(grupoPrueba);
            Grupo grupoRetorno = db.Grupo.Where(g => g.Codigo == "PRUEBA").First();
            Assert.IsTrue(grupoRetorno != null);
            db.Grupo.Remove(grupoRetorno);

        }
    }
}
