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

            command.CommandText
                    = @"SELECT * FROM Tags;";

            Result = command.ExecuteReader().GetTags();
        }

    }
}
