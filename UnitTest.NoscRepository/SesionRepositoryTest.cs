using Nosc.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Nosc.Model;

namespace UnitTest.NoscRepository
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para SesionRepositoryTest y se pretende que
    ///contenga todas las pruebas unitarias SesionRepositoryTest.
    ///</summary>
    [TestClass()]
    public class SesionRepositoryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de la prueba que proporciona
        ///la información y funcionalidad para la ejecución de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales
        // 
        //Puede utilizar los siguientes atributos adicionales mientras escribe sus pruebas:
        //
        //Use ClassInitialize para ejecutar código antes de ejecutar la primera prueba en la clase 
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup para ejecutar código después de haber ejecutado todas las pruebas en una clase
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize para ejecutar código antes de ejecutar cada prueba
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup para ejecutar código después de que se hayan ejecutado todas las pruebas
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Una prueba de GetSesion
        ///</summary>
        [TestMethod()]
        public void GetSesionTest()
        {
            SesionRepository target = new SesionRepository(); // TODO: Inicializar en un valor adecuado
            SesionModel expected = null; // TODO: Inicializar en un valor adecuado
            SesionModel actual;
            actual = target.GetSesion();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
