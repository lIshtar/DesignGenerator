using DesignGenerator.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignGenerator.Infrastructure.DBEntities;

namespace DesignGenerator.Infrastructure.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        private DbSet<Message> _dbSet;
        private DbContext _context;
        public MessageRepository(ApplicationDbContext applicationDbContext)
        {
            _dbSet = applicationDbContext.Messages;
            _context = applicationDbContext;
        }

        public async Task AddAsync(Message entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(Message entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
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

        public async Task<Message> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(Message entity)
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
