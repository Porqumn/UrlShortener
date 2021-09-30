namespace UrlShortener.DAL.Models
{
    public class Url
    {
        public int Id { get; set; }

        public string ShortLink { get; set; }
        
        public string OriginalLink { get; set; }
    }
}