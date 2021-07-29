using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Serilog;

using tag_h.Core.Model;
using tag_h.Core.Persistence;

namespace tag_h.Controllers
{
    public record SubmittedFile(string Data, string FileName, List<Tag> Tags);

    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IHImageRepository _imageRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPhysicalImageProvider _physicalImageProvider;

        public ImagesController(ILogger logger, IHImageRepository imageRepository, ITagRepository tagRepository, IPhysicalImageProvider physicalImageProvider)
        {
            _logger = logger;
            _imageRepository = imageRepository;
            _tagRepository = tagRepository;
            _physicalImageProvider = physicalImageProvider;
        }

        [HttpGet]
        [Route("[action]")]
        public HImageList GetAll()
        {
            var images = _imageRepository.FetchImages(TagQuery.All);
            _logger.Information("Fetching images {list}", images);
            return images;
        }


        [HttpGet]
        [Route("[action]")]
        public FileStreamResult GetFile(int imageId)
        {
            var image = _imageRepository.FetchImages(TagQuery.All with { UUID = imageId }).First();
            var stream = _physicalImageProvider.LoadImageStream(image);
            return File(stream, "image/jpeg");
        }

        [HttpPost]
        [Route("[action]")]
        public void AddImages(List<SubmittedFile> files)
        {
            files.ForEach(
                    file => {
                        var image = _imageRepository.CreateNewImage(file.FileName, Convert.FromBase64String(file.Data));
                        file.Tags.ForEach(tag => _tagRepository.AddTagToImage(image, tag));
                    }
                );
        }
    }
}
