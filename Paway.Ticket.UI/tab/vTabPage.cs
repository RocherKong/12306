using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using Paway.Ticket.UI.Enums;

namespace Paway.Ticket.UI
{
    [Designer(typeof(TabPageDesigner))]
    [ToolboxItem(false)]
    public class vTabPage : Panel
    {
        #region 变量
        private bool _isDrawn = false;
        private bool _selected = false;
        private bool _isCloseDown = false;
        private bool _isStripDown = false;
        private bool _canClose = true;
        private bool _visible = true;
        private string _title = string.Empty;
        private Image _image = null;
        private RectangleF _stripRect = RectangleF.Empty;
        private EMouseState _stripState = EMouseState.Normal;

        #endregion

        #region 自定义事件
        public event EventHandler Changed;
        #endregion

        #region 构造函数
        public vTabPage()
            : this(string.Empty, null)
        {
            this.BackColor = Color.White;
        }
        public vTabPage(Control displayControl)
            : this(string.Empty, null)
        { }
        public vTabPage(string title, Control displayControl)
        {
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ContainerControl, true);
            this.UpdateStyles();
            this.UpdateText(title, displayControl);
            if (displayControl != null)
                this.Controls.Add(displayControl);

        }
        #endregion

        #region 属性
        [Browsable(false), DefaultValue(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsDrawn
        {
            get { return this._isDrawn; }
            set
            {
                if (this._isDrawn == value)
                    return;
                this._isDrawn = value;
            }
        }

        [DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Selected
        {
            get { return this._selected; }
            set
            {
                if (this._selected == value)
                    return;
                this._selected = value;
            }
        }

        [DefaultValue(null)]
        public Image Image
        {
            get { return this._image; }
            set { this._image = value; }
        }

        [DefaultValue("Name")]
        public string Title
        {
            get { return this._title; }
            set
            {
                if (this._title == value)
                    return;
                this._title = value;
                this.OnChanged();
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }

        [DefaultValue(true)]
        public new bool Visible
        {
            get { return this._visible; }
            set
            {
                if (this._visible == value)
                    return;
                this._visible = value;
                this.OnChanged();
            }
        }

        internal RectangleF StripBounds
        {
            get { return this._stripRect; }
            set { this._stripRect = value; }
        }

        internal Rectangle StripRect
        {
            get { return Rectangle.Round(this.StripBounds); }
        }

        internal bool IsStripDown
        {
            get { return this._isStripDown; }
            set { this._isStripDown = value; }
        }

        internal EMouseState StripState
        {
            get { return this._stripState; }
            set { this._stripState = value; }
        }

        internal bool IsCloseDown
        {
            get { return this._isCloseDown; }
            set { this._isCloseDown = value; }
        }
        #endregion

        #region 方法
        public void Assign(vTabPage page)
        {
            this.Visible = page.Visible;
            this.Text = page.Text;
            this.Tag = page.Tag;
        }

        private void UpdateText(string title, Control displayControl)
        {
            if (displayControl != null && displayControl is vTabPage)
            {
                this.Title = (displayControl as vTabPage).Title;
            }
            else if (title.Length <= 0 && displayControl != null)
            {
                this.Title = displayControl.Text;
            }
            else if (title != null)
            {
                this.Title = title;
            }
            else
            {
                this.Title = string.Empty;
            }
        }
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return this.Title;
        }

        #endregion

        #region 激发事件的方法
        protected internal virtual void OnChanged()
        {
            if (this.Changed != null)
                this.Changed(this, EventArgs.Empty);
        }
        #endregion

    }
}
