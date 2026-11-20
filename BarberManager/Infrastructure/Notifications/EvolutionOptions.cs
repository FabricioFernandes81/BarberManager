namespace Infrastructure.Notifications;

public sealed class EvolutionOptions
{
    public const string SectionName = "Evolution";

    public string BaseUrl { get; set; } = string.Empty;

    public string GlobalApiKey { get; set; } = string.Empty;

    public string WebhookUrl { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; } = 30;


    public string[] WebhookEvents { get; set; } =
   {
        "QRCODE_UPDATED",
        "MESSAGES_UPSERT",
        "MESSAGES_UPDATE",
        "SEND_MESSAGE",
        "CONNECTION_UPDATE"
    };
}