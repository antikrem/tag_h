using System.Collections.Generic;

using EphemeralEx.Extensions;
using EphemeralEx.Injection;

using tag_h.Core.Model;
using tag_h.Core.Persistence.Query;
using tag_h.Persistence.Model;

namespace tag_h.Persistence
{
    [Injectable]
    public interface ITagRepository
    {
        TagSet GetAllTags();

        Tag? SearchTag(string value);

        IEnumerable<string> GetValues(Tag tag);

        Tag CreateTag(string name, IEnumerable<string> values);

        TagSet GetTagsForFile(HFileState file);

        bool AddTagToFile(HFileState file, Tag tag);

        void RemoveTagFromFile(HFileState file, Tag tag);
    }

    public class TagRespository : ITagRepository
    {
        private readonly IDatabase _database;

        public TagRespository(IDatabase database)
        {
            _database = database;
        }

        public TagSet GetAllTags()
        {
            return _database.ExecuteQuery(new FetchAllTagsQuery());
        }

        public Tag? SearchTag(string value)
        {
            return _database.ExecuteQuery(new SearchTagQuery(value));
        }

        public IEnumerable<string> GetValues(Tag tag)
        {
            return _database.ExecuteQuery(new FetchTagValues(tag));
        }

        public Tag CreateTag(string name, IEnumerable<string> values)
        {
            //TODO: make query return new tag
            _database.ExecuteQuery(new AddNewTagQuery(name));
            var tag = _database.ExecuteQuery(new SearchTagQuery(name))!;
            values.ForEach(
                    value => _database.ExecuteQuery(new AddTagValue(tag, value))
                );
            return tag;
        }

        // TODO: Add FileTagRepository
        public TagSet GetTagsForFile(HFileState file)
        {
            return _database.ExecuteQuery(new FetchTagsForFileQuery(file));
        }

        public bool AddTagToFile(HFileState file, Tag tag)
        {
            return _database.ExecuteQuery(new AddFileTagQuery(file, tag));
        }

        public void RemoveTagFromFile(HFileState file, Tag tag)
        {
            _database.ExecuteQuery(new RemoveFileTagQuery(file, tag));
        }
    }
}
