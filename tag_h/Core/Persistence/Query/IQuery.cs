namespace tag_h.Core.Persistence.Query
{
    public interface IQuery
    {
        void Execute(ISQLCommandExecutor commandExecutor);
    }

}
