using System.Linq;
using CarRentApi.Controllers.BaseData;
using CarRentApi.Model;
using CarRentApi.Repositories.Database;
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
            var carlist2 = carrent.CarClasses.ToList();

            Assert.IsTrue(carclasslist.Count == carlist2.Count);
        }

        [Test]
        public void Post_CarClass_IsTrue()
        {
            var carclass = new CarClass() { Class = "Ichwerdegeloescht", CostsPerDay = 1000m };
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
                    if (!cclass.Class.Equals(carclass.Class))
                        continue;
                    index = cclass.Id;
                }

                carclasscontroller.DeleteCarClass(index);
            }

            carclasscontroller.PostCarClass(carclass);

            var bOk = false;
            var ind = 0;
            foreach (var carsclasses in carclasscontroller.GetCarClasses())
            {
                if (carsclasses.Class.Equals(carclass.Class) && carsclasses.CostsPerDay == carclass.CostsPerDay)
                {
                    bOk = true;
                    ind = carsclasses.Id;
                    break;
                }
            }

            carclasscontroller.DeleteCarClass(ind);
            Assert.IsTrue(bOk);
        }

        [Test]
        public void Delete_CarClass_IsTrue()
        {
            var carclass = new CarClass() { Class = "Ichwerdegeloeschtzwei", CostsPerDay = 1000m };
            DbContextOptionsBuilder<CarRentDBContext> builder = new DbContextOptionsBuilder<CarRentDBContext>();
            builder.UseInMemoryDatabase("CarRent");
            DbContextOptions<CarRentDBContext> options = builder.Options;
            CarRentDBContext carrent = new CarRentDBContext(options);
            ExampleData.ExampleData.InitTestData(carrent);
            var carclasscontroller = new CarClassesController(carrent);
            carclasscontroller.PostCarClass(carclass);
            bool isAdded = false;
            int id = 0;
            foreach (var cclass in carclasscontroller.GetCarClasses())
            {
                if (cclass.Class.Equals(carclass.Class) && cclass.CostsPerDay == carclass.CostsPerDay)
                {
                    id = cclass.Id;
                    isAdded = true;
                    break;
                }
            }

            carclasscontroller.DeleteCarClass(id);
            Assert.IsTrue(isAdded && carclasscontroller.GetCarClass(id) == null);
        }

        [Test]
        public void Put_CarClass_IsTrue()
        {
            var carclass = new CarClass() { Class = "ichwerde", CostsPerDay = 1000m };
            DbContextOptionsBuilder<CarRentDBContext> builder = new DbContextOptionsBuilder<CarRentDBContext>();
            builder.UseInMemoryDatabase("CarRent");
            DbContextOptions<CarRentDBContext> options = builder.Options;
            CarRentDBContext carrent = new CarRentDBContext(options);
            ExampleData.ExampleData.InitTestData(carrent);
            var carclasscontroller = new CarClassesController(carrent);
            carclasscontroller.PostCarClass(carclass);
            int id = 0;
            foreach (var cclass in carclasscontroller.GetCarClasses())
            {
                if (cclass.Class.Equals("ichwerde"))
                {
                    id = cclass.Id;
                    break;
                }
            }
            var carclass2 = carclasscontroller.GetCarClass(id);
            carclasscontroller.PutCarClass(id, carclass2);
            var carclass3 = carclasscontroller.GetCarClass(id);
            Assert.IsTrue(carclass3.Class.Equals(carclass2.Class));
        }
    }
}