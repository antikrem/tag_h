using System.Data.SQLite;

namespace tag_h.Persistence.Query
{
    class AddNewImageQuery : IQuery
    {
        private string _location;

        public AddNewImageQuery(string location)
        {
            _location = location;
        }

        public void Execute(SQLiteCommand command)
        {
            command.CommandText
                    = @"INSERT INTO Images (fileName, hash, tags, viewed, deleted) 
                        VALUES (@fileName, NULL, NULL, 0, 0);";
            command.Parameters.AddWithValue("@fileName", _location);

            command.ExecuteNonQuery();
        }
    }

}
