using System;
using AnwConnector.Common;
using SAPbobsCOM;

namespace AnwConnector.Util
{
    public class SapDiHelper
    {
        private static Company _company;

        public static Company GetCompany()
        {
            if (_company == null)
            {
                _company = new Company
                {
                    Server = Config.Server,
                    CompanyDB = Config.CompanyDb,
                    language = Config.Language,
                    UseTrusted = Config.UseTrusted,
                    UserName = Config.UserName,
                    Password = Config.Password,
                    DbUserName = Config.DbUserName,
                    DbPassword = Config.DbPassword,
                    DbServerType = Config.DbServerType,
                    LicenseServer = Config.LicenseServer
                };
                LogHelper.WriteLog("正在连接：" + Config.CompanyDb);
                var connect = _company.Connect();
                if (connect != 0)
                {
                    var errCode = 0;
                    var errMsg = "";
                    _company.GetLastError(out errCode, out errMsg);
                    LogHelper.WriteLog("连接：" + Config.CompanyDb+"失败.",new Exception("errCode:"+errCode+",errMsg:"+errMsg));
                }
                else
                {
                    LogHelper.WriteLog("成功连接DI!");
                }
            }
            if (!_company.Connected)
            {
                _company.Connect();
            }
            return _company;
        }

        public static void Discount()
        {
            if (_company != null)
            {
                _company.Disconnect();
            }
        }
    }
}