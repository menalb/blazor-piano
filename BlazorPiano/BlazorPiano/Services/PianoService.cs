using System;
using System.Threading.Tasks;
using BlazorPiano.Model;

namespace BlazorPiano.Services;

public class PianoService
{
    public event Func<Note,Task> OnAttackNote;
    public event Func<Note, Task> OnReleaseNote;

    public event Func<Note,Task> OnToAttackNote;
    public event Func<Note,Task> OnToReleaseNote;

    public void NotifyAttackedNote(Note note)
    {        
        OnAttackNote?.Invoke(note);
    }

    public void NotifyReleasedNote(Note note)
    {
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
