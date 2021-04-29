using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tag_h.Model
{
    class HImageList
    {
        private List<HImage> _images;

        private int pointer = 0;

        public HImageList(List<HImage> images)
        {
            _images = images;
        }

        public HImage Get()
        {
            return _images[pointer];
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
    }
}
