using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using tag_h.Core.Model;
using tag_h.Core.Persistence;
using tag_h.Middleware.Model;


namespace tag_h.Middleware
{
    public class ServerImageProvider
    {
        private readonly RequestDelegate _next;
        private readonly IHImageRepository _imageRepository;
        private readonly IHImageClientDataBuilder _imageClientDataBuilder;

        private static string SearchCondition => nameof(ServerImageProvider);

        public ServerImageProvider(RequestDelegate next, IHImageRepository imageRepository, IHImageClientDataBuilder imageClientDataBuilder)
        {
            _next = next;
            _imageRepository = imageRepository;
            _imageClientDataBuilder = imageClientDataBuilder;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (!context.Request.Path.Value.Contains(SearchCondition) || !context.Request.Query.ContainsKey("Get"))
                {
                    return;
                }

                var uuid = int.Parse(context.Request.Query["Get"]);
                var image = _imageClientDataBuilder.LoadImage(_imageRepository.FetchImages(TagQuery.All with { UUID = uuid }).First());

                context.Response.ContentLength = image.Data.Length;
                context.Response.ContentType = "image/" + image.Extension;
                await context.Response.Body.WriteAsync(image.Data, 0, image.Data.Length);
            }
            finally
            {
                if (!context.Response.HasStarted)
                    await _next(context);
            }
        }

        public static string GetImage(IUrlHelper helper, int UUID) => helper.PageLink() + SearchCondition + helper.Action("Get", null, new { UUID = UUID });
    }
}