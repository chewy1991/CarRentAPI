using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentApi.Model
{
    public class CarClass:IDBTable
    {
        public int Id { get; set; }
        public string Class { get; set;}
    }

    
}
