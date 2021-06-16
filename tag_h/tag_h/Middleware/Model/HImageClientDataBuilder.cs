using tag_h.Injection;
using tag_h.Core.Model;
using tag_h.Core.Persistence;


namespace tag_h.Middleware.Model
{
    [Injectable]
    public interface IHImageClientDataBuilder
    {
        HImageClientData LoadImage(HImage image);
    }

    public class HImageClientDataBuilder : IHImageClientDataBuilder
    {
        private readonly IPhysicalImageProvider _physicalImageProvider;

        public HImageClientDataBuilder(IPhysicalImageProvider physicalImageProvider)
        {
            _physicalImageProvider = physicalImageProvider;
        }

        public HImageClientData LoadImage(HImage image)
        {
            return new HImageClientData(_physicalImageProvider.LoadImage(image), "jpg");
        }
    }

}