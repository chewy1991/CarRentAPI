using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentApi.Model;
using CarRentApi.Repositories;

namespace zbw.car.rent.api.Repositories.InMemory
{
    public class InMemoryRepository<T>: IRepository<T> where T : IDBTable
    {
        private List<T> _objs = new List<T>();

        public Task<List<T>> GetAllAsync()
        {
            return Task.Run(() => _objs);
        }

        public Task<T> GetAsync(int id)
        {
            return Task.Run(() => _objs.FirstOrDefault(c => c.Id == id));
        }

        public Task<T> AddAsync(T obj)
        {
            return Task.Run(() =>
            {
                obj.Id = _objs.Any() ? _objs.Max(c => c.Id) + 1 : 1;
                _objs.Add(obj);
                return obj;
            });
        }

        public Task RemoveAsync(int id)
        {
            return Task.Run(() =>
            {
                _objs.RemoveAll(o => o.Id == id);
            });
        }

        public async Task UpdateAsync(T obj)
        {
            var old = await GetAsync(obj.Id);

            await Task.Run(() =>
            {
                var index = _objs.IndexOf(old);
                _objs[index] = obj;
            });
        }
    }
}
