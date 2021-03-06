﻿using System;
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
    public class CarBrandsController : ControllerBase
    {
        private readonly CarRentDBContext _context;

        public CarBrandsController(CarRentDBContext context)
        {
            _context = context;
        }

        // GET: api/CarBrands
        [HttpGet]
        public List<CarBrand> GetCarBrands()
        {
            return  _context.CarBrands.ToList();
        }

        // GET: api/CarBrands/5
        [HttpGet("{id}")]
        public CarBrand GetCarBrand(int id)
        {
            var carBrand =  _context.CarBrands.Find(id);

            if (carBrand == null)
            {
                return null;
            }

            return carBrand;
        }

        // PUT: api/CarBrands/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarBrand(int id, CarBrand carBrand)
        {
            if (id != carBrand.Id)
            {
                return BadRequest();
            }

            _context.Entry(carBrand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarBrandExists(id))
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

        // POST: api/CarBrands
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CarBrand>> PostCarBrand(CarBrand carBrand)
        {
            if (!CarBrandExists(carBrand.BrandName))
            {
                _context.CarBrands.Add(carBrand);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCarBrand", new { id = carBrand.Id }, carBrand);
            }

            return NoContent();
        }

        // DELETE: api/CarBrands/5
        [HttpDelete("{id}")]
        public CarBrand DeleteCarBrand(int id)
        {
            var carBrand =  _context.CarBrands.Find(id);
            if (carBrand == null)
            {
                return null;
            }

            _context.CarBrands.Remove(carBrand);
            _context.SaveChanges();

            return carBrand;
        }
        
        private bool CarBrandExists(string brandname)
        {
            return _context.CarBrands.Any(e => e.BrandName == brandname);
        }

        private bool CarBrandExists(int id)
        {
            return _context.CarBrands.Any(e => e.Id == id);
        }
    }
}
