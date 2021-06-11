using System.Data.SQLite;

using tagh.Core.Model;

namespace tagh.Core.Persistence.Query
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
