namespace BlazorPiano
{
    public class OnlineConfiguration
    {
        public OnlineConfiguration()
        {

        }

        public const string ConfigurationSettingName = "OnlineConfiguration";
        public bool IsOnline =>
            WebSocketConfiguration is not null && !string.IsNullOrEmpty(WebSocketConfiguration.Uri)
            &&
            BlazorPianoApi is not null && !string.IsNullOrEmpty(BlazorPianoApi.Uri);

        public WebSocketApiConfigOption WebSocketConfiguration { get; set; } = new WebSocketApiConfigOption();
        public BlazorPianoApiConfiguration BlazorPianoApi { get; set; } = new BlazorPianoApiConfiguration();
    }

    public class BlazorPianoApiConfiguration
    {
        public string Uri { get; set; } = "";
    }

    public class WebSocketApiConfigOption
    {
        public string Uri { get; set; } = "";
    }
}