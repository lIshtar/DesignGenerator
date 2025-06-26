using CommunityToolkit.Mvvm.Messaging;
using DesignGenerator.Exceptions.Application;
using DesignGenerator.Application.Messages;
using DesignGenerator.Domain.Interfaces.ImageGeneration;


namespace DesignGenerator.Application.Settings
{
    /// <summary>
    /// Manages the current image generation model selection and provides a list of all available models.
    /// </summary>
    public class ModelSelectionSettings
    {
        private const string defaultImageModelConfig = "Models:DefaultImageModel";

        private readonly AppConfiguration _config;
        private readonly IMessenger _messenger;

        private IImageGenerationClient _selectedModel  = null!;
        private IEnumerable<IImageGenerationClient> _models;

        /// <summary>
        /// The currently selected image generation model.
        /// Sends a message when changed and persists the selection.
        /// </summary>
        internal IImageGenerationClient SelectedModel
        {
            get => _selectedModel;
            set
            {
                _selectedModel = value;
                _messenger.Send(new ModelSelectionChangedMessage(value));
                _config.SetValue(defaultImageModelConfig, value.Name);
            }
        }

        /// <summary>
        /// All available image generation models.
        /// </summary>
        internal IEnumerable<IImageGenerationClient> Models => _models;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelSelectionSettings"/> class,
        /// loading the default model from configuration.
        /// </summary>
        public ModelSelectionSettings(
            AppConfiguration configuration,
            IEnumerable<IImageGenerationClient> models,
            IMessenger messenger)
        {
            _config = configuration;
            _models = models;
            _messenger = messenger;

            InitializeSelectedModel();
        }

        public IImageGenerationParams LoadDefaultParameters() =>
            SelectedModel.DefaultParams;

        /// <summary>
        /// Loads the selected model from configuration, or throws if not found.
        /// </summary>
        private void InitializeSelectedModel()
        {
            string selectedModelName = _config.GetRequiredValue(defaultImageModelConfig);

            SelectedModel = _models.FirstOrDefault(m =>
                m.Name.Equals(selectedModelName, StringComparison.OrdinalIgnoreCase))
                ?? throw new ModelNotFoundException(selectedModelName);
        }
    }
}
