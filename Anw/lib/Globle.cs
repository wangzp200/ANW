using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnwConnector.Util;
using SAPbobsCOM;

namespace Anw
{
    class Globle
    {
        public static readonly Company DiCompany = SapDiHelper.GetCompany();
    }
}
