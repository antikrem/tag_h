using tagh.Core.Injection;
using tagh.Core.Model;
using tagh.Core.Persistence;

namespace tagh.Client.Model
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