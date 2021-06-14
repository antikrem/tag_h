using System.Data.SQLite;

namespace tagh.Core.Persistence.Query
{
    class RemoveDeletedImagesQuery : IQuery
    {

        public void Execute(SQLiteCommand command)
        {
            command.CommandText
                    = @"DELETE FROM Images
                        WHERE deleted = 1;";

            command.ExecuteNonQuery();
        }
    }

}
