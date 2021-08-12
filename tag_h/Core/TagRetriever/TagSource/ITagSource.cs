using System.Collections.Generic;
using System.Threading.Tasks;

using tag_h.Core.Model;
using tag_h.Injection;

namespace tag_h.Core.TagRetriever.TagSource
{
    [Injectable]
    public interface ITagSource
    {

        Task<IEnumerable<string>> RetrieveTags(HImage image);
    }
}
