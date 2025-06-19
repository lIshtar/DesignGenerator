using CommunityToolkit.Mvvm.Messaging;
using DesignGenerator.Application.Messages;


namespace DesignGenerator.Application.Settings
{
    public class ApiKeysService
    {
        private const string visualApiKeyConfig = "ApiKeys:Visual";
        private const string textApiKeyConfig = "ApiKeys:Text";

        private readonly AppConfiguration _config;
        private readonly IMessenger _messenger;

        private string _visualApiKey;
        private string _textApiKey;

        internal string VisualApiKey
        {
            get { return _visualApiKey; }
            set
            {
                _visualApiKey = value;
                _messenger.Send(new VisualApiKeyChangedMessage(value));
                _config.SetValue(visualApiKeyConfig, value);
            }
        }

        internal string TextApiKey
        {
            get { return _textApiKey; }
            set
            {
                _textApiKey = value;
                _messenger.Send(new TextApiKeyChangedMessage(value));
                _config.SetValue(textApiKeyConfig, value);
            }
        }

        public ApiKeysService(AppConfiguration config, IMessenger messenger)
        {
            _config = config;
            _messenger = messenger;

            InitializeKeys();
        }

        private void InitializeKeys()
        {
            _visualApiKey = _config.GetRequiredValue(visualApiKeyConfig);
            _textApiKey = _config.GetRequiredValue(textApiKeyConfig);
        }
    }
}
