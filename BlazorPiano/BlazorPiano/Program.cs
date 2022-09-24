using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorPiano.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BlazorPiano
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped<PianoGateway>();
            builder.Services.AddSingleton<PianoService>();
            builder.Services.AddSingleton<PlayerService>();
            builder.Services.AddScoped<ConnectedPlayersService>();
            builder.Services.AddSingleton<BrowserResizeService>();
            
            builder.Services
                .AddOptions<OnlineConfiguration>()
                .Bind(builder.Configuration.GetSection("OnlineConfiguration"));

            builder.Services.AddScoped(sp =>
            {
                var config = sp.GetService<IOptions<OnlineConfiguration>>();

                if (config is null) throw new ArgumentNullException(nameof(OnlineConfiguration));

                return config.Value.IsOnline ?
                new HttpClient
                {
                    BaseAddress = new Uri(config.Value.BlazorPianoApi.Uri)
                }
                : new HttpClient();
            });

            await builder.Build().RunAsync();
        }
    }
}
