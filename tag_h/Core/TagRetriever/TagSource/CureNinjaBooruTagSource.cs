using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tag_h.Core.Model;
using tag_h.Core.Persistence;

namespace tag_h.Core.TagRetriever.TagSource
{

    public class CureNinjaBooruTagSource : ITagSource
    {
        private readonly IFetchHandler _fetchHandler;
        private readonly IImageHasher _imageHasher;

        private static string _url => "https://cure.ninja/booru/api/json/md5/";

        public CureNinjaBooruTagSource(IFetchHandler fetchHandler, IImageHasher imageHasher)
        {
            _fetchHandler = fetchHandler;
            _imageHasher = imageHasher;
        }

        public async Task<IEnumerable<string>> RetrieveTags(HImage image)
        {
            var hash = _imageHasher.GetHash(image).FileHash;
            var response = await _fetchHandler.FetchAsync<CureNinjaResponse>($"{_url}{hash}");

            return response.Success ? ResolveResults(response.Results) : Enumerable.Empty<string>();
        }

        private static IEnumerable<string> ResolveResults(IEnumerable<CureNinjaResult> results) 
            => results
                .SelectMany(result => result.tag.Split(" "))
                .Distinct();

        private record CureNinjaResponse(bool Success, IEnumerable<CureNinjaResult> Results);

        private record CureNinjaResult(string tag);
    }
}