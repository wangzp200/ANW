using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using Anw.HttpUtils.CsharpHttpHelper.Enum;
using Anw.HttpUtils.CsharpHttpHelper.Static;

namespace Anw.HttpUtils.CsharpHttpHelper.Base
{
    internal  class HttphelperBase
    {
        private IPEndPoint _ipEndPoint;
        private Encoding _encoding = Encoding.Default;
        private Encoding _postencoding = Encoding.Default;
        private HttpWebRequest _request;
        private HttpWebResponse _response;

        internal HttpResult GetHtml(HttpItem item)
        {
            var httpResult = new HttpResult();
            HttpResult result;
            try
            {
                SetRequest(item);
            }
            catch (Exception ex)
            {
                result = new HttpResult
                {
                    Cookie = string.Empty,
                    Header = null,
                    Html = ex.Message,
                    StatusDescription = "配置参数时出错：" + ex.Message
                };
                return result;
            }
            try
            {
                using (_response = (HttpWebResponse) _request.GetResponse())
                {
                    GetData(item, httpResult);
                }
            }
            catch (WebException ex2)
            {
                if (ex2.Response != null)
                {
                    using (_response = (HttpWebResponse) ex2.Response)
                    {
                        GetData(item, httpResult);
                    }
                }
                else
                {
                    httpResult.Html = ex2.Message;
                }
            }
            catch (Exception ex)
            {
                httpResult.Html = ex.Message;
            }
            if (item.IsToLower)
            {
                httpResult.Html = httpResult.Html.ToLower();
            }
            result = httpResult;
            return result;
        }

