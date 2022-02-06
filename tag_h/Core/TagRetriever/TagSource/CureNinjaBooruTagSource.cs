using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using tag_h.Core.Model;
using tag_h.Persistence;
using tag_h.Persistence.Model;


namespace tag_h.Core.TagRetriever.TagSource
{

    public class CureNinjaBooruTagSource : ITagSource
    {
        private readonly ITagMaterialiser _tagMaterialiser;
        private readonly IFetchHandler _fetchHandler;
        private readonly IFileHasher _imageHasher;

        private static string _url => "https://cure.ninja/booru/api/json/md5/";

        public CureNinjaBooruTagSource(ITagMaterialiser tagMaterialiser, IFetchHandler fetchHandler, IFileHasher imageHasher)
        {
            _tagMaterialiser = tagMaterialiser;
            _fetchHandler = fetchHandler;
            _imageHasher = imageHasher;
        }

        public async Task<IEnumerable<Tag>> RetrieveTags(HFileState file)
        {
            var tags = await RetrieveTagsFromApi(file);
            return tags.Select(_tagMaterialiser.GetOrCreateTag);
        }

        private async Task<IEnumerable<string>> RetrieveTagsFromApi(HFileState file)
        {
            var hash = _imageHasher.GetHash(file).DataHash;
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