using System.Threading.Tasks;

using EphemeralEx.Extensions;
using EphemeralEx.Injection;

using tag_h.Core.Model;
using tag_h.Persistence;

namespace tag_h.Core.TagRetriever
{
    [Injectable]
    public interface IAutoTagger
    {
        //Task TagImage(HImage image); TODO
    }

    public class AutoTagger : IAutoTagger
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagRetriever _tagRetriever;

        public AutoTagger(ITagRetriever tagRetriever, ITagRepository tagRepository)
        {
            _tagRetriever = tagRetriever;
            _tagRepository = tagRepository;
        }

        //public async Task TagImage(HImage image)
        //{
        //    var tags = await _tagRetriever.FetchTagValues(image);
        //    tags.ForEach(tag => _tagRepository.AddTagToImage(image, tag));
        //}
    }
}
