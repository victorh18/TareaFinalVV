using System;
using System.Data;
using System.Linq;
using ModuloGestorNotas.Controllers;
using ModuloGestorNotas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModuloGestorNotasTestProject
{
    [TestClass]
    public class PeriodoTests
    {
        [TestMethod]
        public void InsertsPeriodos()
        {
            PeriodosController controller = new PeriodosController();
            ApplicationDbContext db = new ApplicationDbContext();
            Periodo periodoPrueba = new Periodo() { Anio = 2018, Cuatrimestre = 3};
            controller.Create(periodoPrueba);
            periodoPrueba = db.Periodo.Where(g => g.Codigo == "2018-C2").First();
            Assert.IsTrue(periodoPrueba != null);
            db.Periodo.Remove(periodoPrueba);
        }
    }
}
