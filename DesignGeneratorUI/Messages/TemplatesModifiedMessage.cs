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
    /// Message sent via messenger to notify subscribers
    /// that the collection of IllustrationTemplate objects has been modified.
    /// </summary>
    public class TemplatesModifiedMessage
    {
        /// <summary>
        /// Gets the updated collection of illustration templates.
        /// </summary>
        public IEnumerable<ImageGenerationRequestViewModel> Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplatesModifiedMessage"/> class.
        /// </summary>
        /// <param name="updatedTemplates">The updated collection of templates.</param>
        public TemplatesModifiedMessage(IEnumerable<ImageGenerationRequestViewModel> updatedTemplates)
        {
            Value = updatedTemplates?.ToList().AsReadOnly()
                               ?? throw new ArgumentNullException(nameof(updatedTemplates));
        }
    }
}
