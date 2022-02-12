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
    public class FileViewModel
    {
        public int Id { get; }
        public string Location { get; }
        public IEnumerable<Tag> Tags { get; }

        public FileViewModel(HFile file, TagSet tags)
        {
            Id = file.Id;
            Location = file.Location;
            Tags = tags;
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IHFileRepository _fileRepository;
        private readonly ITaskRunner _taskRunner;

        public FilesController(
                ILogger logger,
                IHFileRepository fileRepository, 
                ITaskRunner taskRunner
            )
        {
            _logger = logger;
            _fileRepository = fileRepository;
            _taskRunner = taskRunner;
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<FileViewModel> GetAll()
        {
            var files = _fileRepository.FetchFiles(FileQuery.All);
            _logger.Information("Fetching images {list}", files);
            return files.Select(CreateViewModel);
        }

        [IgnoredByClient]
        [HttpGet]
        [Route("[action]")]
        public FileStreamResult GetFile(int imageId)
        {
            return File(
                _fileRepository
                    .FetchFiles(FileQuery.All with { Id = imageId })
                    .First()
                    .Stream, 
                "image/jpeg"
            );
        }

        [HttpPost]
        [Route("[action]")]
        public void AddFiles(List<SubmittedFile> files)
        {
            _taskRunner.Execute<AddNewImages, AddNewImagesConfiguration>(new(files));
        }

        private FileViewModel CreateViewModel(HFile file)
           => new(file, file.Tags);
    }
}
