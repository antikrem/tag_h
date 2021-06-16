using System.IO;

using tag_h.Injection;
using tag_h.Core.Persistence;
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

        private IDatabaseConnection _connection;

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


        public Database(IDatabaseConnection connection)
        {
            _connection = connection;
        }

        public T ExecuteQuery<T>(T query) where T : IQuery
        {
            using (var command = _connection.CreateCommand())
            {
                query.Execute(command);
            }

            return query;
        }
    }
}