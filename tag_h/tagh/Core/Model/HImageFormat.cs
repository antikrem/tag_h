using System;
using System.Collections.Generic;
using System.Linq;

using Workshell.FileFormats;
using Workshell.FileFormats.Formats;
using Workshell.FileFormats.Formats.Images;

namespace tag_h.Model
{
    static class HImageFormat
    {

        public static IEnumerable<Type> HashableFormats()
        {
            yield return typeof(JPEGImageFormat);
            yield return typeof(BitmapImageFormat);
            yield return typeof(GIFImageFormat);
            yield return typeof(PNGImageFormat);
        }

        public static bool IsHashableFormat(this HImage image)
        {
            return image.Format is JPEGImageFormat || image.Format is BitmapImageFormat || image.Format is GIFImageFormat || image.Format is PNGImageFormat;
        }
    }
}
