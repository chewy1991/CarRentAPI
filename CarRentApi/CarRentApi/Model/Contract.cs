using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentApi.Model
{
    public class Contract: IDBTable
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
    }
}
