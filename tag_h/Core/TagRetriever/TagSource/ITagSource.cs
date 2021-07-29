using System.Threading.Tasks;

using tag_h.Core.Model;


namespace tag_h.Core.TagRetriever.TagSource
{

    public interface ITagSource
    {

        Task<TagSet> RetrieveTags();
    }
}
