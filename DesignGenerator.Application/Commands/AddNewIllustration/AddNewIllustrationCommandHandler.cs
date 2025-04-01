using DesignGenerator.Application.Interfaces;
using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Commands.AddNewIllustration
{
    public class AddNewIllustrationCommandHandler : ICommandHandler<AddNewIllustrationCommand>
    {
        private readonly IIllustartionRepository _repository;

        public AddNewIllustrationCommandHandler(IIllustartionRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(AddNewIllustrationCommand command)
        {
            var illustration = new Illustartion
            {
                Title = command.Title,
                Description = command.Description,
                IllustrationPath = command.IllustrationFolder,
            };

            _repository.AddNewIllustration(illustration);
            return Task.CompletedTask;
        }
    }
}
