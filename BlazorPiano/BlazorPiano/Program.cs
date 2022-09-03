using System;
using System.Net.Http;
using System.Threading.Tasks;
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

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<PianoGateway>();
            builder.Services.AddSingleton<PianoService>();
            builder.Services.AddSingleton(
                new WebSocketApiConfig 
                { 
                    Uri = builder.Configuration["WebSocketApi:Uri"] ?? throw new ArgumentNullException("Invalid Websocket API Uri in configuration") 
                });

            await builder.Build().RunAsync();
        }
    }
}
