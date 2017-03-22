using System.Drawing;
using Anw.HttpUtils.CsharpHttpHelper.Base;
using Anw.HttpUtils.CsharpHttpHelper.Enum;
using Anw.HttpUtils.CsharpHttpHelper.Helper;

namespace Anw.HttpUtils.CsharpHttpHelper.BaseBll
{
    internal class HttpHelperBll
    {
        private readonly HttphelperBase _httpbase = new HttphelperBase();

        internal HttpResult GetHtml(HttpItem item)
        {
            HttpResult result;
            if (item.Allowautoredirect && item.AutoRedirectCookie)
            {
                HttpResult httpResult = null;
                for (var i = 0; i < 100; i++)
                {
                    item.Allowautoredirect = false;
                    httpResult = _httpbase.GetHtml(item);
                    if (string.IsNullOrWhiteSpace(httpResult.RedirectUrl))
                    {
                        break;
                    }
                    item.Url = httpResult.RedirectUrl;
                    item.Method = "GET";
                    if (item.ResultCookieType == ResultCookieType.String)
                    {
                        item.Cookie += httpResult.Cookie;
                    }
                    else
                    {
                        item.CookieCollection.Add(httpResult.CookieCollection);
                    }
                }
                result = httpResult;
            }
            else
            {
                result = _httpbase.GetHtml(item);
            }
            return result;
        }

        internal Image GetImage(HttpItem item)
        {
            item.ResultType = ResultType.Byte;
            return ImageHelper.ByteToImage(GetHtml(item).ResultByte);
        }

        internal HttpResult FastRequest(HttpItem item)
        {
            return _httpbase.FastRequest(item);
        }
    }
}