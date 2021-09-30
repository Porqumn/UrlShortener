using UrlShortener.DAL.Models;

namespace UrlShortener.Services.Contracts
{
    public interface IUrlService
    {
        public UrlDto GetShortLink(string originalLink);

        public string GetOriginalLink(string shortLink);

        public string AddToUrlHttpPrefix(string url);
    }
}