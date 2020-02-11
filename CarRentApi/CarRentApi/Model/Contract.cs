using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentApi.Repositories.Database;

namespace CarRentApi.Model
{
    public class Contract: IDBTable
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
    }

    public class ContractComplete
    {
        public string BrandName { get; set; }
        public string Class { get; set; }
        public decimal CostsPerDay { get; set; }
        public string carType { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Adress { get; set; }
        public string EMailAdress { get; set; }
        public string Telephonenumber { get; set; }
        public int RentalDays { get; set; }
        public decimal Costs { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime RentalDate { get; set; }
        public ReservationState State { get; set; }

        public ContractComplete(CarRentDBContext context, Contract contract)
        {
            Reservation reservation = context.Reservations.Find(contract.ReservationId);
            this.ReservationDate = reservation.ReservationDate;
            this.State = reservation.State;
            this.Costs = reservation.Costs;
            this.ReservationDate = reservation.ReservationDate;
            this.RentalDate = reservation.RentalDate;
            this.RentalDays = reservation.RentalDays;
            Customer customer = context.Customers.Find(reservation.CustomerId);
            this.Firstname = customer.Firstname;
            this.Lastname = customer.Lastname;
            this.Adress = customer.Adress;
            this.EMailAdress = customer.EMailAdress;
            this.Telephonenumber = customer.Telephonenumber;
            Car car = context.Cars.Find(reservation.CarId);
            CarBrand brand = context.CarBrands.Find(car.BrandId);
            this.BrandName = brand.BrandName;
            CarClass carclass = context.CarClasses.Find(car.ClassId);
            this.Class = carclass.Class;
            CarType type = context.CarTypes.Find(car.TypeId);
            this.carType = type.carType;
            this.CostsPerDay = carclass.CostsPerDay;

        }
    }
}
