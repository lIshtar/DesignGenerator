using DBEntities = DesignGenerator.Infrastructure.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Domain;

namespace DesignGenerator.Infrastructure.Repositories
{
    public class MessageRepositoryService : IRepositoryService<Message>
    {
        private IMapper _mapper;
        private IRepository<DBEntities.Message> _repository;

        public MessageRepositoryService(IMapper mapper, IRepository<DBEntities.Message> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task AddAsync(Message dto)
        {
            var message = _mapper.Map<DBEntities.Message>(dto);
            await _repository.AddAsync(message);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Message dto)
        {
            var message = _mapper.Map<DBEntities.Message>(dto);
            await _repository.DeleteAsync(message);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            var messages = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<Message>>(messages); // Маппинг списка
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            DBEntities.Message message = await _repository.GetByIdAsync(id);
            return _mapper.Map<Message>(message); // Конвертируем Entity в DTO
        }

        public async Task UpdateAsync(Message dto)
        {
            DBEntities.Message message = await _repository.GetByIdAsync(dto.Id);
            if (message == null) throw new Exception("Пользователь не найден");

            _mapper.Map(dto, message); // Обновляем данные из DTO в сущности
            await _repository.UpdateAsync(message);
            await _repository.SaveChangesAsync();
        }
    }
}
