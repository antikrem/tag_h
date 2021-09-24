using System;
using System.Data.SQLite;

using Serilog;

using tag_h.Core.Helper.Extensions;
using tag_h.Injection;

namespace tag_h.Core.Persistence
{
    [Injectable]
    public interface ISQLCommandExecutor
    {
        void ExecuteCommand(params Action<SQLiteCommand>[] command);
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

        public void ExecuteCommand(params Action<SQLiteCommand>[] querys)
        {
            using var command = _databaseConnection.CreateCommand();
            try
            {
                querys.ForEach(query => query(command));

                _logger
                    .ForContext("Command", command.CommandText)
                    .ForContext("Parameters", command.Parameters, true)
                    .Verbose("Executed SQL command");
            }
            catch (Exception e)
            {
                _logger
                    .ForContext("Command", command.CommandText)
                    .ForContext("Parameters", command.Parameters, true)
                    .Error(e, "Exception thrown while executing SQL command");
            }

        }
    }
}
