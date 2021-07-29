using Microsoft.AspNetCore.Mvc;

using tag_h.Core.Model;
using tag_h.Core.Persistence;

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
        public TagSet GetAllTags()
        {
            return _tagRepository.GetAllTags();
        }
    }
}
