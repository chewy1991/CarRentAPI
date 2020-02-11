using System.Linq;
using CarRentApi.Controllers.BaseData;
using CarRentApi.Model;
using CarRentApi.Repositories.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CarRentApi.test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Get_CarclassController_IsTrue()
        {
            DbContextOptionsBuilder<CarRentDBContext> builder = new DbContextOptionsBuilder<CarRentDBContext>();
            builder.UseInMemoryDatabase("CarRent");
            DbContextOptions<CarRentDBContext> options = builder.Options;
            CarRentDBContext carrent = new CarRentDBContext(options);
            ExampleData.ExampleData.InitTestData(carrent);
            var carclasscontroller = new CarClassesController(carrent);
            var carclasslist = carclasscontroller.GetCarClasses();

            Assert.IsTrue(carclasslist.Count == 3);
        }

        [Test]
        public void Post_CarClass_IsTrue()
        {
            var carclass = new CarClass(){Class = "Ichwerdegeloescht",CostsPerDay = 1000m};
            DbContextOptionsBuilder<CarRentDBContext> builder = new DbContextOptionsBuilder<CarRentDBContext>();
            builder.UseInMemoryDatabase("CarRent");
            DbContextOptions<CarRentDBContext> options = builder.Options;
            CarRentDBContext carrent = new CarRentDBContext(options);
            ExampleData.ExampleData.InitTestData(carrent);
            var carclasscontroller = new CarClassesController(carrent);

            if (carrent.CarClasses.Any(e => e.Class == carclass.Class))
            {
                int index = 0;
                var carclasslist = carclasscontroller.GetCarClasses();
                foreach (var cclass in carclasslist)
                {
                    if(!cclass.Class.Equals(carclass.Class))
                        continue;
                    index = cclass.Id;
                }

                carclasscontroller.DeleteCarClass(index);
            }

            carclasscontroller.PostCarClass(carclass);

            var bOk = false;
            foreach (var carsclasses in carclasscontroller.GetCarClasses())
            {
                if (carsclasses.Class.Equals(carclass.Class) && carsclasses.CostsPerDay == carclass.CostsPerDay)
                {
                    bOk = true;
                    break;
                }
            }
            Assert.IsTrue(bOk);
        }

        [Test]
        public void Post_CarClassExisting_IsTrue()
        {
            var carclass = new CarClass() { Class = "Ichwerdegeloescht", CostsPerDay = 1000m };
            var carclass2 = new CarClass() { Class = "Ichwerdegeloescht", CostsPerDay = 1000m };

            DbContextOptionsBuilder<CarRentDBContext> builder = new DbContextOptionsBuilder<CarRentDBContext>();
            builder.UseInMemoryDatabase("CarRent");
            DbContextOptions<CarRentDBContext> options = builder.Options;
            CarRentDBContext carrent = new CarRentDBContext(options);
            ExampleData.ExampleData.InitTestData(carrent);
            var carclasscontroller = new CarClassesController(carrent);
            carclasscontroller.PostCarClass(carclass);
            var x = carclasscontroller.PostCarClass(carclass2);

            int Count = 0;
            foreach (var carsclasses in carclasscontroller.GetCarClasses())
            {
                if (carsclasses.Class.Equals(carclass.Class) && carsclasses.CostsPerDay == carclass.CostsPerDay)
                {
                    Count++;
                }
            }
            Assert.IsTrue(Count == 1);
        }

    }
}