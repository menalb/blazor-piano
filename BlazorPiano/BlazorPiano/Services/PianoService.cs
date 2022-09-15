using System;
using BlazorPiano.Model;

namespace BlazorPiano.Services
{
    public class PianoService
    {
        public Action<Note> OnAttackNote;
        public Action<Note> OnReleaseNote;

        public void AttackNote(Note note)
        {
            OnAttackNote?.Invoke(note);
        }

        public void ReleaseNote(Note note)
        {
            OnReleaseNote?.Invoke(note);
        }

        public Action<Note> OnPlayNote;
        public void PlayNote(Note note)
        {
            NotifyStateChanged(note);
        }

        private void NotifyStateChanged(Note note) => OnPlayNote?.Invoke(note);
    }
}