using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tag_h.Core.Model;
using tag_h.Core.Persistence;
using tag_h.Core.TagRetriever;

namespace tag_h.Core.Tasks
{
    public record SubmittedFile(string Data, string FileName, List<Tag> Tags);

    public record AddNewImagesConfiguration(List<SubmittedFile> Files);

    class AddNewImages
        : ITask<AddNewImagesConfiguration>
    {
        private readonly IHImageRepository _imageRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IImageHasher _imageHasher;
        private readonly IAutoTagger _autoTagger;


        public AddNewImages(IHImageRepository imageRepository, ITagRepository tagRepository, IImageHasher imageHasher, IAutoTagger autoTagger)
        {
            _imageRepository = imageRepository;
            _tagRepository = tagRepository;
            _imageHasher = imageHasher;
            _autoTagger = autoTagger;
        }

        public string Name => "Add New Images";

        public Task Run(AddNewImagesConfiguration configuration)
        {
            configuration.Files.ForEach(
                    file =>
                    {
                        var image = _imageRepository.CreateNewImage(file.FileName, Convert.FromBase64String(file.Data));
                        var hash = _imageHasher.HashImage(image); // TODO: bad, make hasher take data
                        if (HashExists(hash))
                        {
                            _imageRepository.DeleteImage(image);
                        }
                        else
                        {
                            TagImage(file, image);
                        }
                    }
                );
            return Task.CompletedTask; //TODO: propogate task down to db
        }

        private void TagImage(SubmittedFile file, HImage image)
        {
            file.Tags.ForEach(tag => _tagRepository.AddTagToImage(image, tag));
            // autoTagger.TagImage(image).Wait();
        }

        private bool HashExists(ImageHash hash)
            => _imageRepository
                .FetchImages(new ImageQuery { ImageHash = hash })
                .Count() > 1;
    }
}
