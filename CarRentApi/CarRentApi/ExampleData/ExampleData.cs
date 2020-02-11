using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentApi.Model;
using CarRentApi.Repositories.Database;

namespace CarRentApi.ExampleData
{
    public static class ExampleData
    {
        public static void InitTestData(CarRentDBContext context)
        {
            var carclasslist = new List<CarClass>()
            {
                new CarClass() {Class = "Economy", CostsPerDay = 25m},
                new CarClass() {Class = "Middleclass", CostsPerDay = 40m},
                new CarClass() {Class = "Luxury", CostsPerDay = 55m}
            };

            var cartypelist = new List<CarType>()
            {
                new CarType(){carType = "Limousine"},
                new CarType(){carType = "Kombi"},
                new CarType(){carType = "SUV"}
            };

            var carbrandlist = new List<CarBrand>()
            {
                new CarBrand(){BrandName = "VW"},
                new CarBrand(){BrandName = "Audi"},
                new CarBrand(){BrandName = "Fiat"},
                new CarBrand(){BrandName = "Seat"}
            };

            context.CarClasses.AddRange(carclasslist);
            context.CarBrands.AddRange(carbrandlist);
            context.CarTypes.AddRange(cartypelist);
            context.SaveChanges();

            var carlist = new List<Car>()
            {
                new Car(){BrandId = 1,ClassId = 1,horsepower = 100,kilometer = 150000,RegistrationYear = 2020,TypeId = 1},
                new Car(){BrandId = 2,ClassId = 2,horsepower = 150,kilometer = 150000,RegistrationYear = 2019,TypeId = 2},
                new Car(){BrandId = 3,ClassId = 3,horsepower = 300,kilometer = 150000,RegistrationYear = 2018,TypeId = 3}
            };
            context.Cars.AddRange(carlist);
            context.SaveChanges();

            var customerlist = new List<Customer>()
            {
                new Customer(){Adress = "W. Wolfensbergerstr. 3, 9424 Rheineck", EMailAdress = "addi.kaufmann@bluewin.ch", Firstname = "Adrian",Lastname = "Kaufmann", Telephonenumber = "0765655282"},
                new Customer(){Adress = "W. Wolfensbergerstr. 5, 9424 Rheineck", EMailAdress = "max.mustermann@bluewin.ch", Firstname = "Max",Lastname = "Mustermann", Telephonenumber = "0798888888"},
                new Customer(){Adress = "W. Wolfensbergerstr. 7, 9424 Rheineck", EMailAdress = "vreni.musterfrau@bluewin.ch", Firstname = "Vreni",Lastname = "Musterfrau", Telephonenumber = "0774445566"},
            };

            context.Customers.AddRange(customerlist);
            context.SaveChanges();

            var reservation = new Reservation(){CarId = 1,CustomerId = 1,RentalDate = new DateTime(2020,02,11),ReservationDate = new DateTime(2020,01,01),RentalDays = 8,State = ReservationState.reserved};
            var reservation2 = new Reservation() { CarId = 2, CustomerId = 2, RentalDate = new DateTime(2020, 02, 11), ReservationDate = new DateTime(2020, 01, 01), RentalDays = 8, State = ReservationState.reserved };
            var car = context.Cars.Find(reservation.CarId);
            var carclass = context.CarClasses.Find(car.ClassId);
            reservation.Costs = reservation.RentalDays * carclass.CostsPerDay;
            context.Reservations.Add(reservation);
            context.Reservations.Add(reservation2);
            context.SaveChanges();
        }
    }
}
