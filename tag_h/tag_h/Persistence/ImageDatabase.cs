using System;
using System.IO;

using tag_h.Helper.Injection;
using tag_h.Persistence.Query;

namespace tag_h.Persistence
{
    [Injectable]
    public interface IImageDatabase : IDisposable
    {
        DirectoryInfo ImageFolder { get; }

        T ExecuteQuery<T>(T query) where T : IQuery;
    }

    public class ImageDatabase : IImageDatabase
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


        public ImageDatabase(IDatabaseConnection connection)
        {
            _connection = connection;
        }

        public void Dispose()
        {
            
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