using System;
using System.Collections.Generic;

using tag_h.Injection.Typing;
using tag_h.Persistence;
using tag_h.Persistence.Model;


namespace tag_h.Core.Model
{
    [UsedByClient]
    public class Tag : IComparable<Tag>
    {
        private readonly TagState _tagState;
        private readonly IDatabase _database;

        public TagState State => _tagState;

        public int Id => _tagState.Id;

        public string Name => _tagState.Name;

        public IEnumerable<string> Values => _database.GetValues(_tagState);

        public Tag(IDatabase database, TagState tagState)
        {
            _tagState = tagState;

            _database = database;
        }

        public override string ToString()
            => Name;

        public int CompareTo(Tag? other)
            => Id.CompareTo(other!.Id);
    }
}
