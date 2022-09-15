namespace BlazorPiano.Model
{
    public class PlayerInfo
    {
        public PlayerInfo(string username, string color)
        {
            Username = username;
            Color = color;
        }

        public string Username { get; }
        public string Color { get; }
        public bool IsYou(string username) => username == Username;
    }
}