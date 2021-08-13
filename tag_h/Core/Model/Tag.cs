using System;

namespace tag_h.Core.Model
{
    public class Tag : IComparable<Tag>
    {
        public int Id { get; }

        public string Name { get; }

        public Tag(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(Tag other)
        {
            return Id.CompareTo(other.Id);
        }
    }
}
