using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CarRentApi.Repositories.Database
{
    public class DatabaseRepository<T> : IRepository<T> where T : class, IDBTable
    {
        private readonly CarRentDBContext _dbContext;
        public DatabaseRepository(CarRentDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<T>> GetAllAsync()
        {
            return _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public Task<T> GetAsync(int id)
        {
            return _dbContext.Set<T>().FindAsync(id).AsTask();
        }

        public async Task<T> AddAsync(T obj)
        {
            await _dbContext.Set<T>().AddAsync(obj);
            await _dbContext.SaveChangesAsync();

            return obj;
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await GetAsync(id);
            _dbContext.Set<T>().Remove(obj);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T obj)
        {
            _dbContext.Set<T>().Update(obj);
            await _dbContext.SaveChangesAsync();
        }

    }
}
