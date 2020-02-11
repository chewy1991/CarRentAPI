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
    public class CarTypesController : ControllerBase
    {
        private readonly CarRentDBContext _context;

        public CarTypesController(CarRentDBContext context)
        {
            _context = context;
        }

        // GET: api/CarTypes
        [HttpGet]
        public List<CarType> GetCarTypes()
        {
            return  _context.CarTypes.ToList();
        }

        // GET: api/CarTypes/5
        [HttpGet("{id}")]
        public CarType GetCarType(int id)
        {
            var carType =  _context.CarTypes.Find(id);

            if (carType == null)
            {
                return null;
            }

            return carType;
        }

        // PUT: api/CarTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarType(int id, CarType carType)
        {
            if (id != carType.Id)
            {
                return BadRequest();
            }

            _context.Entry(carType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarTypeExists(id))
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

        // POST: api/CarTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CarType>> PostCarType(CarType carType)
        {
            if (!CarTypeExists(carType.carType))
            {
                _context.CarTypes.Add(carType);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCarType", new { id = carType.Id }, carType);
            }

            return NoContent();
        }

        // DELETE: api/CarTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CarType>> DeleteCarType(int id)
        {
            var carType = await _context.CarTypes.FindAsync(id);
            if (carType == null)
            {
                return NotFound();
            }

            _context.CarTypes.Remove(carType);
            await _context.SaveChangesAsync();

            return carType;
        }
        private bool CarTypeExists(string cartype)
        {
            return _context.CarTypes.Any(e => e.carType.Equals(cartype));
        }
        private bool CarTypeExists(int id)
        {
            return _context.CarTypes.Any(e => e.Id == id);
        }
    }
}
