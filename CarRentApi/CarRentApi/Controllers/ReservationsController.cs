using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentApi.Model;
using CarRentApi.Repositories.Database;

namespace CarRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly CarRentDBContext _context;

        public ReservationsController(CarRentDBContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public List<Reservation> GetReservations()
        {
            return _context.Reservations.ToList();
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public Reservation GetReservation(int id)
        {
            var reservation =  _context.Reservations.Find(id);

            if (reservation == null)
            {
                return null;
            }

            return reservation;
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reservations
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            if (!ReservationExists(reservation))
            {
                List<Reservation> reslist = _context.Reservations.ToList();
                bool carreserved = false;
                foreach (var res in reslist)
                {
                    if (reservation.CarId == res.CarId)
                    {
                        DateTime resend = res.RentalDate;
                        resend = resend.AddDays(res.RentalDays);
                        DateTime reservationEnd = reservation.RentalDate;
                        reservationEnd = reservationEnd.AddDays(reservation.RentalDays);
                        if (reservation.RentalDate.Date >= res.RentalDate.Date && reservation.RentalDate <= resend || reservationEnd.Date >= res.RentalDate.Date && reservationEnd <= resend)
                        {
                            carreserved = true;
                            break;
                        }
                    }
                    
                }
                if (!carreserved)
                {
                    CarClass carclass = new CarClass();
                    Car car = _context.Cars.Find(reservation.CarId);
                    carclass = _context.CarClasses.Find(car.ClassId);
                    reservation.Costs = reservation.RentalDays * carclass.CostsPerDay;
                    if (reservation.State != ReservationState.pending)
                        reservation.State = ReservationState.pending;
                    _context.Reservations.Add(reservation);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
                }
            }

            return NoContent();
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }
        private bool ReservationExists(Reservation reservation)
        {
            return _context.Reservations.Any(e => e.CarId == reservation.CarId) && _context.Reservations.Any(e => e.Costs == reservation.Costs) && _context.Reservations.Any(e => e.CustomerId == reservation.CustomerId) && _context.Reservations.Any(e => e.RentalDays == reservation.RentalDays) && _context.Reservations.Any(e => e.RentalDate == reservation.RentalDate) && _context.Reservations.Any(e => e.ReservationDate == reservation.ReservationDate) && _context.Reservations.Any(e => e.State == reservation.State);
        }
        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
