using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Serilog;

using tag_h.Core.Model;
using tag_h.Core.Persistence;

namespace tag_h.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ImageTagsController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IHImageRepository _imageRepository;
        private readonly ITagRepository _tagRepository;

        public ImageTagsController(ILogger logger, IHImageRepository imageRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            _imageRepository = imageRepository;
            _tagRepository = tagRepository;
        }

        [HttpGet]
        [Route("[action]")]
        public TagSet GetTags(int uuid)
        {
            var image = _imageRepository.FetchImages(new TagQuery { UUID = uuid }).First();
            var tags = _tagRepository.GetTagsForImage(image);
            _logger.Information("Fetching images {tags}", tags);
            return tags;
        }

        [HttpDelete]
        [Route("[action]")]
        public void DeleteTag(int uuid, string tagName)
        {
            var image = _imageRepository.FetchImages(new TagQuery { UUID = uuid }).First();
            var tag = _tagRepository.GetAllTags().Where(tag => tag.Value == tagName).First();// TODO Optimise

            _tagRepository.RemoveTagFromImage(image, tag);
        }

        [HttpPost]
        [Route("[action]")]
        public void AddTag(int uuid, string tagName)
        {
            var image = _imageRepository.FetchImages(new TagQuery { UUID = uuid }).First();
            var tag = _tagRepository.GetAllTags().Where(tag => tag.Value == tagName).First();// TODO Optimise

            _tagRepository.AddTagToImage(image, tag);
        }
    }
}
