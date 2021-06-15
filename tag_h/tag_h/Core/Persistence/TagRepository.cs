using tag_h.Core.Model;
using tag_h.Core.Injection;
using tag_h.Core.Persistence.Query;


namespace tag_h.Core.Persistence
{
    [Injectable]
    public interface ITagRepository
    {
        TagSet GetAllTags();

        TagSet GetTagsForImage(HImage image);
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

        public TagSet GetTagsForImage(HImage image)
        {
            return _database.ExecuteQuery(new FetchTagsForImageQuery(image)).Result;
        }
    }
}
