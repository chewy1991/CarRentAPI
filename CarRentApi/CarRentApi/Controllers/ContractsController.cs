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
    public class ContractsController : ControllerBase
    {
        private readonly CarRentDBContext _context;

        public ContractsController(CarRentDBContext context)
        {
            _context = context;
        }

        // GET: api/Contracts
        [HttpGet]
        public List<ContractComplete> GetContracts()
        {
            var contractlist = _context.Contracts.ToList();
            List<ContractComplete> concomplete = new List<ContractComplete>();

            foreach (Contract con in contractlist)
            {
                concomplete.Add(new ContractComplete(_context,con));
            }

            return concomplete;
        }

        // GET: api/Contracts/5
        [HttpGet("{id}")]
        public ContractComplete GetContract(int id)
        {
            var contract =  _context.Contracts.Find(id);

            if (contract == null)
            {
                return null;
            }
            ContractComplete complcontract = new ContractComplete(_context,contract);

            return complcontract;
        }

        // PUT: api/Contracts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContract(int id, Contract contract)
        {
            if (id != contract.Id)
            {
                return BadRequest();
            }

            _context.Entry(contract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractExists(id))
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

        // POST: api/Contracts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Contract>> PostContract(Contract contract)
        {
            Reservation reservation = new Reservation();
            reservation = _context.Reservations.Find(contract.ReservationId);
            reservation.State = ReservationState.contracted;
            _context.Reservations.Update(reservation);
            _context.SaveChanges();
            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContract", new { id = contract.Id }, contract);
        }

        // DELETE: api/Contracts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contract>> DeleteContract(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();

            return contract;
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.Id == id);
        }
    }
}
