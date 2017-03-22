using System.Linq;
using System.Text;
using Anw.HttpUtils.CsharpHttpHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SAPbobsCOM;


namespace Anw
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var httpHelper = new HttpHelper();
            //var httpItem = new HttpItem
            //{
            //    Url =
            //        "https://api.sapanywhere.cn:443/v1/SalesOrders?access_token=" +
            //        AccessToken.GetAccessToken(),
            //    ContentType = "application/x-www-form-urlencoded",
            //    Method = "get",
            //    KeepAlive = true
            //};
            //var httpResult = httpHelper.GetHtml(httpItem);
            //var jObject = JsonConvert.DeserializeObject<JArray>(httpResult.Html);
            //var salesOrderInfos = jObject.Children().ToList();
            //foreach (var salesOrderInfo in salesOrderInfos)
            //{
            //    var invoceId = salesOrderInfo["id"].ToString();
            //    var customerName =salesOrderInfo ["customer"]["name"].ToString();
            //    var docNumber = salesOrderInfo["docNumber"].ToString();
            //    var salesOrderId = salesOrderInfo["docNumber"].ToString();
            //    var vInvoice = (Documents)Globle.DiCompany.GetBusinessObject(BoObjectTypes.oInvoices);
            //    if (vInvoice != null)
            //    {
            //    }
            //}
            //更新数据测试
            //jObject["customerRemark"] = "Jack测试";
            //var line = new JObject
            //{
            //    new JProperty("customerRemark", "Jack测试")
            //};
            //jObject = new JObject(line);
            //var item = new HttpItem
            //{
            //    Url = "https://api.sapanywhere.cn:443/v1/SalesOrders/1110?access_token=" +
            //          AccessToken.GetAccessToken(),
            //    Method = "PATCH",
            //    ContentType = "application/json",
            //    KeepAlive = true,
            //    Postdata = jObject.ToString(),
            //    Encoding = Encoding.UTF8,
            //    PostEncoding = Encoding.UTF8
            //};
            ////item.Header.Add("access_token", AccessToken.GetAccessToken());
            //httpResult = httpHelper.GetHtml(item);
            //var x = httpResult.Html;
            //添加数据测试
            
            //var item = new HttpItem
            //{
            //    Url = "https://api.sapanywhere.cn:443/v1/SalesOrders?access_token=" +
            //          AccessToken.GetAccessToken(),
            //    Method = "POST",
            //    ContentType = "application/json",
            //    KeepAlive = true,
            //    Postdata = jObject.ToString(),
            //    Encoding = Encoding.UTF8,
            //    PostEncoding = Encoding.UTF8
            //}; 
            ////item.Header.Add("access_token", AccessToken.GetAccessToken());
            // var  httpResult = httpHelper.GetHtml(item);
            // var jobject = (JObject)JsonConvert.DeserializeObject(httpResult.Html);
            // var x = httpResult.Html;
            
        }
    }
}