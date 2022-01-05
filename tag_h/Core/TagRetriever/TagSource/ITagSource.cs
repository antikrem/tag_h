using System.Collections.Generic;
using System.Threading.Tasks;

using EphemeralEx.Injection;

using tag_h.Core.Model;


namespace tag_h.Core.TagRetriever.TagSource
{
    [MultipleInjectable]
    public interface ITagSource
    {
        Task<IEnumerable<Tag>> RetrieveTags(HImage image);
    }
}
