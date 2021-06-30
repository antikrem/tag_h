using Microsoft.AspNetCore.Mvc;
using Serilog;

using tag_h.Core.Model;
using tag_h.Core.Persistence;

namespace tag_h.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ImageBrowseController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IHImageRepository _imageRepository;

        public ImageBrowseController(ILogger logger, IHImageRepository imageRepository)
        {
            _logger = logger;
            _imageRepository = imageRepository;
        }

        [HttpGet]
        public HImageList Get()
        {
            var images = _imageRepository.FetchImages(TagQuery.All);
            _logger.Information("Fetching images {list}", images);
            return images;

        }
    }
}
