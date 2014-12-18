using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using Paway.Ticket.Entity;

namespace Paway.Ticket
{
    public class AppContext
    {
        public static string ValidateCode_Path = Application.StartupPath + "\\validateCode\\";

        public static LoginUser LoginUser = null;

        public static ToolTip Tip = new ToolTip();

        #region 方法
        
        #endregion
    }
}
