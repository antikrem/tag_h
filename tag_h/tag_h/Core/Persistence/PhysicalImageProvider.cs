using System.IO;

using tagh.Core.Injection;
using tagh.Core.Model;

namespace tagh.Core.Persistence
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
