using tag_h.Core.Persistence;
using tag_h.Core.TagRetriever;

namespace tag_h.Core.Tasks
{
    public interface ITask
    {
        string TaskName { get; }

        //TODO, provide a central provider for these injections
        void Execute(IHImageRepository imageRepository, ITagRepository tagRepository, IImageHasher imageHasher, IAutoTagger autoTagger);
    }
}
