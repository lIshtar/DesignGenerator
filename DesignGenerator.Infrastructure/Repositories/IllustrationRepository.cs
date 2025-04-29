using DesignGenerator.Application.Interfaces;
using DesignGenerator.Infrastructure.DBEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure.Database
{
    class IllustrationRepository : IRepository<Illustration>
    {
        private DbSet<Illustration> _dbSet;
        private DbContext _context;
        public IllustrationRepository(ApplicationDbContext applicationDbContext)
        {
            _dbSet = applicationDbContext.Illustrations;
            _context = applicationDbContext;
        }

        public async Task AddAsync(Illustration entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(Illustration entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<Illustration>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
                throw;
            }
        }

        public async Task<Illustration> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(Illustration entity)
        {
            _dbSet.Attach(entity);  // Подключаем сущность, если она еще не отслеживается
            _context.Entry(entity).State = EntityState.Modified;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
