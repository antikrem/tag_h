using tag_h.Persistence;

namespace tag_h.Core.Persistence.Query
{
    public interface IQuery
    {
        void Execute(ISQLCommandExecutor commandExecutor);
    }

    public interface IQuery<T>
    {
        T Execute(ISQLCommandExecutor commandExecutor);
    }
}
