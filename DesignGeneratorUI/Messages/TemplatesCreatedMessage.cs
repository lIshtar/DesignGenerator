using CommunityToolkit.Mvvm.Messaging.Messages;
using DesignGenerator.Application;
using DesignGeneratorUI.ViewModels.ElementsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGeneratorUI.Messages
{
    /// <summary>
    /// Message class used to transmit a collection of illustration templates
    /// between view models or components via a messenger service.
    /// </summary>
    public class TemplatesCreatedMessage : ValueChangedMessage<IEnumerable<ImageGenerationRequestViewModel>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplatesGeneratedMessage"/> class.
        /// </summary>
        /// <param name="value">The collection of illustration templates to transmit.</param>
        public TemplatesCreatedMessage(IEnumerable<ImageGenerationRequestViewModel> value)
            : base(value) { }
    }

}
