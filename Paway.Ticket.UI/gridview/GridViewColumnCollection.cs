using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Paway.Ticket.UI
{
    public class GridViewColumnCollection : BaseCollection
    {
        #region 变量
        private GridView _owner = null;
        private List<GridViewColumn> _list = null;
        #endregion

        #region 构造函数
        public GridViewColumnCollection(GridView owner)
            : base()
        {
            if (this._list == null)
                this._list = new List<GridViewColumn>();
            this._owner = owner;
        }
        #endregion

        #region 方法
        public virtual void Add(GridViewColumn column)
        {
            this._list.Add(column);
        }
        public virtual GridViewColumn this[int index]
        {
            get
            {
                return this._list[index];
            }
        }

        #endregion

        public override int Count
        {
            get
            {
                return this._list.Count;
            }
        }
    }
}
