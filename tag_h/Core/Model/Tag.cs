using System;

using tag_h.Injection.Typing;


namespace tag_h.Core.Model
{
    [UsedByClient]
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
