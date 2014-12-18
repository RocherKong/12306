using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paway.Ticket.Package.Data
{
    /// <summary>
    /// 登录时的数据包
    /// </summary>
    [Serializable]
    public class LoginData
    {
        private string _loginCheck;
        public string loginCheck
        {
            get { return this._loginCheck; }
            set { this._loginCheck = value; }
        }
    }
}
