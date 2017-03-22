using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Anw.HttpUtils.CsharpHttpHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Anw
{
    public static class AccessToken
    {
        private static string _accessToken;
        internal static Task Task;

        public static string GetAccessToken()
        {
            if (!string.IsNullOrEmpty(_accessToken)) return _accessToken;
            _accessToken = Get();
            Task = new Task(RefreshAccessToken);
            Task.Start();
            return _accessToken;
        }

        private static void RefreshAccessToken()
        {
            while (true)
            {
                Thread.Sleep(10*60*60*1000);
                _accessToken = Get();
            }
        }

        private static string Get()
        {
            var parameters = new SortedList<string, string>
            {
                {"client_id", "3085626607686562-KuNzcTu4qehlDbNRD1rMn5OPXqOQ3KBE"},
                {"client_secret", "R9KSI6pfIB2Ck0HDg_LW-CMKSan4"},
                {"grant_type", "refresh_token"},
                {"refresh_token", "79cbcc9d-6457-4c0c-9709-de0f8c2bc479"}
            };
            var buffer = new StringBuilder();
            var i = 0;
            foreach (var key in parameters.Keys)
            {
                buffer.AppendFormat(i > 0 ? "&{0}={1}" : "{0}={1}", key,
                    HttpHelper.UrlEncode(parameters[key], Encoding.UTF8));
                i++;
            }
            var httpItem = new HttpItem
            {
                Url = "https://my.sapanywhere.cn:443/oauth2/token/",
                Host = "my.sapanywhere.cn:443",
                ContentType = "application/x-www-form-urlencoded",
                Method = "post",
                KeepAlive = true,
                Postdata = buffer.ToString()
            };
            var httpHelper = new HttpHelper();
            var httpResult = httpHelper.GetHtml(httpItem);
            var jObject = (JObject) JsonConvert.DeserializeObject(httpResult.Html);
            return jObject["access_token"].ToString();
        }
    }
}