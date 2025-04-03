using AutoMapper;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure
{
    public class IllustrationRepositoryService : IRepositoryService<Illustration>
    {
        private IMapper _mapper;
        private IRepository<DBEntities.Illustration> _repository;

        public IllustrationRepositoryService(IMapper mapper, IRepository<DBEntities.Illustration> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task AddAsync(Illustration dto)
        {
            var illustration = _mapper.Map<DBEntities.Illustration>(dto);
            await _repository.AddAsync(illustration);
        }

        public async Task DeleteAsync(Illustration dto)
        {
            var illustration = _mapper.Map<DBEntities.Illustration>(dto);
            await _repository.DeleteAsync(illustration);
        }

        public async Task<IEnumerable<Illustration>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<Illustration>>(users); // Маппинг списка
        }

        public async Task<Illustration> GetByIdAsync(int id)
        {
            DBEntities.Illustration user = await _repository.GetByIdAsync(id);
            return _mapper.Map<Illustration>(user); // Конвертируем Entity в DTO
        }

        public async Task UpdateAsync(Illustration dto)
        {
            DBEntities.Illustration illustration = await _repository.GetByIdAsync(dto.Id);
            if (illustration == null) throw new Exception("Пользователь не найден");

            _mapper.Map(dto, illustration); // Обновляем данные из DTO в сущности
            await _repository.UpdateAsync(illustration);
        }
    }
}
