using System;
using System.Collections.Generic;

using Workshell.FileFormats.Formats.Images;

namespace tagh.Core.Model
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
