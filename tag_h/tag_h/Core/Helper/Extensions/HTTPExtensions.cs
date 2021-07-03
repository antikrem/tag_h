using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace tag_h.Core.Helper.Extensions
{
    static class HTTPExtensions
    {
        public static async Task Respond(this HttpContext context, string type, byte[] data)
        {
            context.Response.ContentLength = data.Length;
            context.Response.ContentType = "text";
            await context.Response.Body.WriteAsync(data, 0, data.Length);
        }

        public static async Task RespondWithText(this HttpContext context, string text)
        {
            var data = Encoding.ASCII.GetBytes(text);
            await context.Respond("text", data);
        }

        public static async Task RespondWithObject<T>(this HttpContext context, IJsonifier jsonifier, T obj)
        {
            var json = jsonifier.Jsonify<T>(obj);
            var data = Encoding.ASCII.GetBytes(json);
            await context.Respond("object", data);
        }
    }
}
