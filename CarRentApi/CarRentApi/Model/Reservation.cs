using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentApi.Model
{
    public class Reservation:IDBTable
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public int RentalDays { get; set; }
        public decimal Costs { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime RentalDate { get; set; }
        public ReservationState State { get; set; }
    }

    public enum ReservationState
    {
        pending,
        reserved,
        contracted
    }
}
