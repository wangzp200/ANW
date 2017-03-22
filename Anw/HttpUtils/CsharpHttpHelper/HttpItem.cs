using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Anw.HttpUtils.CsharpHttpHelper.Enum;

namespace Anw.HttpUtils.CsharpHttpHelper
{
    public class HttpItem
    {
        private string _accept = "text/html, application/xhtml+xml, */*";
        private DecompressionMethods _automaticDecompression = DecompressionMethods.None;
        private string _contentType = "text/html";
        private bool _expect100Continue = true;
        private ICredentials _iCredentials = CredentialCache.DefaultCredentials;
        private bool _keepAlive = true;
        private string _method = "GET";
        private PostDataType _postDataType = PostDataType.String;
        private int _readWriteTimeout = 30000;
        private ResultCookieType _resultCookieType = ResultCookieType.String;
        private int _timeout = 100000;
        private string _userAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
        private int _connectionlimit = 1024;
        private WebHeaderCollection _header = new WebHeaderCollection();
        private ResultType _resulttype = ResultType.String;

        public HttpItem()
        {
            IpEndPoint = null;
            Allowautoredirect = false;
            IsToLower = false;
            AutoRedirectCookie = false;
            IfModifiedSince = null;
        }

        public string Url { get; set; }

        public string Method
        {
            get { return _method; }
            set { _method = value; }
        }

        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        public int ReadWriteTimeout
        {
            get { return _readWriteTimeout; }
            set { _readWriteTimeout = value; }
        }

        public string Host { get; set; }

        public bool KeepAlive
        {
            get { return _keepAlive; }
            set { _keepAlive = value; }
        }

        public string Accept
        {
            get { return _accept; }
            set { _accept = value; }
        }

        public string ContentType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }

        public string UserAgent
        {
            get { return _userAgent; }
            set { _userAgent = value; }
        }

        public string Referer { get; set; }

        public Version ProtocolVersion { get; set; }

        public bool Expect100Continue
        {
            get { return _expect100Continue; }
            set { _expect100Continue = value; }
        }

        public int MaximumAutomaticRedirections { get; set; }

        public DateTime? IfModifiedSince { get; set; }

        public Encoding Encoding { get; set; }

        public Encoding PostEncoding { get; set; }

        public DecompressionMethods AutomaticDecompression
        {
            get { return _automaticDecompression; }
            set { _automaticDecompression = value; }
        }

        public PostDataType PostDataType
        {
            get { return _postDataType; }
            set { _postDataType = value; }
        }

        public string Postdata { get; set; }

        public byte[] PostdataByte { get; set; }

        public CookieCollection CookieCollection { get; set; }

        public string Cookie { get; set; }

        public bool AutoRedirectCookie { get; set; }

        public ResultCookieType ResultCookieType
        {
            get { return _resultCookieType; }
            set { _resultCookieType = value; }
        }

        public string CerPath { get; set; }

        public string CerPwd { get; set; }

        public X509CertificateCollection ClentCertificates { get; set; }

        public ICredentials Credentials
        {
            get { return _iCredentials; }
            set { _iCredentials = value; }
        }

        public bool IsToLower { get; set; }
        public bool Allowautoredirect { get; set; }

        public int Connectionlimit
        {
            get { return _connectionlimit; }
            set { _connectionlimit = value; }
        }

        public WebProxy WebProxy { get; set; }

        public string ProxyUserName { get; set; }

        public string ProxyPwd { get; set; }

        public string ProxyIp { get; set; }

        public ResultType ResultType
        {
            get { return _resulttype; }
            set { _resulttype = value; }
        }

        public WebHeaderCollection Header
        {
            get { return _header; }
            set { _header = value; }
        }

        public IPEndPoint IpEndPoint { get; set; }
    }
}