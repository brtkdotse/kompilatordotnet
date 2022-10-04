using Kompilator.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kompilator.Pages
{
    public class GenericPage : PageModel
    {
        private readonly ILogger<GenericPage> _logger;
       
        private readonly ContentService _contentService;

        [BindProperty(SupportsGet = true)]
        public string PageName { get; set; }

        public string Content { get; set; }

        public GenericPage(ILogger<GenericPage> logger, ContentService contentService)
        {
            _logger = logger;
            _contentService = contentService;
        }

        public async Task OnGet()
        {
            Content = await _contentService.GetContentAsync(PageName);
        }
    }
}