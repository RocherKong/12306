using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Paway.Ticket.UI
{
    public class GridView : Control, ISupportInitialize
    {
        #region 常量
        private static readonly Size DEFAULT_SIZE = new Size(240, 150);
        #endregion

        #region 变量
        private bool _isInit = false;
        private GridViewColumnCollection _columns = null;
        #endregion

        #region 构造函数

        public GridView()
            : base()
        {
            this.BeginInit();
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.FixedHeight |
                ControlStyles.FixedWidth |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.UserPaint, true);
            this.UpdateStyles();
            this._columns = new GridViewColumnCollection(this);
            this.EndInit();
        }

        #endregion

        #region 属性
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GridViewColumnCollection Columns
        {
            get 
            {
                if (this._columns == null)
                    this._columns = new GridViewColumnCollection(this);
                return this._columns;
            }
        }
        #endregion

        #region Override Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            if (this.Columns.Count > 0)
            {
                foreach (GridViewColumn column in this.Columns)
                {
                    
                }
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;
            Rectangle bounds = e.ClipRectangle;
            bounds.Width--;
            bounds.Height--;
            g.DrawRectangle(Pens.Black, bounds);
        }
        #endregion

        #region ISupportInitialize

        public void BeginInit()
        {
            this._isInit = false;
        }

        public void EndInit()
        {
            this._isInit = true;
        }

        #endregion
    }
}
