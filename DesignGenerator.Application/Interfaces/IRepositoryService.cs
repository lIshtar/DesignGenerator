using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Interfaces
{
    public interface IRepositoryService<T>
    {
        public Task<T> GetByIdAsync(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task AddAsync(T dto);
        public Task UpdateAsync(T dto);
        public Task DeleteAsync(T dto);
    }
}
