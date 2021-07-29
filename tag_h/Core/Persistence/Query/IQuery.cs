using System.Data.SQLite;


namespace tag_h.Core.Persistence.Query
{
    public interface IQuery
    {
        void Execute(SQLiteCommand command);
    }

}
