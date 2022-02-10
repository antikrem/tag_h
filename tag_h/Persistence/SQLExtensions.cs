using System.Collections.Generic;
using System.Data.SQLite;

using tag_h.Core.Model;
using tag_h.Persistence.Model;

namespace tag_h.Persistence
{
    static class SQLExtensions
    {
        public static string? GetStringOrNull(this SQLiteDataReader dataReader, int column)
            => dataReader.IsDBNull(column) ? null : dataReader.GetString(column);

        private static HFileState GetHFile(this SQLiteDataReader dataReader)
            => new(dataReader.GetInt32(0), dataReader.GetString(1));

        public static IEnumerable<HFileState> GetHFiles(this SQLiteDataReader dataReader)
        {
            while (dataReader.Read())
                yield return dataReader.GetHFile();
        }

        private static TagState GetTag(this SQLiteDataReader dataReader)
            => new(dataReader.GetInt32(0), (string)dataReader.GetValue(1));

        public static IEnumerable<TagState> GetTags(this SQLiteDataReader dataReader)
        {
            while (dataReader.Read())
                yield return dataReader.GetTag();
        }
    }
}
