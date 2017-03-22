using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Xml;

namespace AnwConnector.Util
{
    /// <summary>
    ///     工具类
    /// </summary>
    public class ServiceTools : IConfigurationSectionHandler
    {
        /// <summary>
        ///     实现接口以读写app.config
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="configContext"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public object Create(object parent, object configContext, XmlNode section)
        {
            var handler = new NameValueSectionHandler();
            return handler.Create(parent, configContext, section);
        }

        /// <summary>
        ///     获取AppSettings节点值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        ///     获取configSections节点
        /// </summary>
        /// <returns></returns>
        public static XmlNode GetConfigSections()
        {
            var doc = new XmlDocument();
            doc.Load(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath);
            if (doc.DocumentElement != null) return doc.DocumentElement.FirstChild;
            return null;
        }

        /// <summary>
        ///     获取section节点
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static NameValueCollection GetSection(string nodeName)
        {
            return (NameValueCollection) ConfigurationManager.GetSection(nodeName);
        }

        /// <summary>
        ///     停止Windows服务
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        public static void WindowsServiceStop(string serviceName)
        {
            var control = new ServiceController(serviceName);
            control.Stop();
            control.Dispose();
        }

        /// <summary>
        ///     写日志
        /// </summary>
        /// <param name="path">日志文件</param>
        /// <param name="cont">日志内容</param>
        /// <param name="isAppend">是否追加方式</param>
        public static void WriteLog(string path, string cont, bool isAppend)
        {
            using (var sw = new StreamWriter(path, isAppend, Encoding.UTF8))
            {
                sw.WriteLine(DateTime.Now);
                sw.WriteLine(cont);
                sw.WriteLine("");
                sw.Close();
            }
        }
    } //end class
}