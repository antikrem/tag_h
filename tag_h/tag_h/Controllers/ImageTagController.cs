using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Serilog;

using tag_h.Core.Model;
using tag_h.Core.Persistence;

namespace tag_h.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ImageTagProvider : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IHImageRepository _imageRepository;
        private readonly ITagRepository _tagRepository;

        public ImageTagProvider(ILogger logger, IHImageRepository imageRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            _imageRepository = imageRepository;
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public TagSet GetTags(int uuid)
        {
            var image = _imageRepository.FetchImages(new TagQuery { UUID = uuid }).First();
            var tags = _tagRepository.GetTagsForImage(image);
            _logger.Information("Fetching images {tags}", tags);
            return tags;
        }
    }
}
