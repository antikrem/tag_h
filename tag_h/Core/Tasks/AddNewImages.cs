using System;
using System.Collections.Generic;

using tag_h.Core.Model;
using tag_h.Core.Persistence;


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

        public void Execute(IHImageRepository imageRepository, ITagRepository tagRepository, IImageHasher imageHasher)
        {
            _files.ForEach(
                    file => {
                        var image = imageRepository.CreateNewImage(file.FileName, Convert.FromBase64String(file.Data));
                        file.Tags.ForEach(tag => tagRepository.AddTagToImage(image, tag));
                        imageHasher.HashImage(image);
                    }
                );
        }
    }
}
