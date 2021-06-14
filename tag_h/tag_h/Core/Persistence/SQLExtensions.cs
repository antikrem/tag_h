using System.Collections.Generic;
using System.Data.SQLite;

using tagh.Core.Helper.Extensions;
using tagh.Core.Model;

namespace tagh.Core.Persistence
{
    static class SQLExtensions
    {
        public static HImage GetHImage(this SQLiteDataReader dataReader)
        {
            return new HImage(
                    dataReader.GetInt32(0),
                    dataReader.GetString(1),
                    dataReader.IsDBNull(2) ? null : (ulong?)dataReader.GetString(2).ToHexULong()
                );
        }

        public static Tag GetTag(this SQLiteDataReader dataReader)
        {
            return new Tag(dataReader.GetString(0));
        }

        private static IEnumerable<Tag> GetTagEnumeration(this SQLiteDataReader dataReader)
        {
            while (dataReader.Read())
            {
                yield return dataReader.GetTag();
            }
        }

        public static TagSet GetTags(this SQLiteDataReader dataReader)
        {
            return new TagSet(dataReader.GetTagEnumeration());
        }
    }
}
