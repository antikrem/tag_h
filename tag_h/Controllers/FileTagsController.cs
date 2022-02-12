using System.Linq;

using Microsoft.AspNetCore.Mvc;

using tag_h.Core.Model;
using tag_h.Core.Repositories;
using tag_h.Persistence;


namespace tag_h.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FileTagsController : ControllerBase
    {
        private readonly IHFileRepository _fileRepository;
        private readonly ITagRepository _tagRepository;

        public FileTagsController(IHFileRepository fileRepository, ITagRepository tagRepository)
        {
            _fileRepository = fileRepository;
            _tagRepository = tagRepository;
        }

        [HttpDelete]
        [Route("[action]")]
        public void RemoveTag(int fileId, int tagId)
        {
            var file = _fileRepository.FetchFiles(new FileQuery { Id = fileId }).Single();
            var tag = file.Tags.Where(tag => tag.Id == tagId).Single();

            file.RemoveTag(tag);
        }

        [HttpPost]
        [Route("[action]")]
        public void AddTag(int fileId, int tagId)
        {
            var file = _fileRepository.FetchFiles(new FileQuery { Id = fileId }).First();
            var tag = _tagRepository.GetAllTags().Where(tag => tag.Id == tagId).Single();// TODO Optimise

            file.AddTag(tag);
        }
    }
}
