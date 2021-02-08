namespace BlazorPiano.Model
{
    public class Key
    {
        public Key(string kind, string name)
        {
            Kind = kind;
            Name = name;
        }
        public string Kind { get; }
        public string Name { get; }

        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Kind));
        }
    }
}