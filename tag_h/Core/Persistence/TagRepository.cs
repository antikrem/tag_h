using tag_h.Injection;
using tag_h.Core.Model;
using tag_h.Core.Persistence.Query;


namespace tag_h.Core.Persistence
{
    [Injectable]
    public interface ITagRepository
    {
        TagSet GetAllTags();

        void CreateTag(Tag tag);

        TagSet GetTagsForImage(HImage image);

        void AddTagToImage(HImage image, Tag tag);
        
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
        public void CreateTag(Tag tag)
        {
            _database.ExecuteQuery(new AddNewTagQuery(tag));
        }

        // TODO: Add ImageTagRepository
        public TagSet GetTagsForImage(HImage image)
        {
            return _database.ExecuteQuery(new FetchTagsForImageQuery(image)).Result;
        }

        public void AddTagToImage(HImage image, Tag tag)
        {
            _database.ExecuteQuery(new AddImageTagQuery(image, tag));
        }

        public void RemoveTagFromImage(HImage image, Tag tag)
        {
            _database.ExecuteQuery(new RemoveImageTagQuery(image, tag));
        }
    }
}
