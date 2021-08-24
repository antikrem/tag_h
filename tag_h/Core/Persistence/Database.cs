using System.IO;

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

        public Database(IDatabaseConnection connection, ISQLCommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        public T ExecuteQuery<T>(T query) where T : IQuery
        {
            query.Execute(_commandExecutor);
            return query;
        }
    }
}