using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace Paway.Ticket.UI
{
    public class vTabCollection : CollectionWidthEvents
    {
        #region 变量
        private int _lockUpdate = 0;
        #endregion

        #region 自定义事件
        [Browsable(false)]
        public event CollectionChangeEventHandler CollectionChanged;
        #endregion

        #region 属性
        public vTabPage this[int index]
        {
            get
            {
                if (index < 0 || base.List.Count - 1 < index)
                    return null;
                return (vTabPage)base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }

        [Browsable(false)]
        public virtual int DrawnCount
        {
            get
            {
                int count = base.Count, res = 0;
                if (count == 0) return 0;
                for (int i = 0; i < count; i++)
                {
                    if (this[i].IsDrawn)
                        res++;
                }
                return res;
            }
        }

        public virtual vTabPage LastVisible
        {
            get
            {
                for (int i = base.Count - 1; i > 0; i--)
                {
                    if (this[i].Visible)
                        return this[i];
                }
                return null;
            }
        }

        public virtual vTabPage FirstVisible
        {
            get
            {
                for (int i = 0; i < base.Count; i++)
                {
                    if (this[i].Visible)
                        return this[i];
                }
                return null;
            }
        }

        [Browsable(false)]
        public virtual int VisibleCount
        {
            get
            {
                int count = base.Count, res = 0;
                if (count == 0) return 0;
                for (int i = 0; i < count; i++)
                {
                    if (this[i].Visible)
                        res++;
                }
                return res;
            }
        }
        #endregion

        #region 方法
        protected virtual void BeginUpdate()
        {
            this._lockUpdate++;
        }

        protected virtual void EndUpdate()
        {
            if (--this._lockUpdate == 0)
                this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
        }

        public virtual void AddRange(vTabPage[] pages)
        {
            this.BeginUpdate();
            try
            {
                foreach (vTabPage page in pages)
                {
                    base.List.Add(page);
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }

        public virtual void Assign(vTabCollection collection)
        {
            this.BeginUpdate();
            try
            {
                base.Clear();
                for (int i = 0; i < collection.Count; i++)
                {
                    vTabPage page = collection[i];
                    vTabPage newPage = new vTabPage();
                    newPage.Assign(page);
                    this.Add(newPage);
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }

        public virtual int Add(vTabPage page)
        {
            int res = base.IndexOf(page);
            if (res == -1) res = base.List.Add(page);
            return res;
        }

        public virtual void Remove(vTabPage page)
        {
            if (base.List.Contains(page))
                base.List.Remove(page);
        }

        public virtual vTabPage MoveTo(int newIndex, vTabPage page)
        {
            int currentIndex = base.List.IndexOf(page);
            if (currentIndex >= 0)
            {
                base.RemoveAt(currentIndex);
                this.Insert(0, page);
                return page;
            }
            return null;
        }

        public virtual int IndexOf(vTabPage page)
        {
            return base.List.IndexOf(page);
        }

        public virtual bool Contains(vTabPage page)
        {
            return base.List.Contains(page);
        }

        public virtual void Insert(int index, vTabPage page)
        {
            if (this.Contains(page)) return;
            base.List.Insert(index, page);
        }

        protected override void OnInsert(int index, object value)
        {
            vTabPage page = value as vTabPage;
            page.Changed += new EventHandler(OnPage_Changed);
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
        }

        protected override void OnRemove(int index, object value)
        {
            base.OnRemove(index, value);
            vTabPage page = value as vTabPage;
            page.Changed -= new EventHandler(OnPage_Changed);
            OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, value));
        }

        protected override void OnClear()
        {
            if (base.Count == 0) return;
            this.BeginUpdate();
            try
            {
                for (int i = base.Count -1; i >= 0; i--)
                {
                    base.RemoveAt(i);
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }

        #endregion

        #region 事件
        void OnPage_Changed(object sender, EventArgs e)
        {
            this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, sender));
        }
        #endregion

        #region 激发事件的方法
        protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
        {
            if (this.CollectionChanged != null)
                this.CollectionChanged(this, e);
        }
        #endregion
    }
}
