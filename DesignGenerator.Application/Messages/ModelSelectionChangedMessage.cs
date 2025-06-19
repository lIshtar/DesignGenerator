using CommunityToolkit.Mvvm.Messaging.Messages;
using DesignGenerator.Domain.Interfaces.ImageGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Messages
{
    public class ModelSelectionChangedMessage : ValueChangedMessage<IImageGenerationClient>
    {
        public ModelSelectionChangedMessage(IImageGenerationClient value) : base(value) { }
    }
}
