using System.Collections.Generic;
using System.Linq;

using EphemeralEx.Extensions;
using Microsoft.AspNetCore.Mvc;

using tag_h.Core.Model;
using tag_h.Injection.Typing;
using tag_h.Persistence;

namespace tag_h.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

        public TagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        [Route("[action]")]
        [ClientReturns(typeof(IEnumerable<Tag>))]
        public TagSet GetAllTags()
        {
            return _tagRepository.GetAllTags();
        }

        [HttpGet]
        [Route("[action]")]
        public Tag CreateTag(string name)
        {
            return _tagRepository.CreateTag(name, name.ToLower().ToEnumerable());
        }

        [HttpGet]
        [Route("[action]")]
        public List<string> GetValues(int tagId)
        {
            // TODO: Optimise
            var tag = _tagRepository
                .GetAllTags()
                .Where(tag => tag.Id == tagId)
                .First();
            return _tagRepository.GetValues(tag).ToList();
        }
    }
}
