﻿namespace BlazorPiano.Model
{
    public class PlayerInfo
    {
        public PlayerInfo(string username, string color, bool isYou)
        {
            Username = username;
            Color = color;
            IsYou = isYou;
        }

        public string Username { get; }
        public string Color { get; }
        public bool IsYou { get; }
    }
}