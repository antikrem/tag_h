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
        private readonly ITagRepository _tagRepository;
        private readonly IFileHasher _imageHasher;
        private readonly IAutoTagger _autoTagger;


        public AddNewImages(IHFileRepository fileRepository, ITagRepository tagRepository, IFileHasher imageHasher, IAutoTagger autoTagger)
        {
            _fileRepository = fileRepository;
            _tagRepository = tagRepository;
            _imageHasher = imageHasher;
            _autoTagger = autoTagger;
        }

        public string Name => "Add New Images";

        public Task Run(AddNewImagesConfiguration configuration)
        {
            //TODO: Make this work, probably pull the hashing part out of FileHasher

            //configuration.Files.ForEach(
            //        file =>
            //        {
            //            var image = _fileRepository.CreateNewImage(file.FileName, Convert.FromBase64String(file.Data));
            //            var hash = _imageHasher.HashImage(image); // TODO: bad, make hasher take data
            //            if (HashExists(hash))
            //            {
            //                _fileRepository.DeleteImage(image);
            //            }
            //            else
            //            {
            //                TagImage(file, image);
            //            }
            //        }
            //    );
            return Task.CompletedTask; //TODO: propogate task down to db
        }

        //private void TagImage(SubmittedFile file, HImage image)
        //{
        //    file.Tags.ForEach(tag => _tagRepository.AddTagToImage(image, tag));
        //    // autoTagger.TagImage(image).Wait();
        //}

        //private bool HashExists(FileHash hash)
        //    => _fileRepository
        //        .FetchImages(new FileQuery { ImageHash = hash })
        //        .Count() > 1;
    }
}
