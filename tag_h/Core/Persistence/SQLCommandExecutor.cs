using System;
using System.Data.SQLite;

using Serilog;

using tag_h.Injection;

namespace tag_h.Core.Persistence
{
    [Injectable]
    public interface ISQLCommandExecutor
    {
        void ExecuteCommand(Action<SQLiteCommand> command);
    }

    public class SQLCommandExecutor : ISQLCommandExecutor
    {
        private readonly ILogger _logger;
        private readonly IDatabaseConnection _databaseConnection;

        public SQLCommandExecutor(ILogger logger, IDatabaseConnection databaseConnection)
        {
            _logger = logger;
            _databaseConnection = databaseConnection;
        }

        public void ExecuteCommand(Action<SQLiteCommand> query)
        {
            using var command = _databaseConnection.CreateCommand();
            try
            {
                query(command);
                _logger
                    .ForContext("Command", command)
                    .Information("Executed SQL command");
            }
            catch (Exception e)
            {
                _logger
                    .ForContext("Exception", e)
                    .Error("Exception thrown while executing SQL command");
            }

        }
    }
}
