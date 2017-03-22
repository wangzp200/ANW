using System;
using AnwConnector.Util;
using SAPbobsCOM;

namespace AnwConnector.Common
{
    public static class Config
    {
        public static string Server;
        public static string CompanyDb;
        public static BoSuppLangs Language;
        public static bool UseTrusted;
        public static string UserName;
        public static string Password;
        public static string DbUserName;
        public static string DbPassword;
        public static BoDataServerTypes DbServerType;
        public static string LicenseServer;
        public static string AnwUserName;
        public static string AnwUserPassword;
        public static int SleepTime;

        static Config()
        {
            Server = ServiceTools.GetAppSetting("Server");
            CompanyDb = ServiceTools.GetAppSetting("CompanyDB");
            Language = (BoSuppLangs)Enum.Parse(typeof(BoSuppLangs), ServiceTools.GetAppSetting("language"));
            UseTrusted = bool.Parse(ServiceTools.GetAppSetting("UseTrusted"));
            UserName = ServiceTools.GetAppSetting("UserName");
            Password = ServiceTools.GetAppSetting("Password");
            DbUserName = ServiceTools.GetAppSetting("DbUserName");
            DbPassword = ServiceTools.GetAppSetting("DbPassword");
            DbServerType =
                (BoDataServerTypes)Enum.Parse(typeof(BoDataServerTypes), ServiceTools.GetAppSetting("DbServerType"));
            LicenseServer = ServiceTools.GetAppSetting("LicenseServer");
            AnwUserName = ServiceTools.GetAppSetting("AnwUserName");
            AnwUserPassword = ServiceTools.GetAppSetting("AnwUserPassword");
            SleepTime = int.Parse(ServiceTools.GetAppSetting("SleepTime"));

        }
    }
}