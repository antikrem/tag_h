using System.Collections.Generic;
using System.Threading.Tasks;

using tag_h.Core.Model;
using tag_h.Core.TagRetriever.TagSource;
using tag_h.Injection;

namespace tag_h.Core.TagRetriever
{
    [Injectable]
    public interface ITagRetriever
    {
        Task<IEnumerable<Tag>> FetchTagValues(HImage image);
    }

    public class TagRetriever : ITagRetriever
    {
        private readonly ITagSource _tagSource;

        public TagRetriever(ITagSource tagSource)
        {
            _tagSource = tagSource;
            
        }

        public async Task<IEnumerable<Tag>> FetchTagValues(HImage image)
        {
            return await _tagSource.RetrieveTags(image);
        }
    }
}
