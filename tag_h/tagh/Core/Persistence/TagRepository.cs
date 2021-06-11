using System;

using tagh.Core.Injection;
using tagh.Core.Persistence.Query;
using tagh.Core.Model;

namespace tagh.Core.Persistence
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
