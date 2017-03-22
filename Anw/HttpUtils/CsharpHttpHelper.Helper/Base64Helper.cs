using System;
using System.Text;

namespace Anw.HttpUtils.CsharpHttpHelper.Helper
{
    public class Base64Helper
    {
        public static string Base64ToString(string strbase, Encoding encoding)
        {
            var bytes = Convert.FromBase64String(strbase);
            return encoding.GetString(bytes);
        }

        public static string StringToBase64(byte[] bytebase)
        {
            return Convert.ToBase64String(bytebase);
        }

        public static string StringToBase64(string str, Encoding encoding)
        {
            var bytes = encoding.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }
    }
}