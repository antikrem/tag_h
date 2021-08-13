using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Serilog;

using tag_h.Core.Model;
using tag_h.Core.Persistence;
using tag_h.Core.Tasks;

namespace tag_h.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IHImageRepository _imageRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPhysicalImageProvider _physicalImageProvider;
        private readonly ITaskRunner _taskRunner;

        public ImagesController(
                ILogger logger, 
                IHImageRepository imageRepository, 
                ITagRepository tagRepository, 
                IPhysicalImageProvider physicalImageProvider,
                ITaskRunner taskRunner
            )
        {
            _logger = logger;
            _imageRepository = imageRepository;
            _tagRepository = tagRepository;
            _physicalImageProvider = physicalImageProvider;
            _taskRunner = taskRunner;
        }

        [HttpGet]
        [Route("[action]")]
        public HImageList GetAll()
        {
            var images = _imageRepository.FetchImages(ImageQuery.All);
            _logger.Information("Fetching images {list}", images);
            return images;
        }


        [HttpGet]
        [Route("[action]")]
        public FileStreamResult GetFile(int imageId)
        {
            var image = _imageRepository.FetchImages(ImageQuery.All with { UUID = imageId }).First();
            var stream = _physicalImageProvider.LoadImageStream(image);
            return File(stream, "image/jpeg");
        }

        [HttpPost]
        [Route("[action]")]
        public void AddImages(List<SubmittedFile> files)
        {
            _taskRunner.Submit(new AddNewImages(files));
        }
    }
}
