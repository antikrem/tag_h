using EphemeralEx.Injection;

using tag_h.Persistence.Model;
using System.Collections.Generic;
using System.Linq;

using tag_h.Persistence;


namespace tag_h.Core.Model
{
    [Injectable]
    public interface ITagFactory
    {
        Tag Create(TagState state);

        TagSet Create(IEnumerable<TagState> states);
    }

    public class TagFactory : ITagFactory
    {
        private readonly IDatabase _database;

        public TagFactory(IDatabase database)
        {
            _database = database;
        }

        public Tag Create(TagState state)
            => new(_database, state);

        public TagSet Create(IEnumerable<TagState> states)
            => new(states.Select(this.Create));
    }
}
