using System.Data.SQLite;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence.Query
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
