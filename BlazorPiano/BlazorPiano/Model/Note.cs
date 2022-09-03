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
    }

    public class NotifiedNote : Note
    {
        public NotifiedNote(string kind, string name, Octave octave, string color) : base(kind, name, octave)
        {
            Color = color;
        }

        public string Color { get; }
    }
}