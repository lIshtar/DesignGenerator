using CommunityToolkit.Mvvm.Messaging.Messages;
using DesignGeneratorUI.ViewModels.ElementsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGeneratorUI.Messages
{
    public class RequestBatchCreatedMessage : ValueChangedMessage<IEnumerable<ImageGenerationRequestViewModel>>
    {
        public RequestBatchCreatedMessage(IEnumerable<ImageGenerationRequestViewModel> value)
            : base(value) { }
    }
}
