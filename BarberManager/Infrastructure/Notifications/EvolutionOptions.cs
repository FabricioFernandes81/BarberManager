namespace Infrastructure.Notifications;

public sealed class EvolutionOptions
{
    public const string SectionName = "Evolution";

    public string BaseUrl { get; set; } = string.Empty;

    public string GlobalApiKey { get; set; } = string.Empty;

    public int TimeoutSeconds { get; set; } = 30;
}