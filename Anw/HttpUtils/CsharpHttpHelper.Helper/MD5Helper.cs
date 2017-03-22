using System.Web.Configuration;
using System.Web.Security;

namespace Anw.HttpUtils.CsharpHttpHelper.Helper
{
    internal class Md5Helper
    {
        internal static string ToMD5_32(string str)
        {
            string passwordFormat = FormsAuthPasswordFormat.MD5.ToString();
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, passwordFormat);
        }

        internal static string ToSha1(string str)
        {
            string passwordFormat = FormsAuthPasswordFormat.SHA1.ToString();
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, passwordFormat);
        }
    }
}