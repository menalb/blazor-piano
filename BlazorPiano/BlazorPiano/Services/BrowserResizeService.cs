using Microsoft.JSInterop;
using System.Threading.Tasks;
using System;

namespace BlazorPiano.Services
{
    public class BrowserResizeService
    {
        public event EventHandler<Size> OnResize;        

        private readonly IJSRuntime _js;

        public BrowserResizeService(IJSRuntime js)
        {
            _js = js;
        }
        public async void Init(object component)
        {
            await _js.InvokeAsync<string>("browserResize.registerResizeCallback", DotNetObjectReference.Create(component));
        }       

        public void Resize(int width, int height)
        {
            OnResize?.Invoke(this, new(width,height));
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

        public record Size(int Width, int Height);
    }    
}