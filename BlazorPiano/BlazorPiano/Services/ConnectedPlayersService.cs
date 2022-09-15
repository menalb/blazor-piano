using BlazorPiano.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorPiano.Services
{
    public class ConnectedPlayersService
    {
        private readonly HttpClient _httpClient;

        public ConnectedPlayersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PlayerInfo>> GetConnectedPlayers()
        {
            var players = await _httpClient.GetFromJsonAsync<GetPlayersResponse>("players");

            return players.players;
        }

        class GetPlayersResponse
        {
            public IEnumerable<PlayerInfo> players { get; set; }
        }
    }
}