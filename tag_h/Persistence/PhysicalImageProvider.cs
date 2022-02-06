using System.IO;

using EphemeralEx.Injection;
using tag_h.Persistence.Model;

namespace tag_h.Persistence
{
    [Injectable]
    public interface IPhysicalImageProvider
    {
        void CreatePhysicalImage(string location, string filename, byte[] data);
        byte[] LoadFile(HFileState file);
        //TODO: why does this exist?
        FileStream LoadFileStream(HFileState file);
    }

    public class PhysicalImageProvider : IPhysicalImageProvider
    {
        public void CreatePhysicalImage(string location, string filename, byte[] data)
        {
            using var stream = File.Create(Path.Join(location, filename));
            stream.Write(data);
        }

        public byte[] LoadFile(HFileState file)
        {
            return File.ReadAllBytes(file.Location);
        }

        public FileStream LoadFileStream(HFileState file)
        {
            return File.OpenRead(file.Location);
        }
    }
}
