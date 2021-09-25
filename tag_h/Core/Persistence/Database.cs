using System.IO;

using Serilog;

using tag_h.Injection;
using tag_h.Core.Persistence.Query;

namespace tag_h.Core.Persistence
{
    [Injectable]
    public interface IDatabase
    {
        DirectoryInfo ImageFolder { get; }

        T ExecuteQuery<T>(T query) where T : IQuery;
    }

    public class Database : IDatabase
    {
        private readonly ILogger _logger;
        ISQLCommandExecutor _commandExecutor;

        public DirectoryInfo ImageFolder
        {
            get
            {
                if (!Directory.Exists("Images/"))
                {
                    Directory.CreateDirectory("Images/");
                }
                return new DirectoryInfo("Images/");
            }
        }

        public Database(ILogger logger, IDatabaseConnection connection, ISQLCommandExecutor commandExecutor)
        {
            _logger = logger;
            _commandExecutor = commandExecutor;
        }

        public T ExecuteQuery<T>(T query) where T : IQuery
        {
            _logger.Verbose("Executing Query: {QueryName}", typeof(T).Name);
            query.Execute(_commandExecutor);
            _logger.Verbose("Executed Query: {QueryName}", typeof(T).Name);
            return query;
        }
    }
}