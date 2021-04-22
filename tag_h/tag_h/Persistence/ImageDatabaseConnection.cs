using System;
using System.Data.SQLite;

namespace tag_h.Persistence
{
    internal class ImageDatabaseConnection : IDisposable
    {

        private readonly SQLiteConnection _connection;

        public ImageDatabaseConnection()
        {
            _connection = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True;");
            _connection.Open();

            CreateIfNotExistent();
        }

        private void CreateIfNotExistent()
        {
            string imageDBQuery =
                @"CREATE TABLE if not exists Images (
                        id INTEGER PRIMARY KEY ASC,
                        fileName STRING NOT NULL, 
                        tags STRING, 
                        viewed INTEGER
                    );";
            using (var command = CreateCommand())
            {
                command.CommandText = imageDBQuery;
                command.ExecuteNonQuery();
            }
                
        }

        public SQLiteCommand CreateCommand()
        {
            return _connection.CreateCommand();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
