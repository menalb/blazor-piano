using System;
using BlazorPiano.Model;

namespace BlazorPiano
{
    public class PianoService
    {
        public Action<Note> OnPlayNote;
        public void PlayNote(Note note)
        {
            NotifyStateChanged(note);
        }

        private void NotifyStateChanged(Note note) => OnPlayNote?.Invoke(note);
    }
}