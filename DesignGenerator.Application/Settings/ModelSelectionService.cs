using CommunityToolkit.Mvvm.Messaging;
using DesignGenerator.Application.Exceptions;
using DesignGenerator.Application.Messages;
using DesignGenerator.Domain.Interfaces.ImageGeneration;


namespace DesignGenerator.Application.Settings
{
    public class ModelSelectionService
    {
        private const string defaultImageModelConfig = "Models:DefaultImageModel";

        private readonly AppConfiguration _config;
        private readonly IMessenger _messenger;

        private IImageGenerationClient _selectedModel;
        private IEnumerable<IImageGenerationClient> _models;
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

        internal IEnumerable<IImageGenerationClient> Models
        {
            get => _models;
        }

        public ModelSelectionService(
            AppConfiguration configuration, 
            List<IImageGenerationClient> models, 
            IMessenger messenger)
        {
            _config = configuration;
            _models = models;
            _messenger = messenger;

            InitializeSelectedModel();
        }

        private void InitializeSelectedModel()
        {
            string selectedModelName = _config.GetRequiredValue(defaultImageModelConfig);

            _selectedModel = _models.FirstOrDefault(m => m.Name.Equals(selectedModelName, StringComparison.OrdinalIgnoreCase))
                ?? throw new ModelNotFoundException(selectedModelName);
        }
    }
}
