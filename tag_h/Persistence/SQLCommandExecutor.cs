using System;
using System.Data.SQLite;

using EphemeralEx.Extensions;
using EphemeralEx.Injection;
using Serilog;


namespace tag_h.Persistence
{
    public class SqlExecutionException : Exception
    {

        internal SqlExecutionException(Exception innerException)
            : base("An exception has occured executing an SQL command", innerException)
        { }

    }

    [Injectable]
    public interface ISQLCommandExecutor
    {
        void ExecuteCommand(params Action<SQLiteCommand>[] querys);

        T ExecuteCommand<T>(Func<SQLiteCommand, T> query);
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

                throw new SqlExecutionException(e);
            }

        }

        public T ExecuteCommand<T>(Func<SQLiteCommand, T> query)
        {
            using var command = _databaseConnection.CreateCommand();
            try
            {
                var result = query(command);

                _logger
                    .ForContext("Command", command.CommandText)
                    .ForContext("Parameters", command.Parameters, true)
                    .Verbose("Executed SQL command");

                return result;
            }
            catch (Exception e)
            {
                _logger
                    .ForContext("Command", command.CommandText)
                    .ForContext("Parameters", command.Parameters, true)
                    .Error(e, "Exception thrown while executing SQL command");

                throw new SqlExecutionException(e);
            }
        }
    }
}
