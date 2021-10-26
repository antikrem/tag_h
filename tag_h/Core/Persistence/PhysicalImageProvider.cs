using System.IO;

using EphemeralEx.Injection;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence
{
    [Injectable]
    public interface IPhysicalImageProvider
    {
        void CreatePhysicalImage(string location, string filename, byte[] data);
        byte[] LoadImage(HImage image);
        FileStream LoadImageStream(HImage image);
    }

    public class PhysicalImageProvider : IPhysicalImageProvider
    {
        public void CreatePhysicalImage(string location, string filename, byte[] data)
        {
            using var stream = File.Create(Path.Join(location, filename));
            stream.Write(data);
        }

        public byte[] LoadImage(HImage image)
        {
            return File.ReadAllBytes(image.Location);
        }

        public FileStream LoadImageStream(HImage image)
        {
            return File.OpenRead(image.Location);
        }
    }
}
