using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

using tag_h.Core.Helper.Extensions;
using tag_h.Injection;

namespace tag_h.Core.TagRetriever
{
    [Injectable]
    public interface IFetchHandler
    {
        Task<T> FetchAsync<T>(string url);
    }

    public class FetchHandler : IFetchHandler
    {
        private readonly IJsonifier _jsonifier;

        public FetchHandler(IJsonifier jsonifier)
        {
            _jsonifier = jsonifier;
        }

        public async Task<string> GetAsync(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public async Task<T> FetchAsync<T>(string url)
        {
            var response = await GetAsync(url);
            return _jsonifier.ParseJson<T>(response);
        }
    }
}
