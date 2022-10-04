using Kompilator.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kompilator.Pages
{
    public class GenericPage : PageModel
    {
        private readonly ILogger<GenericPage> _logger;
        private readonly SimplecastService _service;

        [BindProperty(SupportsGet = true)]
        public string PageName { get; set; }

        public GenericPage(ILogger<GenericPage> logger, SimplecastService service)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
          
        }
    }
}