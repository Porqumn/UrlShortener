using System;
using System.Net;
using UrlShortener.Services.Contracts;

namespace UrlShortener.Services.Implementations
{
    public class UrlValidator : IUrlValidator
    {
        private IUrlService _service;

        public UrlValidator(IUrlService service)
        {
            _service = service;
        }

        public bool IsValid(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }

            var urlWithHttpPrefix = _service.AddToUrlHttpPrefix(url);

            var request = (HttpWebRequest)WebRequest.Create(urlWithHttpPrefix);

            HttpWebResponse response = null;

            request.Headers.Add("Accept-Language", "en-GB");
            request.UserAgent = @"UserAgent";
            request.Accept = "*/*";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "HEAD";

            try
            {
                response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }
    }
}