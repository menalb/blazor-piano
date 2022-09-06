using Microsoft.JSInterop;
using System.Threading.Tasks;
using System;
using System.Runtime.CompilerServices;

namespace BlazorPiano
{
    public class BrowserResizeService
    {
        public event Func<Task> OnResize;

        private readonly IJSRuntime js;

        public BrowserResizeService(IJSRuntime js)
        {
            this.js = js;
         }

        [JSInvokable]
        public async Task OnBrowserResize()
        {
            await OnResize?.Invoke();
        }

        public async Task<int> GetInnerHeight()
        {
            return await js.InvokeAsync<int>("browserResize.getInnerHeight");
        }

        public async Task<int> GetInnerWidth()
        {
            return await js.InvokeAsync<int>("browserResize.getInnerWidth");
        }
    }
}