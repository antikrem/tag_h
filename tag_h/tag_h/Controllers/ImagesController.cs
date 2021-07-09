using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Serilog;

using tag_h.Core.Model;
using tag_h.Core.Persistence;
using tag_h.Core.Helper.Extensions;

namespace tag_h.Controllers
{
    public record SubmittedFile(string Data, string FileName);

    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IHImageRepository _imageRepository;

        public ImagesController(ILogger logger, IHImageRepository imageRepository)
        {
            _logger = logger;
            _imageRepository = imageRepository;
        }

        [HttpGet]
        [Route("[action]")]
        public HImageList Get()
        {
            var images = _imageRepository.FetchImages(TagQuery.All);
            _logger.Information("Fetching images {list}", images);
            return images;
        }

        [HttpPost]
        [Route("[action]")]
        public void AddImages(List<SubmittedFile> files)
        {
            files.ForEach(
                    file => _imageRepository.AddNewImage(file.FileName, Convert.FromBase64String(file.Data))
                );
        }
    }
}
