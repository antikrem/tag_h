using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using tag_h.Core.Helper.Extensions;
using tag_h.Core.Model;
using tag_h.Core.Persistence;
using tag_h.Middleware.Model;


namespace tag_h.Middleware
{
    public class ImageProvider
    {
        private readonly RequestDelegate _next;
        private readonly IHImageRepository _imageRepository;
        private readonly IHImageClientDataBuilder _imageClientDataBuilder;

        private static string SearchCondition => nameof(ImageProvider);

        public ImageProvider(
                RequestDelegate next,
                IHImageRepository imageRepository, 
                IHImageClientDataBuilder imageClientDataBuilder
            )
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

                await context.Respond("image/" + image.Extension, image.Data);
            }
            finally
            {
                if (!context.Response.HasStarted)
                    await _next(context);
            }
        }

    }
}