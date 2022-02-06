using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using EphemeralEx.Extensions;
using EphemeralEx.Injection;


namespace tag_h.Persistence
{
    [Injectable]
    public interface IDataHasher
    {
        string Hash(string data);

        string Hash(FileStream stream);
    }

    public class DataHasher : IDataHasher, IDisposable
    {
        private readonly MD5 _md5Hasher = MD5.Create();

        public void Dispose()
        {
            _md5Hasher.Dispose();
        }

        public string Hash(string data)
            => _md5Hasher
                .ComputeHash(Encoding.UTF8.GetBytes(data))
                .ToHexString();

        public string Hash(FileStream stream)
            => _md5Hasher
                .ComputeHash(stream)
                .ToHexString();
    }

}