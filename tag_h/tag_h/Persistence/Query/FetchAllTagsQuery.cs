using System;
using System.Collections.Generic;
using System.Data.SQLite;

using tag_h.Model;

namespace tag_h.Persistence.Query
{
    class FetchAllTagsQuery : IQuery
    {
        public TagSet Result { get; private set; }

        public void Execute(SQLiteCommand command)
        {
            ISet<Model.Tag> tags = new SortedSet<Model.Tag>();

            command.CommandText
                    = @"SELECT * FROM Tags;";

            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                tags.Add(dataReader.GetTag());
            }

            Result = new TagSet(tags);
        }

    }
}
