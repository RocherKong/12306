using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paway.Ticket.Package.Data
{
    [Serializable]
    public class CheckLoginData
    {
        private bool _flag;
        public bool flag
        {
            get { return this._flag; }
            set { this._flag = value; }
        }
    }
}
