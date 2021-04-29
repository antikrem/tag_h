using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tag_h.Persistence.Query;

namespace tag_h.Persistence
{
    class ImageDatabase : IDisposable
    {

        private DatabaseConnection _connection;

        public DirectoryInfo ImageFolder {
            get
            {
                if (!Directory.Exists("Images/"))
                {
                    Directory.CreateDirectory("Images/");
                }
                return new DirectoryInfo("Images/");
            }
        }
        

        public ImageDatabase()
        {
            _connection = new DatabaseConnection();
        }

        public void Dispose()
        {
            _connection.Dispose();
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