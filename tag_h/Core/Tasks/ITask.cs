using System.Threading.Tasks;


namespace tag_h.Core.Tasks
{
    public interface ITask
    {
        string Name { get; }

        Task Run();
    }

    public interface ITask<TConfiguration>
    {
        string Name { get; }

        Task Run(TConfiguration configuration);
    }
}
