using System;
using System.Collections.Generic;
using System.Linq;

using tag_h.Core.Model;
using tag_h.Core.Persistence;
using tag_h.Core.TagRetriever;

namespace tag_h.Core.Tasks
{
    public record SubmittedFile(string Data, string FileName, List<Tag> Tags);

    class AddNewImages : ITask
    {
        private readonly List<SubmittedFile> _files;

        public string TaskName => "Adding New Images";

        public AddNewImages(List<SubmittedFile> files)
        {
            _files = files;
        }

        public void Execute(IHImageRepository imageRepository, ITagRepository tagRepository, IImageHasher imageHasher, IAutoTagger autoTagger)
        {
            _files.ForEach(
                    file =>
                    {
                        var image = imageRepository.CreateNewImage(file.FileName, Convert.FromBase64String(file.Data));
                        var hash = imageHasher.HashImage(image);
                        if (HashExists(imageRepository, hash))
                        {
                            imageRepository.DeleteImage(image);
                        }
                        else
                        {
                            TagImage(tagRepository, autoTagger, file, image);
                        }
                    }
                );
        }

        private static void TagImage(ITagRepository tagRepository, IAutoTagger autoTagger, SubmittedFile file, HImage image)
        {
            file.Tags.ForEach(tag => tagRepository.AddTagToImage(image, tag));
            autoTagger.TagImage(image).Wait();
        }

        private static bool HashExists(IHImageRepository imageRepository, ImageHash hash)
        {
            return imageRepository
                .FetchImages(new ImageQuery { ImageHash = hash })
                .Count() > 1;
        }
    }
}
