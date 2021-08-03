using tag_h.Core.Persistence;


namespace tag_h.Core.Tasks
{
    public interface ITask
    {
        string TaskName { get; }

        void Execute(IHImageRepository imageRepository, ITagRepository tagRepository);
    }
}
