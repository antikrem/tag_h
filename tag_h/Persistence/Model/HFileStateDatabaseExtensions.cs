using System.Collections.Generic;

using tag_h.Core.Model;
using tag_h.Core.Persistence.Query;

namespace tag_h.Persistence.Model
{
    static class HFileStateDatabaseExtensions
    {
        static public HFileState AddNewFile(this IDatabase database, string fileName)
        {
            return database.ExecuteQuery(new AddNewFileQuery(fileName));
        }

        static public void SaveFile(this IDatabase database, HFileState file)
        {
            database.ExecuteQuery(new SaveFileQuery(file));
        }

        static public List<HFileState> FetchAllFiles(this IDatabase database, FileQuery query)
        {
            return database.ExecuteQuery(new FetchFilesQuery(query));
        }

        static public void DeleteFile(this IDatabase database, HFileState file)
        {
            database.ExecuteQuery(new DeleteFileQuery(file));
        }

        static public TagSet GetTagsForFile(this IDatabase database, HFileState file)
        {
            return database.ExecuteQuery(new FetchTagsForFileQuery(file));
        }

        static public void AddTagToFile(this IDatabase database, HFileState file, Tag tag)
        {
            database.ExecuteQuery(new AddFileTagQuery(file, tag));
        }

        static public void RemoveTagFromImage(this IDatabase database, HFileState file, Tag tag)
        {
            database.ExecuteQuery(new RemoveFileTagQuery(file, tag));
        }

        static public void RemoveDeletedFiles(this IDatabase database)
        {
            database.ExecuteQuery(new RemoveDeletedImagesQuery());
        }

        static public List<HFileState> GetDeletedFiles(this IDatabase database)
        {
            return database.ExecuteQuery(new FetchDeletedFilesQuery());
        }
    }
}