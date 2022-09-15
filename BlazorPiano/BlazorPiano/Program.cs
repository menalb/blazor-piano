using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorPiano.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorPiano
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            
            builder.Services.AddScoped(sp => 
            new HttpClient 
            { 
                BaseAddress = new Uri(builder.Configuration["BlazorPianoApi:Uri"] ?? throw new ArgumentNullException("Invalid BlazorPiano API Uri in configuration")) 
            });

            builder.Services.AddScoped<PianoGateway>();
            builder.Services.AddScoped<ConnectedPlayersService>();
            builder.Services.AddSingleton<PianoService>();
            builder.Services.AddSingleton<PlayerService>();
            builder.Services.AddSingleton<BrowserResizeService>();            

            builder.Services.AddSingleton(
                new WebSocketApiConfig 
                { 
                    Uri = builder.Configuration["WebSocketApi:Uri"] ?? throw new ArgumentNullException("Invalid Websocket API Uri in configuration") 
                });

            await builder.Build().RunAsync();
        }
    }
}
