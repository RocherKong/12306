using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace Paway.Ticket.UI
{
    public class BlockCollection : BaseCollection, IList, ICollection, IEnumerable
    {
        #region 变量

        private BlockContainer _owner = null;
        private ArrayList _arrayList = null;

        #endregion

        #region 构造函数
        public BlockCollection(BlockContainer owner)
        {
            this._arrayList = new ArrayList();
            this._owner = owner;
        }
        #endregion

        #region 方法
        private void InvalidateOwner()
        {
            if (this._owner != null)
            {
                this._owner.Invalidate();
            }
        }
        private void InvalidateOwner(Rectangle rect)
        {
            if (this._owner != null)
            {
                this._owner.Invalidate(rect);
            }
        }

        public int Add(Block block)
        {
            int result = this._arrayList.Add(block);
            if (result > -1)
                this.InvalidateOwner();
            return result;
        }
        public void Clear()
        {
            this._arrayList.Clear();
            this.InvalidateOwner();
        }
        public bool Contains(Block block)
        {
            return this._arrayList.Contains(block);
        }
        public int IndexOf(Block block)
        {
            return this._arrayList.IndexOf(block);
        }
        public void Insert(int index, Block block)
        {
            this._arrayList.Insert(index, block);
            this.InvalidateOwner();
        }
        public void Remove(Block block)
        {
            this._arrayList.Remove(block);
            this.InvalidateOwner();
        }
        public void RemoveAt(int index)
        {
            this._arrayList.RemoveAt(index);
            this.InvalidateOwner();
        }
        public Block this[int index]
        {
            get { return this._arrayList[index] as Block; }
            set
            {
                Block block = this._arrayList[index] as Block;
                if (block == value)
                    return;
                block = value;
                this.InvalidateOwner(block.Bounds);
            }
        }

        new public void CopyTo(Array array, int index)
        {
            this._arrayList.CopyTo(array, index);
        }

        protected override ArrayList List
        {
            get
            {
                return this._arrayList;
            }
        }
        #endregion

        #region IList
        int IList.Add(object value)
        {
            return this.Add(value as Block);
        }
        void IList.Clear()
        {
            this.Clear();
        }
        bool IList.Contains(object value)
        {
            return this.Contains(value as Block);
        }
        int IList.IndexOf(object value)
        {
            return this.IndexOf(value as Block);
        }
        void IList.Insert(int index, object value)
        {
            this.Insert(index, value as Block);
        }
        bool IList.IsFixedSize
        {
            get { return this._arrayList.IsFixedSize; }
        }
        bool IList.IsReadOnly
        {
            get { return this._arrayList.IsReadOnly; }
        }
        void IList.Remove(object value)
        {
            this.Remove(value as Block);
        }
        void IList.RemoveAt(int index)
        {
            this.RemoveAt(index);
        }
        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = value as Block;
            }
        }
        #endregion

        #region ICollection
        void ICollection.CopyTo(Array array, int index)
        {
            this.CopyTo(array, index);
        }
        int ICollection.Count
        {
            get { return this._arrayList.Count; }
        }
        bool ICollection.IsSynchronized
        {
            get { return this._arrayList.IsSynchronized; }
        }
        object ICollection.SyncRoot
        {
            get { return this; }
        }
        #endregion

        #region IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._arrayList.GetEnumerator();
        }
        #endregion
    }
}
