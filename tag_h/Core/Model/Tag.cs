using System;

namespace tag_h.Core.Model
{
    public class Tag : IComparable<Tag>
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

        public int CompareTo(Tag other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}
