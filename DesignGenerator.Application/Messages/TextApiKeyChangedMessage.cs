using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Messages
{
    public class TextApiKeyChangedMessage : ValueChangedMessage<string>
    {
        public TextApiKeyChangedMessage(string newKey) : base(newKey) { }
    }
}
