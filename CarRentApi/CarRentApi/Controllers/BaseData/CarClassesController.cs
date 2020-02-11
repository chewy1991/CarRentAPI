using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentApi.Model;
using CarRentApi.Repositories.Database;

namespace CarRentApi.Controllers.BaseData
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarClassesController : ControllerBase
    {
        private readonly CarRentDBContext _context;

        public CarClassesController(CarRentDBContext context)
        {
            _context = context;
        }

        // GET: api/CarClasses
        [HttpGet]
        public  List<CarClass> GetCarClasses()
        {
            return  _context.CarClasses.ToList();
        }

        // GET: api/CarClasses/5
        [HttpGet("{id}")]
        public CarClass GetCarClass(int id)
        {
            var carClass =  _context.CarClasses.Find(id);

            if (carClass == null)
            {
                return null;
            }

            return carClass;
        }

        // PUT: api/CarClasses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarClass(int id, CarClass carClass)
        {
            if (id != carClass.Id)
            {
                return BadRequest();
            }

            _context.Entry(carClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarClassExists(id))
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

        // POST: api/CarClasses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CarClass>> PostCarClass(CarClass carClass)
        {
            if (!CarClassExists(carClass.Class))
            {
                _context.CarClasses.Add(carClass);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCarClass", new { id = carClass.Id }, carClass);
            }

            return NoContent();
        }

        // DELETE: api/CarClasses/5
        [HttpDelete("{id}")]
        public CarClass DeleteCarClass(int id)
        {
            var carClass =  _context.CarClasses.Find(id);
            if (carClass == null)
            {
                return null;
            }

            _context.CarClasses.Remove(carClass);
            _context.SaveChanges();

            return carClass;
        }

        private bool CarClassExists(string carclass)
        {
            return _context.CarClasses.Any(e => e.Class.Equals(carclass));
        }
        private bool CarClassExists(int id)
        {
            return _context.CarClasses.Any(e => e.Id == id);
        }
    }
}
