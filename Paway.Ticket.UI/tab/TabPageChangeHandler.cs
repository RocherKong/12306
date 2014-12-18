using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paway.Ticket.UI
{
    public class TabPageChangeEventArgs : EventArgs
    {
        #region 变量
        
        private vTabPage _page;
        private ETabPageChangeType _type;
        #endregion

        #region 构造函数
        public TabPageChangeEventArgs(vTabPage page, ETabPageChangeType type)
        {
            this._type = type;
            this._page = page;
        }
        #endregion

        #region 属性
        
        public ETabPageChangeType TabPageChangeType
        {
            get { return this._type; }
        }

        public vTabPage TabPage
        {
            get { return this._page; }
        }

        #endregion

    }

    public delegate void TabPageChangeHandler(TabPageChangeEventArgs e);
}
