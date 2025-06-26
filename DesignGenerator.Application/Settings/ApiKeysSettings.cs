using CommunityToolkit.Mvvm.Messaging;
using DesignGenerator.Application;
using DesignGenerator.Application.Messages;

/// <summary>
/// Manages the storage and notification of API keys used for visual and text-based AI services.
/// </summary>
public class ApiKeysSettings
{
    // Configuration keys used to retrieve or store values.
    private const string visualApiKeyConfig = "ApiKeys:Visual";
    private const string textApiKeyConfig = "ApiKeys:Text";

    private readonly AppConfiguration _config;
    private readonly IMessenger _messenger;

    private string _visualApiKey;
    private string _textApiKey;

    /// <summary>
    /// The API key for visual generation services (e.g., Stable Diffusion).
    /// Updates config and sends change notification.
    /// </summary>
    internal string VisualApiKey
    {
        get => _visualApiKey;
        set
        {
            _visualApiKey = value;
            _messenger.Send(new VisualApiKeyChangedMessage(value));
            _config.SetValue(visualApiKeyConfig, value);
        }
    }

    /// <summary>
    /// The API key for text-based services (e.g., OpenAI).
    /// Updates config and sends change notification.
    /// </summary>
    internal string TextApiKey
    {
        get => _textApiKey;
        set
        {
            _textApiKey = value;
            _messenger.Send(new TextApiKeyChangedMessage(value));
            _config.SetValue(textApiKeyConfig, value);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiKeysSettings"/> class,
    /// reading keys from configuration and registering messenger events.
    /// </summary>
    public ApiKeysSettings(AppConfiguration config, IMessenger messenger)
    {
        _config = config;
        _messenger = messenger;
        InitializeKeys();
    }

    /// <summary>
    /// Loads initial API keys from persistent configuration.
    /// </summary>
    private void InitializeKeys()
    {
        VisualApiKey = _config.GetRequiredValue(visualApiKeyConfig);
        TextApiKey = _config.GetRequiredValue(textApiKeyConfig);
    }
}