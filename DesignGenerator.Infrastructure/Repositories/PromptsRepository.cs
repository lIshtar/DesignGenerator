using DesignGenerator.Application.Interfaces;
using DesignGenerator.Infrastructure.Database.DBEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure.Repositories
{
    public class PromptsRepository : IRepository<Prompt>
    {
        private DbSet<Prompt> _dbSet;
        private DbContext _context;
        public PromptsRepository(ApplicationDbContext applicationDbContext)
        {
            _dbSet = applicationDbContext.Prompts;
            _context = applicationDbContext;
        }

        public async Task AddAsync(Prompt entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(Prompt entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<Prompt>> GetAllAsync()
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

        public async Task<Prompt> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(Prompt entity)
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
