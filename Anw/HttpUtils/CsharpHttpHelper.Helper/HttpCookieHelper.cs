using System;
using System.Collections.Generic;
using System.Net;

namespace Anw.HttpUtils.CsharpHttpHelper.Helper
{
    internal static class HttpCookieHelper
    {
        internal static string GetSmallCookie(string strcookie)
        {
            string result;
            if (string.IsNullOrWhiteSpace(strcookie))
            {
                result = string.Empty;
            }
            else
            {
                var list = new List<string>();
                var array = strcookie.Split(new[]
                {
                    ",",
                    ";"
                }, StringSplitOptions.RemoveEmptyEntries);
                var array2 = array;
                for (var i = 0; i < array2.Length; i++)
                {
                    var text = array2[i];
                    var text2 = text.ToLower().Trim().Replace("\r\n", string.Empty).Replace("\n", string.Empty);
                    if (!string.IsNullOrWhiteSpace(text2))
                    {
                        if (text2.Contains("="))
                        {
                            if (!text2.Contains("path="))
                            {
                                if (!text2.Contains("expires="))
                                {
                                    if (!text2.Contains("domain="))
                                    {
                                        if (!list.Contains(text))
                                        {
                                            list.Add(string.Format("{0};", text));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                result = string.Join(";", list);
            }
            return result;
        }

        internal static CookieCollection StrCookieToCookieCollection(string strcookie)
        {
            CookieCollection result;
            if (string.IsNullOrWhiteSpace(strcookie))
            {
                result = null;
            }
            else
            {
                var cookieCollection = new CookieCollection();
                strcookie = GetSmallCookie(strcookie);
                var array = strcookie.Split(new[]
                {
                    ";"
                }, StringSplitOptions.RemoveEmptyEntries);
                var array2 = array;
                for (var i = 0; i < array2.Length; i++)
                {
                    var text = array2[i];
                    var array3 = text.Split(new[]
                    {
                        "="
                    }, StringSplitOptions.RemoveEmptyEntries);
                    if (array3.Length == 2)
                    {
                        cookieCollection.Add(new Cookie
                        {
                            Name = array3[0].Trim(),
                            Value = array3[1].Trim()
                        });
                    }
                }
                result = cookieCollection;
            }
            return result;
        }

        internal static string CookieCollectionToStrCookie(CookieCollection cookie)
        {
            string result;
            if (cookie == null)
            {
                result = string.Empty;
            }
            else
            {
                var text = string.Empty;
                foreach (Cookie cookie2 in cookie)
                {
                    text += string.Format("{0}={1};", cookie2.Name, cookie2.Value);
                }
                result = text;
            }
            return result;
        }
    }
}