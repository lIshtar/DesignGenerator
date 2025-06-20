using CommunityToolkit.Mvvm.Messaging;
using DesignGenerator.Application.Interfaces.ImageGeneration;
using DesignGenerator.Application.Messages;
using DesignGenerator.Application.Settings;
using DesignGenerator.Domain.Interfaces.ImageGeneration;
using DesignGenerator.Exceptions.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.ImageGeneration
{
    /// <summary>
    /// Responsible for holding and providing the currently selected image generation client.
    /// Listens for model selection changes via the message bus.
    /// </summary>
    public class SettingsClientSelector : IClientSelector
    {
        private IImageGenerationClient? _client;

        /// <summary>
        /// Initializes the selector with an initial client and registers for model selection changes.
        /// </summary>
        /// <param name="client">The initially selected image generation client.</param>
        /// <param name="messenger">The messenger used for inter-component communication.</param>
        public SettingsClientSelector(IImageGenerationClient client, IMessenger messenger)
            : this(messenger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Initializes the selector and registers for model selection changes.
        /// </summary>
        /// <param name="messenger">The messenger used to listen for selection changes.</param>
        public SettingsClientSelector(IMessenger messenger)
        {
            if (messenger == null)
                throw new ArgumentNullException(nameof(messenger));

            // Register to receive messages when model selection changes
            messenger.Register<ModelSelectionChangedMessage>(this, (_, message) => ReloadClient(message.Value));
        }

        /// <summary>
        /// Updates the current image generation client based on a received selection message.
        /// </summary>
        /// <param name="client">The newly selected client.</param>
        private void ReloadClient(IImageGenerationClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Returns the currently selected image generation client.
        /// </summary>
        /// <returns>The selected client.</returns>
        /// <exception cref="ClientNotSelectedException">
        /// Thrown if no client has been selected prior to calling this method.
        /// </exception>
        public IImageGenerationClient SelectClient()
        {
            return _client ?? throw new ClientNotSelectedException(
                "No image generation client is currently selected. Ensure a model has been chosen.");
        }
    }
}
