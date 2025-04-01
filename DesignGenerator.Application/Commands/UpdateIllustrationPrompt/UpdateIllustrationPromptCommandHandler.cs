using DesignGenerator.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Commands.UpdateIllustrationPrompt
{
    class UpdateIllustrationPromptCommandHandler : ICommandHandler<UpdateIllustrationPromptCommand>
    {
        private IIllustartionRepository _illustartionRepository;

        public UpdateIllustrationPromptCommandHandler(IIllustartionRepository illustartionRepository)
        {
            _illustartionRepository = illustartionRepository;
        }

        public async Task Handle(UpdateIllustrationPromptCommand command)
        {
            await _illustartionRepository.UpdateIllustration(command.IllustrationPath, command.Prompt);

            return;
        }
    }
}
