using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;

namespace Anw.HttpUtils.CsharpHttpHelper.Helper
{
    internal class HttpUrlHelper
    {
        internal static string UrlDecode(string text, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }
            return HttpUtility.UrlDecode(text, encoding);
        }

        internal static string UrlEncode(string text, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }
            return HttpUtility.UrlEncode(text, encoding);
        }

        internal static NameValueCollection GetNameValueCollection(string str)
        {
            NameValueCollection result = null;
            try
            {
                result = HttpUtility.ParseQueryString(str);
            }
            catch
            {
            }
            return result;
        }

        internal static string GetUrlHost(string url)
        {
            string result;
            try
            {
                result = new Uri(url).Host;
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }

        internal static string GetUrlIp(string url)
        {
            string result;
            try
            {
                var hostByName = Dns.GetHostByName(GetUrlHost(url));
                result = hostByName.AddressList[0].ToString();
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }

        public string GetNowUrl(int selectNo)
        {
            string result;
            switch (selectNo)
            {
                case 1:
                    result = HttpContext.Current.Request.Url.ToString();
                    break;
                case 2:
                    result = HttpContext.Current.Request.RawUrl;
                    break;
                case 3:
                    result = HttpContext.Current.Request.Url.AbsolutePath;
                    break;
                case 4:
                    result = HttpContext.Current.Request.Url.Host;
                    break;
                case 5:
                    result = HttpContext.Current.Request.Url.Query;
                    break;
                default:
                    result = HttpContext.Current.Request.Url.ToString();
                    break;
            }
            return result;
        }
    }
}