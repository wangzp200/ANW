using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anw.HttpUtils.CsharpHttpHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Anw.Model
{
    internal class Customer 
    {
        public  void TaskJob()
        {
            var httpHelper = new HttpHelper();
            var httpItem = new HttpItem
            {
                Url =
                    "https://api.sapanywhere.cn:443/v1/Customer?access_token=" +
                    AccessToken.GetAccessToken(),
                ContentType = "application/x-www-form-urlencoded",
                Method = "get",
                KeepAlive = true
            };
            var httpResult = httpHelper.GetHtml(httpItem);
            var jObject = (JObject) JsonConvert.DeserializeObject(httpResult.Html);

        }
    }
}
