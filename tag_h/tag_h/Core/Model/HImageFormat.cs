using System;
using System.Collections.Generic;

using Workshell.FileFormats.Formats.Images;


namespace tag_h.Core.Model
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
            return image.Format is JPEGImageFormat or BitmapImageFormat or GIFImageFormat or PNGImageFormat;
        }
    }
}
