using System;

namespace Anw.HttpUtils.CsharpHttpHelper.Helper
{
    internal class JsonHelper
    {
        internal static object JsonToObject<T>(string jsonstr)
        {
            object result;
            try
            {
                result = JsonToObject<T>(jsonstr);
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        internal static string ObjectToJson(object obj)
        {
            string result;
            try
            {
                result = ObjectToJson(obj);
            }
            catch (Exception)
            {
                result = string.Empty;
            }
            return result;
        }
    }
}