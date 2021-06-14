using tagh.Client.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;
using tagh.Core.Model;
using tagh.Core.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace tagh.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHImageRepository _imageRepository;
        private readonly IHImageClientDataBuilder _imageClientDataBuilder;

        public IndexModel(ILogger<IndexModel> logger, IHImageRepository imageRepository, IHImageClientDataBuilder imageClientDataBuilder)
        {
            _logger = logger;
            _imageRepository = imageRepository;
            _imageClientDataBuilder = imageClientDataBuilder;
        }

        public void OnGet()
        {

        }
    }
}
