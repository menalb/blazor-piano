using System;

namespace BlazorPiano.Model
{
    public class Note
    {
        public Note(string kind, string name, Octave octave)
        {
            Kind = kind;
            Name = name;
            Octave = octave;
        }
        public string Kind { get; }
        public string Name { get; }
        public Octave Octave { get; }

        public bool IsValid() => !(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Kind)) && IsOctaveValid();
        private bool IsOctaveValid() => Octave is not null && Octave.IsValid();

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Note note = obj as Note;
            if (note == null) return false;

            return Name == note.Name && Octave.Number == note.Octave.Number;
        }

        public override string ToString()
        {
            return $"{Name}, {Octave.Number})";
        }
    }

    public class NotifiedNote : Note
    {
        public NotifiedNote(string kind, string name, Octave octave, string color, string username) : base(kind, name, octave)
        {
            Color = color;
            Username = username;
        }

        public string Color { get; }
        public string Username { get; }
    }

    public class ReplayNote : Note
    {
        public ReplayNote(string kind, string name, Octave octave) : base(kind, name, octave)
        {
        }
    }
}