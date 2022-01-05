using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using EphemeralEx.Injection;
using EphemeralEx.Extensions;

using tag_h.Core.Model;
using tag_h.Core.TagRetriever.TagSource;


namespace tag_h.Core.TagRetriever
{
    [Injectable]
    public interface ITagRetriever
    {
        Task<IEnumerable<Tag>> FetchTagValues(HImage image);
    }

    public class TagRetriever : ITagRetriever
    {
        private readonly IEnumerable<ITagSource> _tagSources;

        public TagRetriever(IEnumerable<ITagSource> tagSources)
        {
            _tagSources = tagSources;
            
        }

        public async Task<IEnumerable<Tag>> FetchTagValues(HImage image)
        {
            var tagResult = await Task.WhenAll(_tagSources.Select(source => source.RetrieveTags(image)));
            return tagResult
                .Flatten()
                .DistinctBy(tag => tag.Id);
        }
    }
}
