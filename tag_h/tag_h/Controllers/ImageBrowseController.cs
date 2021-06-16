using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using tag_h.Core.Model;
using tag_h.Core.Persistence;

namespace tag_h.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ImageBrowseController : ControllerBase
    {

        private readonly ILogger<ImageBrowseController> _logger;
        private readonly IHImageRepository _imageRepository;

        public ImageBrowseController(ILogger<ImageBrowseController> logger, IHImageRepository imageRepository)
        {
            _logger = logger;
            _imageRepository = imageRepository;
        }

        [HttpGet]
        public HImageList Get()
        {
            return _imageRepository.FetchImages(TagQuery.All);

        }
    }
}
