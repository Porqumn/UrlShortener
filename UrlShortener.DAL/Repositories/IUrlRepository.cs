using UrlShortener.DAL.Models;

namespace UrlShortener.DAL.Repositories
{
    public interface IUrlRepository
    {
        public Url GetUrlByShortLink(string shortLink);
        
        public Url GetUrlByOriginalLink(string originalLink);

        public void Save(Url url);
    }
}