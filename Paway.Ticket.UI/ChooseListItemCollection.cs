using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Paway.Ticket.UI
{
    public class ChooseListItemCollection : BaseCollection, IList, ICollection, IEnumerable
    {
        #region 变量

        private ChooseList _owner = null;
        private ArrayList _list = null;

        #endregion

        #region 构造函数

        public ChooseListItemCollection()
        {
            if (this._list == null)
                this._list = new ArrayList();
        }
        public ChooseListItemCollection(ChooseList owner)
            : this()
        {
            this._owner = owner;
        }

        #endregion

        #region 属性

        internal ChooseList Owner
        {
            get { return this._owner; }
        }
        protected override ArrayList List
        {
            get { return this._list; }
        }
        public override int Count
        {
            get { return this._list.Count; }
        }
        #endregion

        #region 方法
        
        public int Add(ChooseListItem item)
        {
            return this._list.Add(item);
        }
        public void Clear()
        {
            this._list.Clear();
        }
        public bool Contains(ChooseListItem item)
        {
            return this._list.Contains(item);
        }
        public void Insert(int index, ChooseListItem item)
        {
            this._list.Insert(index, item);
        }
        public void Remove(ChooseListItem item)
        {
            this._list.Remove(item);
        }
        public void RemoveAt(int index)
        {
            this._list.RemoveAt(index);
        }
        public ChooseListItem this[int index]
        {
            get { return this._list[index] as ChooseListItem; }
            set { this._list[index] = value; }
        }
        public int IndexOf(ChooseListItem value)
        {
            return this._list.IndexOf(value);
        }

        #endregion

        #region IList 成员

        int IList.Add(object value)
        {
            return this.Add(value as ChooseListItem);
        }

        void IList.Clear()
        {
            this.Clear();
        }

        bool IList.Contains(object value)
        {
            return this.Contains(value as ChooseListItem);
        }

        int IList.IndexOf(object value)
        {
            return this.IndexOf(value as ChooseListItem);
        }

        void IList.Insert(int index, object value)
        {
            this._list.Insert(index, value);
        }

        bool IList.IsFixedSize
        {
            get { return this._list.IsFixedSize; }
        }

        void IList.Remove(object value)
        {
            this.Remove(value as ChooseListItem);
        }

        void IList.RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set { this[index] = value as ChooseListItem; }
        }

        #endregion

        #region ICollection 成员

        void ICollection.CopyTo(Array array, int index)
        {
            this._list.CopyTo(array, index);
        }

        int ICollection.Count
        {
            get { return this._list.Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return this._list.IsSynchronized; }
        }

        object ICollection.SyncRoot
        {
            get { return this; }
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        #endregion
    }
}
