﻿using System.Collections.Generic;
using System.Data.SQLite;

using tag_h.Core.Helper.Extensions;
using tag_h.Core.Model;


namespace tag_h.Core.Persistence
{
    static class SQLExtensions
    {
        public static HImage GetHImage(this SQLiteDataReader dataReader)
        {
            return new HImage(
                    dataReader.GetInt32(0),
                    dataReader.GetString(1),
                    dataReader.IsDBNull(2) ? null : dataReader.GetString(2).ToHexULong()
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
