namespace tagh.Core.Model
{
    public class Tag
    {
        public string Value { get; }

        public Tag(string value)
        {
            Value = value;
            Value.ToString();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
