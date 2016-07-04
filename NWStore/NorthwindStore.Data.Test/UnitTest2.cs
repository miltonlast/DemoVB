using System;
using Northwind.Store.Data;
using Northwind.Store.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;


namespace NorthwindStore.Data.Test
{
     [TestClass]
    public  class UnitTest2
    {
         [TestInitialize]
         public void Init()
         {
             var rD = new RegionD();
             rD.Delete(5);
         }

         [TestMethod]
         public void TestRegionActualizar()
         {
             //arrange
             var rD = new RegionD();
             int key = 4;// llave existe

             //act
             var rUpdated = rD.Read(key);

             string esperado = rUpdated.RegionDescription.Trim() + "1";
             rUpdated.ModifiedProperties.Add("RegionDescription");

             rUpdated.RegionDescription = esperado;
             rD.Save(rUpdated);

             var result = rD.Read(key);

             //assert
             Assert.IsTrue(esperado==result.RegionDescription, "El cambio no fue satisfactorio.");
         }

         [TestMethod]
         public void TestRegionActualizar2()
         {
             var rD = new RegionD();
             var rD2 = new RegionD();
             int key = 4;
             var nm = new Northwind.Store.Notification.NotificationMessage();

             var r1 = rD.Read(key);
             string e1 = r1.RegionDescription.Trim() + "1";
             r1.ModifiedProperties.Add("RegionDescription");
             r1.RegionDescription = e1;


             var r2 = rD2.Read(key);
             string e2 = r1.RegionDescription.Trim() + "1";
             r2.ModifiedProperties.Add("RegionDescription");
             r2.RegionDescription = e2;
             rD2.Save(r2);

             rD.Save(r1,nm);


             Assert.IsTrue(nm.IsNotified, "No se genero notificacion.");
             Assert.IsInstanceOfType(nm.Messages.First(), 
                 typeof(Northwind.Store.Notification.MessageConcurrency), "Se esperaba error de concurrencia.");

         }



          [TestMethod]
          public void TestMethod1()
          {
              var rD = new RegionD();
              int key = 1;

              var result = rD.Read(key);

              Assert.IsNotNull(result, "La lleve no existe.");
          }


          [TestMethod]
         // [Ignore]
          public void TestRegionCrear()
          {
              int Key=5;
              var rD = new RegionD();

              var rNew = new Region()
              {
                  RegionID = 5,
                  RegionDescription ="Atlantic",
                  State = State.Added
                
              };
              var result1 = rD.Read(Key);

              Assert.IsNull(result1, "La region ya existe");

              rD.Save(rNew);
              var result2 = rD.Read(rNew.RegionID);

              Assert.IsNotNull(result2, "La region no fue creada");

          }

    }
}
