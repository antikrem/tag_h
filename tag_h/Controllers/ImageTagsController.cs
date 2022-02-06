using System.Linq;

using Microsoft.AspNetCore.Mvc;

using tag_h.Core.Model;
using tag_h.Core.Repositories;
using tag_h.Persistence;


namespace tag_h.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ImageTagsController : ControllerBase
    {
        private readonly IHFileRepository _fileRepository;
        private readonly ITagRepository _tagRepository;

        public ImageTagsController(IHFileRepository fileRepository, ITagRepository tagRepository)
        {
            _fileRepository = fileRepository;
            _tagRepository = tagRepository;
        }

        [HttpDelete]
        [Route("[action]")]
        public void RemoveTag(int imageId, int tagId)
        {
            var image = _fileRepository.FetchFiles(new FileQuery { Id = imageId }).Single();
            var tag = image.Tags.Where(tag => tag.Id == tagId).Single();
            image.RemoveTag(tag);
        }

        [HttpPost]
        [Route("[action]")]
        public void AddTag(int id, int tagId)
        {
            var image = _fileRepository.FetchFiles(new FileQuery { Id = id }).First();
            var tag = _tagRepository.GetAllTags().Where(tag => tag.Id == tagId).First();// TODO Optimise

            //_tagRepository.AddTagToImage(image, tag); TODO
        }
    }
}
