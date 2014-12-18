using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paway.Ticket.UI
{
    public class TabPageClosingEventArgs : EventArgs
    {
        #region 变量
        
        private bool _cancel = false;
        private vTabPage _page;

        #endregion

        #region 构造函数

        public TabPageClosingEventArgs(vTabPage page)
        {
            this._page = page;
        }

        #endregion

        #region 属性
        
        public bool Cancel
        {
            get { return this._cancel; }
            set { this._cancel = value; }
        }

        public vTabPage Page
        {
            get { return this._page; }
            set { this._page = value; }
        }

        #endregion
    }

    public delegate void TabPageClosingHandler(TabPageClosingEventArgs e);
}
