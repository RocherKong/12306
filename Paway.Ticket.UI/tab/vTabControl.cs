using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using Paway.Ticket.UI.Enums;

namespace Paway.Ticket.UI
{
    [Designer(typeof(TabControlDesiner))]
    [DefaultEvent("TabPageSelectionChanged")]
    [DefaultProperty("TabPages")]
    [ToolboxItem(true)]
    public class vTabControl : BaseStyledPanel, ISupportInitialize, IDisposable
    {
        #region 资源图片
        private static readonly Image ImageToolbar_Hover = AppResource.GetImage("Images.toolbar_hover.png");
        #endregion

        #region 变量
        private static readonly Size HEADER_SIZE = new Size(80, 75);

        private int _startPos = 0;
        private vTabPage _selecedPage = null;

        private vTabCollection _tabPages;
        private StringFormat _sf = null;
        private static Font _defaultFont = new Font("Tahoma", 8.25f, FontStyle.Regular);

        private bool _isIniting = false;
        #endregion

        #region 自定义事件
        public event TabPageClosingHandler TabPageClosing;
        public event TabPageChangeHandler TabPageSelectionChange;
        public event EventHandler TabPageClosed;
        #endregion

        #region 构造函数
        public vTabControl()
        {
            this.BeginInit();

            this.SetStyle(
                ControlStyles.ContainerControl |
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.Selectable, true);
            this.UpdateStyles();

            this._tabPages = new vTabCollection();
            this._tabPages.CollectionChanged += new CollectionChangeEventHandler(OnCollectionChanged);

            this.Font = _defaultFont;
            this._sf = new StringFormat();
            this.EndInit();

            this.UpdateLayout();
        }
        #endregion

        #region 事件
        void OnCollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            vTabPage page = e.Element as vTabPage;
            if (e.Action == CollectionChangeAction.Add)
            {
                this.Controls.Add(page);
                this.OnTabPageChanged(new TabPageChangeEventArgs(page, ETabPageChangeType.Added));
            }
            else if (e.Action == CollectionChangeAction.Remove)
            {
                this.Controls.Remove(page);
                OnTabPageChanged(new TabPageChangeEventArgs(page, ETabPageChangeType.Removed));
            }
            else
            {
                this.OnTabPageChanged(new TabPageChangeEventArgs(page, ETabPageChangeType.Changed));
            }

            this.UpdateLayout();
            base.Invalidate();
        }

        #endregion

        #region 属性
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public vTabCollection TabPages
        {
            get { return this._tabPages; }
        }

        [DefaultValue(null), RefreshProperties(RefreshProperties.All), Browsable(false)]
        public vTabPage SelectedPage
        {
            get { return this._selecedPage; }
            set
            {
                if (value == null)
                    return;
                if (this._selecedPage == null && this._tabPages.Count > 0)
                {
                    vTabPage page = this._tabPages[0];
                    if (page.Visible)
                    {
                        this._selecedPage = page;
                        this._selecedPage.Selected = true;
                    }
                }
                else
                {
                    this._selecedPage = value;
                    this._selecedPage.Selected = false;
                }
                foreach (vTabPage page in this._tabPages)
                {
                    if (page == this._selecedPage)
                    {
                        this.SelectPage(page);
                        page.Dock = DockStyle.Fill;
                        page.Show();
                    }
                    else
                    {
                        this.UnSelectPage(page);
                        page.Hide();
                    }
                }

                this.SelectPage(this._selecedPage);
                base.Invalidate();

                if (this._selecedPage != null && !this._selecedPage.IsDrawn)
                {
                    this._tabPages.MoveTo(0, this._selecedPage);
                    base.Invalidate();
                }

                this.OnTabPageChanged(new TabPageChangeEventArgs(this._selecedPage, ETabPageChangeType.SelectionChanged));
            }
        }

