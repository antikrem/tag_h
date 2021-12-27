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
        private readonly IHImageRepository _imageRepository;
        private readonly ITagRepository _tagRepository;

        public ImageTagsController(IHImageRepository imageRepository, ITagRepository tagRepository)
        {
            _imageRepository = imageRepository;
            _tagRepository = tagRepository;
        }

        [HttpDelete]
        [Route("[action]")]
        public void RemoveTag(int imageId, int tagId)
        {
            var image = _imageRepository.FetchImages(new ImageQuery { Id = imageId }).First();
            var tag = _tagRepository.GetAllTags().Where(tag => tag.Id == tagId).First();// TODO Optimise

            _tagRepository.RemoveTagFromImage(image, tag);
        }

        [HttpPost]
        [Route("[action]")]
        public void AddTag(int id, int tagId)
        {
            var image = _imageRepository.FetchImages(new ImageQuery { Id = id }).First();
            var tag = _tagRepository.GetAllTags().Where(tag => tag.Id == tagId).First();// TODO Optimise

            _tagRepository.AddTagToImage(image, tag);
        }
    }
}
