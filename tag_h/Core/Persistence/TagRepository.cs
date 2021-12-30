using System.Collections.Generic;

using EphemeralEx.Extensions;
using EphemeralEx.Injection;

using tag_h.Core.Model;
using tag_h.Core.Persistence.Query;


namespace tag_h.Core.Persistence
{
    [Injectable]
    public interface ITagRepository
    {
        TagSet GetAllTags();

        Tag SearchTag(string value);

        IEnumerable<string> GetValues(Tag tag);

        Tag CreateTag(string name, IEnumerable<string> values);

        TagSet GetTagsForImage(HImage image);

        bool AddTagToImage(HImage image, Tag tag);
        
        void RemoveTagFromImage(HImage image, Tag tag);
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
            return _database.ExecuteQuery(new FetchAllTagsQuery()).Result;
        }

        public Tag SearchTag(string value)
        {
            return _database.ExecuteQuery(new SearchTagQuery(value)).Result;
        }

        public IEnumerable<string> GetValues(Tag tag)
        {
            return _database.ExecuteQuery(new FetchTagValues(tag)).Result;
        }

        public Tag CreateTag(string name, IEnumerable<string> values)
        {
            _database.ExecuteQuery(new AddNewTagQuery(name));
            var tag = _database.ExecuteQuery(new SearchTagQuery(name)).Result;
            values.ForEach(
                    value => _database.ExecuteQuery(new AddTagValue(tag, value))
                );
            return tag;
        }

        // TODO: Add ImageTagRepository
        public TagSet GetTagsForImage(HImage image)
        {
            return _database.ExecuteQuery(new FetchTagsForImageQuery(image)).Result;
        }

        public bool AddTagToImage(HImage image, Tag tag)
        {
            return _database.ExecuteQuery(new AddImageTagQuery(image, tag)).Success;
        }

        public void RemoveTagFromImage(HImage image, Tag tag)
        {
            _database.ExecuteQuery(new RemoveImageTagQuery(image, tag));
        }
    }
}
