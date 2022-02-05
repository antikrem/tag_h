using System.IO;

using EphemeralEx.Injection;
using Serilog;

using tag_h.Core.Persistence.Query;
using tag_h.Helpers;

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
            using (_logger.LogPerformance(typeof(T).Name))
                query.Execute(_commandExecutor);
            return query;
        }
    }
}