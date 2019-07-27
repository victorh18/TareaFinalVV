using System;
using System.Data;
using System.Linq;
using ModuloGestorNotas.Controllers;
using ModuloGestorNotas.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModuloGestorNotasTestProject
{
    [TestClass]
    public class SeccionTests
    {
        [TestMethod]
        public void InsertsSeccion()
        {
            SeccionController sController = new SeccionController();
            Seccion seccion = new Seccion() { Nombre = "AAR4"};
            sController.Create(seccion);
            ApplicationDbContext db = new ApplicationDbContext();
            seccion = db.Seccion.Where(s => s.Nombre == "AAR4").First();
            Assert.IsTrue(seccion != null);
            db.Seccion.Remove(seccion);
        }
    }
}
