
namespace UrlShortener.Services.Contracts
{
    public interface IUrlValidator
    {
        public bool IsValid(string url);
    }
}