using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Messages
{
    public class VisualApiKeyChangedMessage : ValueChangedMessage<string>
    {
        public VisualApiKeyChangedMessage(string newKey) : base(newKey) { }
    }
}
