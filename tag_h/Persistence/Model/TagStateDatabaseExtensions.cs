using System.Collections.Generic;

using tag_h.Core.Persistence.Query;


namespace tag_h.Persistence.Model
{
    static class TagStateDatabaseExtensions
    {
        public static IEnumerable<TagState> GetAllTags(this IDatabase database)
        {
            return database.ExecuteQuery(new FetchAllTagsQuery());
        }

        public static TagState? SearchTag(this IDatabase database, string value)
        {
            return database.ExecuteQuery(new SearchTagQuery(value));
        }

        public static IEnumerable<string> GetValues(this IDatabase database, TagState tag)
        {
            return database.ExecuteQuery(new FetchTagValues(tag));
        }

        public static TagState CreateTag(this IDatabase database, string name)
        {
            return database.ExecuteQuery(new AddNewTagQuery(name));
        }

        // TODO: Add FileTagRepository
        public static IEnumerable<TagState> GetTagsForFile(this IDatabase database, HFileState file)
        {
            return database.ExecuteQuery(new FetchTagsForFileQuery(file));
        }

        public static bool AddTagToFile(this IDatabase database, HFileState file, TagState tag)
        {
            return database.ExecuteQuery(new AddFileTagQuery(file, tag));
        }

        public static void RemoveTagFromFile(this IDatabase database, HFileState file, TagState tag)
        {
            database.ExecuteQuery(new RemoveFileTagQuery(file, tag));
        }
    }
}