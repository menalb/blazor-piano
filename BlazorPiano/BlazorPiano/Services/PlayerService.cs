using BlazorPiano.Model;

namespace BlazorPiano.Services;

public class PlayerService
{

    public void LocalPlayerConnected(PlayerInfo localPlayer)
    {
        LocalConnectedPlayer = localPlayer;
    }

    public PlayerInfo LocalConnectedPlayer { get; private set; }
}