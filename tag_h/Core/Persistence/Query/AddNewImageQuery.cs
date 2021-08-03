using System.Data.SQLite;


namespace tag_h.Core.Persistence.Query
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
                    = @"INSERT INTO Images (fileName, hash, deleted) 
                        VALUES (@fileName, NULL, 0);";
            command.Parameters.AddWithValue("@fileName", _location);

            command.ExecuteNonQuery();
        }
    }

}
