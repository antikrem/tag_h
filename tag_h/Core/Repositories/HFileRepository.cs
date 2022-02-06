using System.Collections.Generic;
using System.IO;
using System.Linq;

using EphemeralEx.Extensions;
using EphemeralEx.Injection;

using tag_h.Core.Model;
using tag_h.Persistence;
using tag_h.Persistence.Model;

namespace tag_h.Core.Repositories
{
    [Injectable]
    public interface IHFileRepository
    {
        //TODO: Remove into seperate repository
        DirectoryInfo ImageFolder { get; }

        void ApplyDeletions();

        void AddNewFile(string fileName);

        HFile CreateNewImage(string fileName, byte[] data);

        IEnumerable<HFile> FetchFiles(FileQuery query);
    }

    public class HImageRepository : IHFileRepository
    {
        private IDatabase _database;
        private readonly IPhysicalImageProvider _physicalImageProvider;
        private readonly IHFileFactory _hFileFactory;

        public DirectoryInfo ImageFolder => _database.ImageFolder;

        public HImageRepository(IDatabase database, IPhysicalImageProvider physicalImageProvider, IHFileFactory hFileFactory)
        {
            _database = database;
            _physicalImageProvider = physicalImageProvider;
            _hFileFactory = hFileFactory;
        }

        public void ApplyDeletions()
        {
            var deletedImages = _database.GetDeletedFiles();
            _database.RemoveDeletedFiles();
            deletedImages
                .Select(x => x.Location)
                .Where(File.Exists)
                .ForEach(File.Delete);
        }

        public void AddNewFile(string fileName)
        {
            _database.AddNewFile(fileName);
        }

        public HFile CreateNewImage(string fileName, byte[] data)
        {
            _physicalImageProvider.CreatePhysicalImage(_database.ImageFolder.FullName, fileName, data);
            fileName = Path.Join(_database.ImageFolder.FullName, fileName);
            return _hFileFactory.Create(
                _database.AddNewFile(fileName)
            );
        }

        public IEnumerable<HFile> FetchFiles(FileQuery query)
        {
            return _database.FetchAllFiles(query).Select(_hFileFactory.Create);
        }
    }
}