        [DefaultValue(typeof(Size), "500, 500")]
        public new Size Size
        {
            get { return base.Size; }
            set
            {
                base.Size = value;
                this.UpdateLayout();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ControlCollection Controls
        {
            get { return base.Controls; }
        }
        protected override Size DefaultSize
        {
            get { return new Size(500, 500); }
        }
        #endregion

        #region 方法

        public vTabPage GetTabPageByPoint(Point pt)
        {
            vTabPage page = null;
            bool found = false;

            for (int i = 0; i < this._tabPages.Count; i++)
            {
                vTabPage currentPage = this._tabPages[i];
                if (currentPage.StripBounds.Contains(pt) && currentPage.Visible && currentPage.IsDrawn)
                {
                    page = currentPage;
                    found = true;
                }
                if (found)
                    break;
            }
            return page;
        }

        public void AddTab(vTabPage page)
        {
            this.AddTab(page, false);
        }

        public void AddTab(vTabPage page, bool autoSelect)
        {
            page.Dock = DockStyle.Fill;
            this._tabPages.Add(page);

            if ((autoSelect && page.Visible) || (page.Visible && this._tabPages.DrawnCount < 1))
            {
                this.SelectedPage = page;
                this.SelectPage(page);
            }
        }

        public void RemoveTab(vTabPage page)
        {
            int tabIndex = this._tabPages.IndexOf(page);
            if (tabIndex >= 0)
            {
                this.UnSelectPage(page);
                this._tabPages.Remove(page);
            }

            if (this._tabPages.Count > 0)
            {
                if (this._tabPages[tabIndex - 1] != null)
                    this.SelectedPage = this._tabPages[tabIndex - 1];
                else
                    this.SelectedPage = this._tabPages.FirstVisible;

            }
        }

        internal void SelectPage(vTabPage page)
        {
            if (page != null)
            {
                page.Dock = DockStyle.Fill;
                page.Visible = true;
                page.Selected = true;
            }
        }

        internal void UnSelectPage(vTabPage page)
        {
            page.Selected = false;
        }

        /// <summary>
        /// 更新控件的布局
        /// </summary>
        private void UpdateLayout()
        {
            this._sf.Trimming = StringTrimming.EllipsisCharacter;
            this._sf.FormatFlags |= StringFormatFlags.NoWrap;
            this._sf.FormatFlags &= StringFormatFlags.DirectionRightToLeft;

            base.DockPadding.Top = HEADER_SIZE.Height + 1;
            base.DockPadding.Bottom = 1;
            base.DockPadding.Right = 1;
            base.DockPadding.Left = 1;
        }

        private void SetDefaultSelected()
        {
            if (this._tabPages.Count > 0)
            {
                this._selecedPage = this._tabPages[0];
            }
            for (int i = 0; i < this._tabPages.Count; i++)
            {
                vTabPage page = this._tabPages[i];
                page.Dock = DockStyle.Fill;
            }
        }
        private void OnCalcTabPage(Graphics g, vTabPage page)
        {
            RectangleF buttonRect = new RectangleF(new Point(this._startPos, 0), HEADER_SIZE);
            page.StripBounds = buttonRect;
            this._startPos += (int)HEADER_SIZE.Width - 1;
        }
        /// <summary>
        /// 绘制选项卡的Header
        /// </summary>
        private void OnDrawTabPage(Graphics g, vTabPage page)
        {
            Rectangle pageRect = Rectangle.Round(page.StripBounds);
            if (page.Selected)
            {
                g.DrawImage(
                    ImageToolbar_Hover,
                    pageRect,
                    new Rectangle(Point.Empty, ImageToolbar_Hover.Size),
                    GraphicsUnit.Pixel);
            }
            else
            {
                switch (page.StripState)
                {
                    case EMouseState.Normal:
                    case EMouseState.MouseLeave:
                        break;
                    case EMouseState.MouseMove:
                    case EMouseState.MouseDown:
                    case EMouseState.MouseUp:
                        g.DrawImage(
                            ImageToolbar_Hover,
                            pageRect,
                            new Rectangle(Point.Empty, ImageToolbar_Hover.Size),
                            GraphicsUnit.Pixel);
                        break;
                }
            }
            if (page.Image != null)
            {
                Rectangle imageRect = new Rectangle();
                imageRect.X = pageRect.X + (pageRect.Width - 48) / 2;
                //imageRect.Y += 10;
                imageRect.Size = new Size(48, 48);
                g.DrawImage(
                    page.Image,
                    imageRect,
                    new Rectangle(Point.Empty, page.Image.Size),
                    GraphicsUnit.Pixel);
            }
            else
            {
                Rectangle imageRect = new Rectangle();
                imageRect.X = pageRect.X + (pageRect.Width - 48) / 2;
                imageRect.Size = new Size(48, 48);
                g.DrawRectangle(Pens.Red, imageRect);
            }
            //计算文字的绘制区域
            if (page.Title != string.Empty)
            {
                SizeF size = g.MeasureString(page.Title, page.Font);
                RectangleF textRect = new RectangleF();
                textRect.X = pageRect.X + (pageRect.Width - size.Width) / 2;
                textRect.Y = pageRect.Height - 20;
                textRect.Width = pageRect.Width;
                textRect.Height = 20;
                using (Brush brush = new SolidBrush(this.ForeColor))
                {
                    g.DrawString(page.Title, this.Font, brush, textRect, this._sf);
                }
            }
            page.IsDrawn = true;
        }
        #endregion

        #region Override Methods
        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);
            this.UpdateLayout();
            base.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            this.SetDefaultSelected();
            Rectangle borderRect = this.ClientRectangle;
            borderRect.Width--;
            borderRect.Height--;

            this._startPos = 0;
            if (this.DesignMode)
            {
                e.Graphics.DrawRectangle(SystemPens.ControlDark, borderRect);
            }
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            for (int i = 0; i < this._tabPages.Count; i++)
            {
                vTabPage currentPage = this._tabPages[i];
                if (!currentPage.Visible && !DesignMode)
                    continue;
                this.OnCalcTabPage(e.Graphics, currentPage);
                currentPage.IsDrawn = false;
                this.OnDrawTabPage(e.Graphics, currentPage);
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left)
                return;
            for (int i = 0; i < this._tabPages.Count; i++)
            {
                vTabPage page = this._tabPages[i];
                if (page.StripRect.Contains(e.Location))
                {
                    page.StripState = EMouseState.MouseDown;
                    page.IsStripDown = true;
                }
                else
                {
                    page.StripState = EMouseState.Normal;
                }
            }
            vTabPage tabPage = this.GetTabPageByPoint(e.Location);
            this.SelectedPage = tabPage;
            base.Invalidate();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            for (int i = 0; i < this._tabPages.Count; i++)
            {
                vTabPage page = this._tabPages[i];
                if (!page.IsStripDown)
                {
                    if (page.StripBounds.Contains(e.Location))
                        page.StripState = EMouseState.MouseMove;
                    else
                        page.StripState = EMouseState.Normal;
                    base.Invalidate(page.StripRect);
                }
            }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            for (int i = 0; i < this._tabPages.Count; i++)
            {
                vTabPage page = this._tabPages[i];
                if (!page.IsStripDown)
                {
                    page.StripState = EMouseState.Normal;
                    base.Invalidate(page.StripRect);
                }
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button != MouseButtons.Left)
                return;
            for (int i = 0; i < this._tabPages.Count; i++)
            {
                vTabPage page = this._tabPages[i];
                page.IsCloseDown = false;
                page.IsStripDown = false;
                page.StripState = EMouseState.Normal;
                base.Invalidate(page.StripRect);
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this._isIniting)
                return;
            this.UpdateLayout();
        }
        #endregion

        #region ISupportInitialize 成员

        public void BeginInit()
        {
            this._isIniting = true;
        }

        public void EndInit()
        {
            this._isIniting = false;
        }

        #endregion

        #region IDisposable 成员
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._tabPages.CollectionChanged -= new CollectionChangeEventHandler(OnCollectionChanged);

                foreach (vTabPage page in this._tabPages)
                {
                    if (page != null && page.IsDisposed)
                    {
                        page.Dispose();
                    }
                }

                if (this._sf != null)
                {
                    this._sf.Dispose();
                    this._sf = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion

        #region 激发事件的方法
        protected virtual void OnTabPageChanged(TabPageChangeEventArgs e)
        {
            if (this.TabPageSelectionChange != null)
                this.TabPageSelectionChange(e);
        }

        #endregion
    }
}
