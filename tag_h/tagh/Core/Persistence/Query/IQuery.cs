using System;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tagh.Core.Persistence.Query
{
    public interface IQuery
    {
        void Execute(SQLiteCommand command);
    }

}
