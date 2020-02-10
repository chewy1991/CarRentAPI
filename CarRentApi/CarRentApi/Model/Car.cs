using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentApi.Model
{
    public class Car:IDBTable
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ClassId { get; set; }
        public int TypeId { get; set; }
        public decimal kilometer { get; set; }
        public int horsepower { get; set; }
        public int RegistrationYear { get; set; }
    }
}
