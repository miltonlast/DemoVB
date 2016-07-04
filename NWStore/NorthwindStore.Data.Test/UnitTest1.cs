using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NorthwindStore.Data.Test
{
    [TestClass]
    public class UnitTest1
    {
        [Description("Prueba de operador de suma.")]
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            int a = 1;
            int b = 1;
            int expected = 3;
            //act
            int result = a + b;
            //Assert
            Assert.AreEqual(expected, result, "La suma es inválida.")
;
        }
    }
}
