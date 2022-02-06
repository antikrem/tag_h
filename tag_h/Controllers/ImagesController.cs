using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Serilog;

using tag_h.Core.Model;
using tag_h.Core.Repositories;
using tag_h.Core.Tasks;
using tag_h.Injection.Typing;
using tag_h.Persistence;

namespace tag_h.Controllers
{
    [UsedByClient]
    public class ImageViewModel
    {
        public int Id { get; }
        public string Location { get; }
        public IEnumerable<Tag> Tags { get; }

        public ImageViewModel(HFile file, TagSet tags)
        {
            Id = file.Id;
            Location = file.Location;
            Tags = tags;
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IHFileRepository _fileRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPhysicalImageProvider _physicalImageProvider;
        private readonly ITaskRunner _taskRunner;

        public ImagesController(
                ILogger logger,
                IHFileRepository fileRepository, 
                ITagRepository tagRepository, 
                IPhysicalImageProvider physicalImageProvider,
                ITaskRunner taskRunner
            )
        {
            _logger = logger;
            _fileRepository = fileRepository;
            _tagRepository = tagRepository;
            _physicalImageProvider = physicalImageProvider;
            _taskRunner = taskRunner;
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<ImageViewModel> GetAll()
        {
            var images = _fileRepository.FetchFiles(FileQuery.All);
            _logger.Information("Fetching images {list}", images);
            return images.Select(CreateViewModel);
        }

        [IgnoredByClient]
        [HttpGet]
        [Route("[action]")]
        public FileStreamResult GetFile(int imageId)
        {
            var image = _fileRepository.FetchFiles(FileQuery.All with { Id = imageId }).First();
            return File(image.Stream, "image/jpeg");
        }

        [HttpPost]
        [Route("[action]")]
        public void AddImages(List<SubmittedFile> files)
        {
            _taskRunner.Execute<AddNewImages, AddNewImagesConfiguration>(new(files));
        }

        //private ImageViewModel CreateViewModel(HFile file) TODO
        //   => new(file, _tagRepository.GetTagsForFile(file));

        private ImageViewModel CreateViewModel(HFile file)
           => new(file, TagSet.Empty);
    }
}
