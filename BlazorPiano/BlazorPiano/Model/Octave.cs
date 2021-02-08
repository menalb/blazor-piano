namespace BlazorPiano.Model
{
    public class Octave
    {
        public Octave(int number)
        {
            Number = number;
        }
        public int Number { get; }

        public bool IsValid() => Number > 0;
    }
}