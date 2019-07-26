using System;
using System.Data;
using System.Linq;
using ModuloGestorNotas.Models;
using ModuloGestorNotas.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModuloGestorNotasTestProject
{
    [TestClass]
    public class MateriasTests
    {
        [TestMethod]
        public void InsertsMaterias()
        {
            MateriasController mController = new MateriasController();
            Materia materia = new Materia() { Nombre = "Psicologia" };
            mController.Create(materia);
            ApplicationDbContext db = new ApplicationDbContext();
            materia = db.Materia.Where(m => m.Nombre == "Psicologia").First();
            Assert.IsTrue(materia != null);
            db.Materia.Remove(materia);

            
        }
    }
}
