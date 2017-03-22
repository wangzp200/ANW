using System;
using System.Linq;
using System.Net;

namespace Anw.HttpUtils.CsharpHttpHelper
{
    public class HttpResult
    {
        public string Cookie { get; set; }

        public CookieCollection CookieCollection { get; set; }

        public string Html { get; set; }

        public byte[] ResultByte { get; set; }

        public WebHeaderCollection Header { get; set; }

        public string StatusDescription { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string ResponseUri { get; set; }

        public string RedirectUrl
        {
            get
            {
                string result;
                try
                {
                    if (Header != null && Header.Count > 0)
                    {
                        if (Header.AllKeys.Any((string k) => k.ToLower().Contains("location")))
                        {
                            var text = Header["location"].Trim();
                            var text2 = text.ToLower();
                            if (!string.IsNullOrWhiteSpace(text2))
                            {
                                if (!text2.StartsWith("http://") && !text2.StartsWith("https://"))
                                {
                                    text = new Uri(new Uri(ResponseUri), text).AbsoluteUri;
                                }
                            }
                            result = text;
                            return result;
                        }
                    }
                }
                catch
                {
                }
                result = string.Empty;
                return result;
            }
        }
    }
}