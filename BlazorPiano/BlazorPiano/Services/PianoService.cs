using System;
using System.Threading.Tasks;
using BlazorPiano.Model;

namespace BlazorPiano.Services;

public class PianoService
{
    public event Func<Note,Task> OnAttackNoteAsync;
    public event Action<Note> OnAttackNote;

    public event Func<Note, Task> OnReleaseNoteAsync;
    public event Action<Note> OnReleaseNote;

    public event Func<Note,Task> OnToAttackNote;
    public event Func<Note,Task> OnToReleaseNote;

    public void NotifyAttackedNote(Note note)
    {        
        OnAttackNoteAsync?.Invoke(note);
        OnAttackNote?.Invoke(note);
    }

    public void NotifyReleasedNote(Note note)
    {
        OnReleaseNoteAsync?.Invoke(note);
        OnReleaseNote?.Invoke(note);
    }

    public void AttackNote(Note note)
    {
        OnToAttackNote?.Invoke(note);
    }

    public void ReleaseNote(Note note)
    {
        OnToReleaseNote?.Invoke(note);
    }

    [Obsolete]
    public Action<Note> OnPlayNote;
    public void PlayNote(Note note)
    {
        NotifyStateChanged(note);
    }

    private void NotifyStateChanged(Note note) => OnPlayNote?.Invoke(note);
}
