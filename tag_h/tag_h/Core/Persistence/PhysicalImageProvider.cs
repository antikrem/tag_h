using System.IO;

using tag_h.Injection;
using tag_h.Core.Model;


namespace tag_h.Core.Persistence
{
    [Injectable]
    public interface IPhysicalImageProvider
    {
        byte[] LoadImage(HImage image);
    }

    public class PhysicalImageProvider : IPhysicalImageProvider
    {

        public byte[] LoadImage(HImage image)
        {
            return File.ReadAllBytes(image.Location);
        }
    }
}
