using System;
using System.Collections;
using System.Collections.Generic;
using tagh.Core.Model;
using tagh.Core.Persistence;

namespace tagh.Core.Model
{
    public class HImageList : IEnumerable<HImage>, IDisposable
    {
        private List<HImage> _images;

        private int pointer = 0;

        private IHImageRepository _imageRepository;

        public HImageList(IHImageRepository imageRepository, List<HImage> images)
        {
            _images = images;
            _imageRepository = imageRepository;
        }

        public HImageList()
        {
            _images = new List<HImage>();
        }

        public HImage Get()
        {
            if  (_images.Count > 0)
            {
                var image = _images[pointer];
                //if (!image.isImageLoaded())
                //{
                //    image.loadBitmap();
                //}
                return image;
            }
            else
            {
                return null;
            }
            
        }

        public void MoveBack()
        {
            pointer = Math.Max(0, pointer - 1);
        }

        public void MoveForward()
        {
            pointer = Math.Min(_images.Count - 1, pointer + 1);
            pointer = Math.Max(0, pointer);
        }

        public bool AtStart()
        {
            return pointer == 0;
        }

        public bool AtEnd()
        {
            return pointer == _images.Count - 1;
        }

        public IEnumerator<HImage> GetEnumerator()
        {
            return _images.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _images.GetEnumerator();
        }

        public void Dispose()
        {
            _images.ForEach(_imageRepository.SaveImage);
        }
    }
}
