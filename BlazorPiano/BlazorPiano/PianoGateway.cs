using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorPiano
{
    public class PianoGateway
    {
        private readonly IJSRuntime js;
        public PianoGateway(IJSRuntime js)
        {
            this.js = js;
        }

        public ValueTask SwitchToPiano() => InvokePianoAction("initPiano");

        public ValueTask SwitchToSinth() => InvokePianoAction("initSynth");

        public ValueTask PlayNote(string note) => InvokePianoAction("playNote", note);
        public ValueTask AttackNote(string note) => InvokePianoAction("attack", note);
        public ValueTask ReleaseNote(string note) => InvokePianoAction("release", note);
        private async ValueTask InvokePianoAction(string action, params object[] args)
        {
            await js.InvokeVoidAsync("PianoLib." + action, args);
        }
    }
}