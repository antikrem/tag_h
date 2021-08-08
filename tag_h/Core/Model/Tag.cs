using System;

namespace tag_h.Core.Model
{
    public class Tag : IComparable<Tag>
    {
        public string Name { get; }

        public Tag(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(Tag other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
