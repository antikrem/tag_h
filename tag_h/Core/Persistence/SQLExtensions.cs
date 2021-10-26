using System.Collections.Generic;
using System.Data.SQLite;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence
{
    static class SQLExtensions
    {
        public static string GetStringOrNull(this SQLiteDataReader dataReader, int column) 
            => dataReader.IsDBNull(column) ? null : dataReader.GetString(column);

        private static HImage GetHImage(this SQLiteDataReader dataReader) 
            => new HImage(
                    dataReader.GetInt32(0),
                    dataReader.GetString(1)
                );

        public static IEnumerable<HImage> GetHImages(this SQLiteDataReader dataReader)
        {
            while (dataReader.Read())
            {
                yield return dataReader.GetHImage();
            }
        }

        private static Tag GetTag(this SQLiteDataReader dataReader) 
            => new Tag(dataReader.GetInt32(0), (string)dataReader.GetValue(1));

        private static IEnumerable<Tag> GetTagEnumeration(this SQLiteDataReader dataReader)
        {
            while (dataReader.Read())
            {
                yield return dataReader.GetTag();
            }
        }

        public static TagSet GetTags(this SQLiteDataReader dataReader) 
            => new TagSet(dataReader.GetTagEnumeration());
    }
}
