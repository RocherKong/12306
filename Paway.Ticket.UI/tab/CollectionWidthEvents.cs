using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace Paway.Ticket.UI
{
    public delegate void CollectionClear();

    public delegate void CollectionChange(int index, object value);

    public class CollectionWidthEvents : CollectionBase
    {
        #region 变量
        
        private int _suspendCount = 0;

        #endregion

        #region 自定义事件

        [Browsable(false)]
        public event CollectionClear Clearing;

        [Browsable(false)]
        public event CollectionClear Cleared;

        [Browsable(false)]
        public event CollectionChange Inserting;

        [Browsable(false)]
        public event CollectionChange Inserted;

        [Browsable(false)]
        public event CollectionChange Removing;

        [Browsable(false)]
        public event CollectionChange Removed;

        #endregion

        #region 方法
        public void SuspendEvents()
        {
            this._suspendCount++;
        }

        public void ResumeEvents()
        {
            --this._suspendCount;
        }

        protected int IndexOf(object value)
        {
            return base.List.IndexOf(value);
        }

        #endregion

        #region 属性
        public bool IsSuspended
        {
            get { return (this._suspendCount > 0); }
        }

        #endregion

        #region Override Methods

        protected override void OnClear()
        {
            if (!this.IsSuspended)
            {
                if (this.Clearing != null)
                    this.Clearing();
            }
        }

        protected override void OnClearComplete()
        {
            if (!this.IsSuspended)
            {
                if (this.Cleared != null)
                    this.Cleared();
            }
        }

        protected override void OnInsert(int index, object value)
        {
            if (!this.IsSuspended)
            {
                if (this.Inserting != null)
                    this.Inserting(index, value);
            }
        }

        protected override void OnInsertComplete(int index, object value)
        {
            if (!this.IsSuspended)
            {
                if (this.Inserted != null)
                    this.Inserted(index, value);
            }
        }

        protected override void OnRemove(int index, object value)
        {
            if (!this.IsSuspended)
            {
                if (this.Removing != null)
                    this.Removing(index, value);
            }
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            if (!this.IsSuspended)
            {
                if (this.Removed != null)
                    this.Removed(index, value);
            }
        }

        #endregion
    }
}
