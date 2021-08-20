using System.Collections.Generic;
using System.Linq;

using tag_h.Core.Helper.Extensions;
using tag_h.Core.Model;
using tag_h.Core.Persistence;
using tag_h.Injection;

namespace tag_h.Core.TagRetriever.TagSource
{
    [Injectable]
    public interface ITagMaterialiser
    {
        Tag GetOrCreateTag(string value);
    }

    public class TagMaterialiser : ITagMaterialiser
    {
        private readonly ITagRepository _tagRepository;

        private readonly object _singleMaterialiserLock = new();

        public TagMaterialiser(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public Tag GetOrCreateTag(string value)
        {
            lock (_singleMaterialiserLock)
            {
                var tagSearch = _tagRepository.SearchTag(value);
                return tagSearch ?? MaterialiseTag(value);
            }
        }

        private Tag MaterialiseTag(string value)
        {
            _tagRepository.CreateTag(CreateTagName(value), GetTagValues(value).Distinct());
            return _tagRepository.SearchTag(value);
        }

        private static string CreateTagName(string value) => value.ToLower().Split("_(")[0].Replace("_", " ");

        private static IEnumerable<string> GetTagValues(string value)
        {
            yield return value;
            yield return value.ToLower();
            yield return value.ToLower().Filter("(", ")");
        }
    }
}
