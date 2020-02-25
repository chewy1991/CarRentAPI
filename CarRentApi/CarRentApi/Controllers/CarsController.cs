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
    public class CarsController : ControllerBase
    {
        private readonly CarRentDBContext _context;

        public CarsController(CarRentDBContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public List<Car> GetCars()
        {
            return _context.Cars.ToList();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public Car GetCar(int id)
        {
            var car =  _context.Cars.Find(id);

            if (car == null)
            {
                return null;
            }

            return car;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/Cars
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            if (!CarExists(car))
            {
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCar", new { id = car.Id }, car);
            }

            return NoContent();

        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }
        private bool CarExists(Car car)
        {
            return _context.Cars.Any(e =>e.BrandId == car.BrandId) && _context.Cars.Any(e => e.ClassId == car.ClassId) && _context.Cars.Any(e => e.TypeId == car.TypeId) && _context.Cars.Any(e => e.RegistrationYear == car.RegistrationYear) && _context.Cars.Any(e => e.horsepower == car.horsepower) && _context.Cars.Any(e => e.kilometer == car.kilometer);
        }
        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