        internal HttpResult FastRequest(HttpItem item)
        {
            var httpResult = new HttpResult();
            HttpResult result;
            try
            {
                SetRequest(item);
            }
            catch (Exception ex)
            {
                result = new HttpResult
                {
                    Cookie = (_response.Headers["set-cookie"] != null) ? _response.Headers["set-cookie"] : string.Empty,
                    Header = null,
                    Html = ex.Message,
                    StatusDescription = "配置参数时出错：" + ex.Message
                };
                return result;
            }
            try
            {
                using (_response = (HttpWebResponse) _request.GetResponse())
                {
                    result = new HttpResult
                    {
                        Cookie =
                            (_response.Headers["set-cookie"] != null) ? _response.Headers["set-cookie"] : string.Empty,
                        Header = _response.Headers,
                        StatusCode = _response.StatusCode,
                        StatusDescription = _response.StatusDescription
                    };
                    return result;
                }
            }
            catch (WebException ex2)
            {
                using (_response = (HttpWebResponse) ex2.Response)
                {
                    result = new HttpResult
                    {
                        Cookie =
                            (_response.Headers["set-cookie"] != null) ? _response.Headers["set-cookie"] : string.Empty,
                        Header = _response.Headers,
                        StatusCode = _response.StatusCode,
                        StatusDescription = _response.StatusDescription
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                httpResult.Html = ex.Message;
            }
            if (item.IsToLower)
            {
                httpResult.Html = httpResult.Html.ToLower();
            }
            result = httpResult;
            return result;
        }

        private void GetData(HttpItem item, HttpResult result)
        {
            if (_response != null)
            {
                result.StatusCode = _response.StatusCode;
                result.ResponseUri = _response.ResponseUri.ToString();
                result.StatusDescription = _response.StatusDescription;
                result.Header = _response.Headers;
                if (_response.Cookies != null)
                {
                    result.CookieCollection = _response.Cookies;
                }
                if (_response.Headers["set-cookie"] != null)
                {
                    result.Cookie = _response.Headers["set-cookie"];
                }
                var @byte = GetByte();
                if (@byte != null && @byte.Length > 0)
                {
                    SetEncoding(item, result, @byte);
                    result.Html = _encoding.GetString(@byte);
                }
                else
                {
                    result.Html = string.Empty;
                }
            }
        }

        private void SetEncoding(HttpItem item, HttpResult result, byte[] responseByte)
        {
            if (item.ResultType == ResultType.Byte)
            {
                result.ResultByte = responseByte;
            }
            if (_encoding == null)
            {
                var match = Regex.Match(Encoding.Default.GetString(responseByte), RegexString.Enconding,
                    RegexOptions.IgnoreCase);
                var text = string.Empty;
                if (match != null && match.Groups.Count > 0)
                {
                    text = match.Groups[1].Value.ToLower().Trim();
                }
                var text2 = string.Empty;
                if (!string.IsNullOrWhiteSpace(_response.CharacterSet))
                {
                    text2 = _response.CharacterSet.Trim().Replace("\"", "").Replace("'", "");
                }
                if (text.Length > 2)
                {
                    try
                    {
                        _encoding =
                            Encoding.GetEncoding(
                                text.Replace("\"", string.Empty)
                                    .Replace("'", "")
                                    .Replace(";", "")
                                    .Replace("iso-8859-1", "gbk")
                                    .Trim());
                    }
                    catch
                    {
                        if (string.IsNullOrEmpty(text2))
                        {
                            _encoding = Encoding.UTF8;
                        }
                        else
                        {
                            _encoding = Encoding.GetEncoding(text2);
                        }
                    }
                }
                else if (string.IsNullOrEmpty(text2))
                {
                    _encoding = Encoding.UTF8;
                }
                else
                {
                    _encoding = Encoding.GetEncoding(text2);
                }
            }
        }

        private byte[] GetByte()
        {
            byte[] result = null;
            using (var memoryStream = new MemoryStream())
            {
                if (_response.ContentEncoding != null &&
                    _response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                {
                    new GZipStream(_response.GetResponseStream(), CompressionMode.Decompress).CopyTo(memoryStream, 10240);
                }
                else
                {
                    _response.GetResponseStream().CopyTo(memoryStream, 10240);
                }
                result = memoryStream.ToArray();
            }
            return result;
        }

        private void SetRequest(HttpItem item)
        {
            ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
            _request = (HttpWebRequest) WebRequest.Create(item.Url);
            if (item.IpEndPoint != null)
            {
                _ipEndPoint = item.IpEndPoint;
                _request.ServicePoint.BindIPEndPointDelegate = BindIpEndPointCallback;
            }
            _request.AutomaticDecompression = item.AutomaticDecompression;
            SetCer(item);
            SetCerList(item);
            if (item.Header != null && item.Header.Count > 0)
            {
                var allKeys = item.Header.AllKeys;
                for (var i = 0; i < allKeys.Length; i++)
                {
                    var name = allKeys[i];
                    _request.Headers.Add(name, item.Header[name]);
                }
            }
            SetProxy(item);
            if (item.ProtocolVersion != null)
            {
                _request.ProtocolVersion = item.ProtocolVersion;
            }
            _request.ServicePoint.Expect100Continue = item.Expect100Continue;
            _request.Method = item.Method;
            _request.Timeout = item.Timeout;
            _request.KeepAlive = item.KeepAlive;
            _request.ReadWriteTimeout = item.ReadWriteTimeout;
            if (!string.IsNullOrWhiteSpace(item.Host))
            {
                _request.Host = item.Host;
            }
            if (item.IfModifiedSince.HasValue)
            {
                _request.IfModifiedSince = Convert.ToDateTime(item.IfModifiedSince);
            }
            _request.Accept = item.Accept;
            _request.ContentType = item.ContentType;
            _request.UserAgent = item.UserAgent;
            _encoding = item.Encoding;
            _request.Credentials = item.Credentials;
            SetCookie(item);
            _request.Referer = item.Referer;
            _request.AllowAutoRedirect = item.Allowautoredirect;
            if (item.MaximumAutomaticRedirections > 0)
            {
                _request.MaximumAutomaticRedirections = item.MaximumAutomaticRedirections;
            }
            SetPostData(item);
            if (item.Connectionlimit > 0)
            {
                _request.ServicePoint.ConnectionLimit = item.Connectionlimit;
            }
        }

        private void SetCer(HttpItem item)
        {
            if (!string.IsNullOrWhiteSpace(item.CerPath))
            {
                if (!string.IsNullOrWhiteSpace(item.CerPwd))
                {
                    _request.ClientCertificates.Add(new X509Certificate(item.CerPath, item.CerPwd));
                }
                else
                {
                    _request.ClientCertificates.Add(new X509Certificate(item.CerPath));
                }
            }
        }

        private void SetCerList(HttpItem item)
        {
            if (item.ClentCertificates != null && item.ClentCertificates.Count > 0)
            {
                foreach (var current in item.ClentCertificates)
                {
                    _request.ClientCertificates.Add(current);
                }
            }
        }

        private void SetCookie(HttpItem item)
        {
            if (!string.IsNullOrEmpty(item.Cookie))
            {
                _request.Headers[HttpRequestHeader.Cookie] = item.Cookie;
            }
            if (item.ResultCookieType == ResultCookieType.CookieCollection)
            {
                _request.CookieContainer = new CookieContainer();
                if (item.CookieCollection != null && item.CookieCollection.Count > 0)
                {
                    _request.CookieContainer.Add(item.CookieCollection);
                }
            }
        }

        private void SetPostData(HttpItem item)
        {
            if (!_request.Method.Trim().ToLower().Contains("get"))
            {
                if (item.PostEncoding != null)
                {
                    _postencoding = item.PostEncoding;
                }
                byte[] array = null;
                if (item.PostDataType == PostDataType.Byte && item.PostdataByte != null && item.PostdataByte.Length > 0)
                {
                    array = item.PostdataByte;
                }
                else if (item.PostDataType == PostDataType.FilePath && !string.IsNullOrWhiteSpace(item.Postdata))
                {
                    var streamReader = new StreamReader(item.Postdata, _postencoding);
                    array = _postencoding.GetBytes(streamReader.ReadToEnd());
                    streamReader.Close();
                }
                else if (!string.IsNullOrWhiteSpace(item.Postdata))
                {
                    array = _postencoding.GetBytes(item.Postdata);
                }
                if (array != null)
                {
                    _request.ContentLength = array.Length;
                    _request.GetRequestStream().Write(array, 0, array.Length);
                }
            }
        }

        private void SetProxy(HttpItem item)
        {
            var flag = false;
            if (!string.IsNullOrWhiteSpace(item.ProxyIp))
            {
                flag = item.ProxyIp.ToLower().Contains("ieproxy");
            }
            if (!string.IsNullOrWhiteSpace(item.ProxyIp) && !flag)
            {
                if (item.ProxyIp.Contains(":"))
                {
                    var array = item.ProxyIp.Split(':');
                    var webProxy = new WebProxy(array[0].Trim(), Convert.ToInt32(array[1].Trim()));
                    webProxy.Credentials = new NetworkCredential(item.ProxyUserName, item.ProxyPwd);
                    _request.Proxy = webProxy;
                }
                else
                {
                    var webProxy = new WebProxy(item.ProxyIp, false);
                    webProxy.Credentials = new NetworkCredential(item.ProxyUserName, item.ProxyPwd);
                    _request.Proxy = webProxy;
                }
            }
            else if (!flag)
            {
                _request.Proxy = item.WebProxy;
            }
        }

        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors errors)
        {
            return true;
        }

        public IPEndPoint BindIpEndPointCallback(ServicePoint servicePoint, IPEndPoint remoteEndPoint, int retryCount)
        {
            return _ipEndPoint;
        }
    }
}