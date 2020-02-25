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
    public class CustomersController : ControllerBase
    {
        private readonly CarRentDBContext _context;

        public CustomersController(CarRentDBContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public List<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public Customer GetCustomer(int id)
        {
            var customer =  _context.Customers.Find(id);

            if (customer == null)
            {
                return null;
            }

            return customer;
        }
        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(string Lastname)
        {
            Customer customer = null;
            var customerlist = await _context.Customers.ToListAsync();
            foreach (var customers in customerlist)
            {
                if(!customer.Lastname.Equals(Lastname))
                    continue;

                customer = customers;
            }

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (!CustomerExists(customer))
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
            }

            return NoContent();

        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        private bool CustomerExists(Customer customer)
        {
            return _context.Customers.Any(e => e.Adress.Equals(customer.Adress)) && _context.Customers.Any(e => e.EMailAdress.Equals(customer.EMailAdress)) && _context.Customers.Any(e => e.Lastname.Equals(customer.Lastname)) && _context.Customers.Any(e => e.Firstname.Equals(customer.Firstname)) && _context.Customers.Any(e => e.Telephonenumber == customer.Telephonenumber);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
