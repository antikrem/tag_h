using EphemeralEx.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tag_h.Core.Model;
using tag_h.Core.Repositories;
using tag_h.Core.TagRetriever;
using tag_h.Persistence;

namespace tag_h.Core.Tasks
{
    public record SubmittedFile(string Data, string FileName, List<Tag> Tags);

    public record AddNewImagesConfiguration(List<SubmittedFile> Files);

    class AddNewImages
        : ITask<AddNewImagesConfiguration>
    {
        private readonly IHFileRepository _fileRepository;
        private readonly IDataHasher _datahasher;


        public AddNewImages(IHFileRepository fileRepository, IDataHasher dataHasher)
        {
            _fileRepository = fileRepository;
            _datahasher = dataHasher;
        }

        public string Name => "Add New Images";

        public Task Run(AddNewImagesConfiguration configuration)
        {
            configuration
                .Files
                .Where(HashIsNew)
                .ForEach(AddImage);

            return Task.CompletedTask; //TODO: propogate task down to db
        }

        private void AddImage(SubmittedFile file)
        {
            var image = _fileRepository.CreateNewImage(file.FileName, Convert.FromBase64String(file.Data));
            file.Tags.ForEach(image.AddTag);
            // autoTagger.TagImage(image).Wait();
        }

        private bool HashIsNew(SubmittedFile file)
            => _fileRepository
                .FetchFiles(new FileQuery { ImageHash = new FileHash(_datahasher.Hash(file.Data), null) })
                .Count() ==  0;
    }
}
