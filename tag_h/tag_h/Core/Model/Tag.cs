namespace tag_h.Core.Model
{
    public class Tag
    {
        public string Value { get; }

        public Tag(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
