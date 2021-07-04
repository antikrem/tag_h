using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using tag_h.Core.Helper.Extensions;
using tag_h.Core.Model;
using tag_h.Core.Persistence;


namespace tag_h.Middleware
{
    public class ImageProvider
    {
        private readonly RequestDelegate _next;
        private readonly IHImageRepository _imageRepository;
        private readonly IPhysicalImageProvider _physicalImageProvider;

        private static string SearchCondition => nameof(ImageProvider);

        public ImageProvider(
                RequestDelegate next,
                IHImageRepository imageRepository,
                IPhysicalImageProvider physicalImageProvider
            )
        {
            _next = next;
            _imageRepository = imageRepository;
            _physicalImageProvider = physicalImageProvider;
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
                var image =_imageRepository.FetchImages(TagQuery.All with { UUID = uuid }).First();

                var data = _physicalImageProvider.LoadImage(image);

                await context.Respond("image/" + image.Format.Extensions.First(), data);
            }
            finally
            {
                if (!context.Response.HasStarted)
                    await _next(context);
            }
        }

    }
}