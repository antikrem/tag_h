using System.IO;
using System.Net;
using System.Threading.Tasks;

using tag_h.Core.Helper.Extensions;
using tag_h.Core.Model;
using tag_h.Injection;

namespace tag_h.Core.TagRetriever
{
    [Injectable]
    public interface IAutoTagger
    {
        Task TagImage(HImage image);
    }

    public class AutoTagger : IAutoTagger
    {
        private readonly ITagRetriever _tagRetriever;

        public AutoTagger(ITagRetriever tagRetriever)
        {
            _tagRetriever = tagRetriever;
        }

        public async Task TagImage(HImage image)
        {
            var tags = _tagRetriever.FetchTagValues(image);
        }
    }
}
