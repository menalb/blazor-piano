using Microsoft.JSInterop;
using System.Threading.Tasks;
using System;
using System.Security.Cryptography.X509Certificates;

namespace BlazorPiano.Services
{
    public class BrowserResizeService
    {
        public event EventHandler<Size> OnResize;
        public event EventHandler<PianoSize> OnPianoResize;

        private readonly IJSRuntime _js;

        public BrowserResizeService(IJSRuntime js)
        {
            _js = js;
        }
        public async void Init()
        {
            await _js.InvokeAsync<string>("browserResize.registerResizeCallback", DotNetObjectReference.Create(this));
        }

        public void Resize(int width, int height)
        {
            OnResize?.Invoke(this, new(width, height));
            OnPianoResize?.Invoke(this, GetPianoSize(new(width, height)));
        }

        public async Task<int> GetInnerHeight()
        {
            return await _js.InvokeAsync<int>("browserResize.getInnerHeight");
        }

        public async Task<int> GetInnerWidth()
        {
            return await _js.InvokeAsync<int>("browserResize.getInnerWidth");
        }

        public async Task<Size> GetSize()
        {
            return new(await GetInnerWidth(), await GetInnerHeight());
        }

        [JSInvokable]
        public void SetBrowserDimensions(int width, int height)
        {

            Resize(width, height);
        }

        public async Task<PianoSize> GetPianoSize()
        {
            return GetPianoSize(await GetSize());
        }

        public static PianoSize GetPianoSize(Size size) =>
            size switch
            {
                { Width: < 670 } => new PianoSize(4, 1),
                { Width: < 970 } => new PianoSize(3, 2),
                { Width: < 1300 } => new PianoSize(3, 3),
                { Width: < 1600 } => new PianoSize(3, 4),
                _ => new PianoSize(2, 5)
            };


        public record Size(int Width, int Height);
        public record PianoSize(int startingOctave, int nOctaves);
    }
}