using System.Linq;
using UrlShortener.DAL.Models;

namespace UrlShortener.DAL.Repositories
{
    public class UrlRepository: IUrlRepository
    {
        private readonly UrlContext _context;

        public UrlRepository(UrlContext context)
        {
            _context = context;
        }
        
        public Url GetUrlByShortLink(string shortLink)
        {
            return _context.Urls.FirstOrDefault(u => u.ShortLink == shortLink);
        }

        public Url GetUrlByOriginalLink(string originalLink)
        {
            return _context.Urls.FirstOrDefault(u => u.OriginalLink == originalLink);
        }

        public void Save(Url url)
        {
            _context.Urls.Add(url);
            _context.SaveChanges();
        }
    }
}