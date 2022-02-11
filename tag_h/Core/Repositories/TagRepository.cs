using System.Collections.Generic;

using EphemeralEx.Extensions;
using EphemeralEx.Injection;

using tag_h.Core.Model;
using tag_h.Persistence;
using tag_h.Persistence.Model;


namespace tag_h.Core.Repositories
{
    [Injectable]
    public interface ITagRepository
    {
        Tag CreateTag(string name, IEnumerable<string> values);

        TagSet GetAllTags();

        Tag? SearchTag(string value);
    }

    public class TagRespository : ITagRepository
    {
        private readonly IDatabase _database;
        private readonly ITagFactory _tagFactory;

        public TagRespository(IDatabase database, ITagFactory tagFactory)
        {
            _database = database;
            _tagFactory = tagFactory;
        }

        public Tag CreateTag(string name, IEnumerable<string> values)
        {
            var state = _database.CreateTag(name);
            values.ForEach(value => _database.AddValue(state, value));
            return _tagFactory.Create(state);
        }

        public TagSet GetAllTags()
        {
            return _tagFactory.Create(_database.GetAllTags());
        }

        public Tag? SearchTag(string value)
        {
            var state = _database.SearchTag(value);
            return state != null 
                ? _tagFactory.Create(state) 
                : null;
        }
    }
}
