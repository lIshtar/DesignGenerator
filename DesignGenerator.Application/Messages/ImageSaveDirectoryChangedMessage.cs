using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Messages
{
    internal class ImageSaveDirectoryChangedMessage : ValueChangedMessage<string>
    {
        public ImageSaveDirectoryChangedMessage(string value) : base(value) { }
    }
}
