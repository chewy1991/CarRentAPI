using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentApi.Model
{
    public class CarBrand:IDBTable
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
    }
}
