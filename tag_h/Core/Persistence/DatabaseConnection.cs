using Serilog;
using System.Data.SQLite;
using tag_h.Injection;

namespace tag_h.Core.Persistence
{
    [Injectable]
    public interface IDatabaseConnection : IStopOnDejection
    {
        SQLiteCommand CreateCommand();
    }

    internal class DatabaseConnection : IDatabaseConnection
    {

        private readonly SQLiteConnection _connection;
        private readonly ILogger _logger;


        public DatabaseConnection(ILogger logger)
        {
            _logger = logger;

            _connection = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True;");
            _connection.Open();

            CreateIfNotExistent();

            _logger.ForContext("Connection", _connection).Information("Connection Made");
        }

        private readonly string[] _initialiserScripts = new string[]
        {
            @"CREATE TABLE if not exists Images (
                    id INTEGER PRIMARY KEY ASC,
                    fileName STRING NOT NULL, 
                    fileHash STRING,
                    perceptualHash STRING,
                    deleted INTEGER
                );",
            @"CREATE TABLE if not exists Tags (
                    tag STRING NOT NULL PRIMARY KEY
                );",
            @"CREATE TABLE if not exists TagValues (
                    tag STRING NOT NULL,
                    value STRING NOT NULL,
                    PRIMARY KEY (tag, value)
                );",
            @"CREATE TABLE if not exists ImageTags (
                    id INTEGER NOT NULL,
                    tag STRING NOT NULL,
                    PRIMARY KEY (id, tag)
                );"
        };

        private void CreateIfNotExistent()
        {
            foreach (string query in _initialiserScripts)
            {
                using (var command = CreateCommand())
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
            }

        }

        public SQLiteCommand CreateCommand()
        {
            return _connection.CreateCommand();
        }

        public void Stop()
        {
            _connection.Close();
        }
    }
}
