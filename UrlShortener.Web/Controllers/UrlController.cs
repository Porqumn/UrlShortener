using Microsoft.AspNetCore.Mvc;
using UrlShortener.DAL.Models;
using UrlShortener.Services.Contracts;

namespace UrlShortener.Controllers
{
    [ApiController]
    public class UrlController : Controller
    {
        private IUrlService _service;
        private IUrlValidator _validator;
        
        public UrlController(IUrlService service, IUrlValidator validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpPost("api/url/get")]
        public IActionResult GetShortLink(UrlDto url)
        {

            if (!_validator.IsValid(url.OriginalLink))
            {
                ModelState.AddModelError("Url", "Incorrect url");
                return BadRequest(ModelState);
            }

            return Ok(_service.GetShortLink(url.OriginalLink));
        }

        [HttpGet("/{shortLink}")]
        public IActionResult RedirectToOriginalLink(string shortLink)
        {
            var urlToRedirect = _service.GetOriginalLink(shortLink);

            if (urlToRedirect == null)
            {
                return RedirectToPage("/Error/NotFound");
            }

            return RedirectPermanent(_service.AddToUrlHttpPrefix(urlToRedirect));
        }
    }
}