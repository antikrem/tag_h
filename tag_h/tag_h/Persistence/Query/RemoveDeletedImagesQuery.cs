using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using tag_h.Model;

namespace tag_h.Persistence.Query
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
