using Base62;
using System;
using UrlShortener.DAL.Models;
using UrlShortener.DAL.Repositories;
using UrlShortener.Services.Contracts;

namespace UrlShortener.Services.Implementations
{
    public class UrlService: IUrlService
    {
        private const string HttpPrefix = "http";

        private const int LengthShortLink = 8;
        
        private IUrlRepository _urlRepository;

        public UrlService(IUrlRepository repository)
        {
            _urlRepository = repository;
        }

        public UrlDto GetShortLink(string originalLink)
        {
            var existingUrl = _urlRepository.GetUrlByOriginalLink(originalLink);

            if (existingUrl != null)
            {
                return new UrlDto {OriginalLink = existingUrl.OriginalLink, ShortLink = existingUrl.ShortLink};
            }

            var url = new Url {OriginalLink = originalLink, ShortLink = GenerateShortLink(originalLink)};

            _urlRepository.Save(url);

            return new UrlDto {OriginalLink = url.OriginalLink, ShortLink = url.ShortLink};
        }

        public string GetOriginalLink(string shortLink)
        {
            return _urlRepository.GetUrlByShortLink(shortLink)?.OriginalLink;
        }

        public string AddToUrlHttpPrefix(string  url)
        {
            if (!url.StartsWith(HttpPrefix))
            {
                return $"{HttpPrefix}://{url}";
            }

            return url;
        }

        private string GenerateShortLink(string originalLink)
        {
            var base62Converter = new Base62Converter();

            var encoded = base62Converter.Encode(originalLink);

            var potentialShortLink = encoded[0..LengthShortLink];

            if (_urlRepository.GetUrlByShortLink(potentialShortLink) != null)
            {
                var random = new Random();
                GenerateShortLink($"{originalLink}{random.Next(1, 100)}");
            }

            return potentialShortLink;
        }


    }
}