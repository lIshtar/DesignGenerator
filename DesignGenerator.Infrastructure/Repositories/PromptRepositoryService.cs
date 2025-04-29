using AutoMapper;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Domain;

namespace DesignGenerator.Infrastructure.Repositories
{
    public class PromptRepositoryService : IRepositoryService<Prompt>
    {
        private IMapper _mapper;
        private IRepository<Database.DBEntities.Prompt> _repository;

        public PromptRepositoryService(IMapper mapper, IRepository<Database.DBEntities.Prompt> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task AddAsync(Prompt dto)
        {
            var prompt = _mapper.Map<Database.DBEntities.Prompt>(dto);
            await _repository.AddAsync(prompt);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Prompt dto)
        {
            var prompt = _mapper.Map<Database.DBEntities.Prompt>(dto);
            await _repository.DeleteAsync(prompt);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Prompt>> GetAllAsync()
        {
            var prompts = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<Prompt>>(prompts); // Маппинг списка
        }

        public async Task<Prompt> GetByIdAsync(int id)
        {
            Database.DBEntities.Prompt prompt = await _repository.GetByIdAsync(id);
            return _mapper.Map<Prompt>(prompt); // Конвертируем Entity в DTO
        }

        public async Task UpdateAsync(Prompt dto)
        {
            Database.DBEntities.Prompt prompt = await _repository.GetByIdAsync(dto.Id);
            if (prompt == null) throw new Exception("Пользователь не найден");

            _mapper.Map(dto, prompt); // Обновляем данные из DTO в сущности
            await _repository.UpdateAsync(prompt);
            await _repository.SaveChangesAsync();
        }
    }
}
