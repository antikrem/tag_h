using System;
using System.Collections;
using System.Collections.Generic;

namespace tag_h.Model
{
    class HImageList : IEnumerable<HImage>
    {
        private List<HImage> _images;

        private int pointer = 0;

        public HImageList(List<HImage> images)
        {
            _images = images;
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
                if (!image.isImageLoaded())
                {
                    image.loadBitmap();
                }
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
    }
}
