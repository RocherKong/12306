using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using Paway.Ticket.Win32;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using Paway.Ticket.UI.Enums;

namespace Paway.Ticket
{
    /// <summary>
    /// 窗体的基类，完成一部分共有的功能
    /// </summary>
    public class FormBase : Form
    {
        #region 变量
        /// <summary>
        /// 系统按钮
        /// </summary>
        private ESysButton _sysButton = ESysButton.Normal;
        /// <summary>
        /// 关闭按钮的鼠标状态
        /// </summary>
        private EMouseState _closeState = EMouseState.Normal;
        /// <summary>
        /// 最大化按钮的鼠标状态
        /// </summary>
        private EMouseState _maxState = EMouseState.Normal;
        /// <summary>
        /// 最小化按钮的鼠标状态
        /// </summary>
        private EMouseState _minState = EMouseState.Normal;
        /// <summary>
        /// 记录窗体大小
        /// </summary>
        private Size _formSize = Size.Empty;
        /// <summary>
        /// 记录窗体位置
        /// </summary>
        private Point _formPoint = Point.Empty;
        /// <summary>
        /// 指定窗体窗口如何显示
        /// </summary>
        private FormWindowState _windowState = FormWindowState.Normal;
        /// <summary>
        /// 是否允许改变窗口大小
        /// </summary>
        private bool _isResize = true;
        /// <summary>
        /// 是否显示图标
        /// </summary>
        private bool _showIcon = true;
        /// <summary>
        /// 是否透明化
        /// </summary>
        private bool _isTransfer = false;
        /// <summary>
        /// 窗体字体信息
        /// </summary>
        private Font _font = new Font("宋体", 9f, FontStyle.Bold);
        private FormWindowState _lastWindowState = FormWindowState.Normal;
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化 Paway.Windows.Forms.FormBase 类的新实例。
        /// </summary>
        public FormBase()
        {
            this.SetStyle(
               ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.ResizeRedraw |
               ControlStyles.Selectable |
               ControlStyles.ContainerControl |
               ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Opaque, false);
            this.UpdateStyles();
            this.Initialize();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 是否允许改变窗口大小
        /// </summary>
        [Description("是否允许改变窗口大小"), DefaultValue(true)]
        public virtual bool IsResize
        {
            get { return this._isResize; }
            set { _isResize = value; }
        }
        /// <summary>
        /// 指定窗体窗口如何显示
        /// </summary>
        [Description("指定窗体窗口如何显示")]
        new public virtual FormWindowState WindowState
        {
            get { return this._windowState; }
            set
            {
                if (this._windowState == value)
                    return;
                switch (value)
                {
                    case FormWindowState.Maximized:
                        if (this._windowState != FormWindowState.Minimized && this._windowState != FormWindowState.Maximized || this._windowState == FormWindowState.Normal)
                        {
                            this._formSize = this.Size;
                            this._formPoint = this.Location;
                            this.Location = new Point(0, 0);
                            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                        }
                        break;
                    case FormWindowState.Minimized:
                        this._lastWindowState = this._windowState;
                        base.WindowState = FormWindowState.Minimized;
                        break;
                    case FormWindowState.Normal:
                        if (this._windowState != FormWindowState.Minimized)
                        {
                            this.Size = this._formSize;
                            this.Location = this._formPoint;
                            base.WindowState = FormWindowState.Normal;
                        }
                        break;
                }
                this._windowState = value;
            }
        }
        /// <summary>
        /// 是否透明
        /// </summary>
        public bool IsTransfer
        {
            get { return this._isTransfer; }
            set { this._isTransfer = value; }
        }
        /// <summary>
        /// 封装创建控件时所需的信息。
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                if (!this._isTransfer)
                    return base.CreateParams;
                else
                {
                    CreateParams param = base.CreateParams;
                    param.ExStyle = 0x00080000;
                    return param;
                }
            }
        }
        /// <summary>
        /// 是否显示图标
        /// </summary>
        new public virtual bool ShowIcon
        {
            get { return this._showIcon; }
            set
            {
                this._showIcon = value;
                base.Invalidate(this.TitleBarRect);
            }
        }
        /// <summary>
        /// 窗体标题文字
        /// </summary>
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                base.Invalidate(this.TitleBarRect);
            }
        }
        /// <summary>
        /// 窗体文字信息
        /// </summary>
        public override Font Font
        {
            get
            {
                return this._font;
            }
            set
            {
                this._font = value;
                this.Invalidate(this.TextRect);
            }
        }
        /// <summary>
        /// 系统控制按钮
        /// </summary>
        [Description("系统控制按钮的显示与隐藏")]
        public ESysButton SysButton
        {
            get { return this._sysButton; }
            set
            {
                this._sysButton = value;
                base.Invalidate(this.TitleBarRect);
            }
        }
        /// <summary>
        /// 窗体与内部元素之间的距离
        /// </summary>
        new protected virtual Padding DockPadding
        {
            get { return new Padding(3, 26, 3, 3); }
        }
        /// <summary>
        /// 系统控制按钮区域
        /// </summary>
        protected virtual Rectangle SysBtnRect
        {
            get { return new Rectangle(); }
        }
        /// <summary>
        /// 标题栏区域
        /// </summary>
        protected virtual Rectangle TitleBarRect
        {
            get { return new Rectangle(0, 0, this.Width, 30); }
        }
        /// <summary>
        /// 关闭按钮区域
        /// </summary>
        protected virtual Rectangle CloseRect
        {
            get { return Rectangle.Empty; }
        }
        /// <summary>
        /// 最小化按钮区域
        /// </summary>
        protected virtual Rectangle MiniRect
        {
            get { return Rectangle.Empty; }
        }
        /// <summary>
        /// 最大化按钮区域
        /// </summary>
        protected virtual Rectangle MaxRect
        {
            get { return Rectangle.Empty; }
        }
        /// <summary>
        /// 图标显示区域
        /// </summary>
        protected virtual Rectangle IconRect
        {
            get { return new Rectangle(4, 4, 16, 16); }
        }
        /// <summary>
        /// 标题文本显示区域
        /// </summary>
        protected virtual Rectangle TextRect
        {
            get
            {
                int width = this.TitleBarRect.Width - this.IconRect.Width - 15;
                int height = this.TitleBarRect.Height - 10;
                Rectangle textRect = new Rectangle(8, 2, width, height);
                if (this.ShowIcon)
                    textRect.X = this.IconRect.Width + 8;
                return textRect;
            }
        }
        /// <summary>
        /// 关闭按钮当前的鼠标状态
        /// </summary>
        protected EMouseState CloseState
        {
            get { return this._closeState; }
            set
            {
                this._closeState = value;
                base.Invalidate(this.CloseRect);
            }
        }
        /// <summary>
        /// 最大化按钮当前的鼠标状态
        /// </summary>
        protected EMouseState MaxState
        {
            get { return this._maxState; }
            set
            {
                this._maxState = value;
                base.Invalidate(this.MaxRect);
            }
        }
        /// <summary>
        /// 最小化按钮当前的鼠标状态
        /// </summary>
        protected EMouseState MinState
        {
            get { return this._minState; }
            set
            {
                this._minState = value;
                base.Invalidate(this.MiniRect);
            }
        }
        #endregion

        #region 方法

        /// <summary>
        /// 初始化窗口
        /// </summary>
        protected virtual void Initialize()
        {
            this.FormBorderStyle = FormBorderStyle.None;
        }
        /// <summary>
        /// 拖动窗口大小
        /// </summary>
        /// <param name="m"></param>
        private void WmNcHitTest(ref Message m)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                int wparam = m.LParam.ToInt32();

                Point point = new Point(
                    NativeMethods.LOWORD(wparam),
                    NativeMethods.HIWORD(wparam));

                point = PointToClient(point);
                if (_isResize)
                {
                    if (point.X <= 3)
                    {
                        if (point.Y <= 3)
                            m.Result = (IntPtr)WM_NCHITTEST.HTTOPLEFT;
                        else if (point.Y > Height - 3)
                            m.Result = (IntPtr)WM_NCHITTEST.HTBOTTOMLEFT;
                        else
                            m.Result = (IntPtr)WM_NCHITTEST.HTLEFT;
                    }
                    else if (point.X >= Width - 3)
                    {
                        if (point.Y <= 3)
                            m.Result = (IntPtr)WM_NCHITTEST.HTTOPRIGHT;
                        else if (point.Y >= Height - 3)
                            m.Result = (IntPtr)WM_NCHITTEST.HTBOTTOMRIGHT;
                        else
                            m.Result = (IntPtr)WM_NCHITTEST.HTRIGHT;
                    }
                    else if (point.Y <= 3)
                    {
                        m.Result = (IntPtr)WM_NCHITTEST.HTTOP;
                    }
                    else if (point.Y >= Height - 3)
                    {
                        m.Result = (IntPtr)WM_NCHITTEST.HTBOTTOM;
                    }
                    else
                    {
                        if (!this.SysBtnRect.Contains(point))
                            m.Result = (IntPtr)2;
                    }
                }
                else
                {
                    if (!this.SysBtnRect.Contains(point))
                        m.Result = (IntPtr)2;
                }
            }
        }
        /// <summary>
        /// 设置图片为窗体，透明区域根据 opacity 的值决定透明度
        /// </summary>
        /// <param name="bitmap">透明位图</param>
        /// <param name="opacity">透明度的值0~255</param>
        public void SetBitmap(Bitmap bitmap, byte opacity)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");

            // The ideia of this is very simple,
            // 1. Create a compatible DC with screen;
            // 2. Select the bitmap with 32bpp with alpha-channel in the compatible DC;
            // 3. Call the UpdateLayeredWindow.

            IntPtr screenDc = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr memDc = NativeMethods.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));  // grab a GDI handle from this GDI+ bitmap
                oldBitmap = NativeMethods.SelectObject(memDc, hBitmap);

                SIZE size = new SIZE(bitmap.Width, bitmap.Height);
                POINT pointSource = new POINT(0, 0);
                POINT topPos = new POINT(Left, Top);
                BLENDFUNCTION blend = new BLENDFUNCTION();
                blend.BlendOp = Consts.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = opacity;
                blend.AlphaFormat = Consts.AC_SRC_ALPHA;

                NativeMethods.UpdateLayeredWindow(Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, Consts.ULW_ALPHA);
            }
            finally
            {
                NativeMethods.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    NativeMethods.SelectObject(memDc, oldBitmap);
                    //Windows.DeleteObject(hBitmap); // The documentation says that we have to use the Windows.DeleteObject... but since there is no such method I use the normal DeleteObject from Win32 GDI and it's working fine without any resource leak.
                    NativeMethods.DeleteObject(hBitmap);
                }
                NativeMethods.DeleteDC(memDc);
            }
        }

        #endregion

        #region Override Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            if (this.ShowIcon)//绘画图标
            {
                Bitmap iconImage = this.Icon.ToBitmap();
                g.DrawImage(iconImage, this.IconRect);
            }
            //绘制标题文字
            if (!string.IsNullOrEmpty(this.Text))
            {
                TextRenderer.DrawText(g, this.Text, this._font, this.TextRect, this.ForeColor, TextFormatFlags.VerticalCenter);
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                switch (this.StartPosition)
                {
                    case FormStartPosition.CenterParent:
                        if (this.Parent != null)
                        {
                            this.Location = new Point(
                               (this.Parent.Width - this.Width) / 2,
                               (this.Parent.Height - this.Height) / 2);
                        }
                        break;
                    case FormStartPosition.CenterScreen:
                        this.Location = new Point(
                            (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                            (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
                        break;
                    case FormStartPosition.Manual:
                    case FormStartPosition.WindowsDefaultBounds:
                    case FormStartPosition.WindowsDefaultLocation:
                        break;
                }
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            base.Padding = this.DockPadding;
        }
        protected override void WndProc(ref Message m)
        {
            try
            {
                if (!this.DesignMode)
                {
                    switch (m.Msg)
                    {
                        case (int)WindowsMessage.WM_SYSCOMMAND:  // 屏蔽系统的最大化(利用自定义的最大化替换系统的最大化)
                            if (m.WParam == (IntPtr)61490)
                            {
                                if (this.MaximizeBox)
                                    this.WindowState = FormWindowState.Maximized;
                            }
                            else if (m.WParam == (IntPtr)61728)
                            {
                                if (this._lastWindowState == FormWindowState.Normal)
                                    this.WindowState = FormWindowState.Normal;
                                else if (this.MaximizeBox)
                                    this.WindowState = FormWindowState.Maximized;
                                NativeMethods.ShowWindow(this.Handle, Consts.SW_SHOWNOACTIVATE);
                                this.Activate();
                            }
                            else
                            {
                                base.WndProc(ref m);
                            }
                            break;
                        case (int)WindowsMessage.WM_NCPAINT:
                        case (int)WindowsMessage.WM_NCCALCSIZE:
                            break;
                        case (int)WindowsMessage.WM_NCACTIVATE:
                            if (m.WParam == (IntPtr)0)
                            {
                                m.Result = (IntPtr)1;
                            }
                            break;
                        case 50060:
                            break;
                        case (int)WindowsMessage.WM_NCHITTEST:
                            base.WndProc(ref m);
                            this.WmNcHitTest(ref m);
                            break;
                        default:
                            base.WndProc(ref m);
                            break;
                    }
                }
                else
                {
                    base.WndProc(ref m);
                }
            }
            catch { }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Point point = e.Location;
            if (e.Button == MouseButtons.Left)
            {
                if (this.CloseRect.Contains(point))
                    this.CloseState = EMouseState.MouseDown;
                else if (this.MiniRect.Contains(point))
                    this.MinState = EMouseState.MouseDown;
                else if (this.MaxRect.Contains(point))
                    this.MaxState = EMouseState.MouseDown;
            }
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (this.MaximizeBox && e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                if (this.WindowState == FormWindowState.Maximized)
                    this.WindowState = FormWindowState.Normal;
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Point point = e.Location;
            if (this._closeState != EMouseState.MouseDown)
            {
                if (this.CloseRect.Contains(point))
                    this.CloseState = EMouseState.MouseMove;
                else
                    this.CloseState = EMouseState.Normal;
            }
            if (this._minState != EMouseState.MouseDown)
            {
                if (this.MiniRect.Contains(point))
                    this.MinState = EMouseState.MouseMove;
                else
                    this.MinState = EMouseState.Normal;
            }
            if (this.SysButton == ESysButton.Normal && this._maxState != EMouseState.MouseDown)
            {
                if (this.MaxRect.Contains(point))
                    this.MaxState = EMouseState.MouseMove;
                else
                    this.MaxState = EMouseState.Normal;
            }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.CloseState = EMouseState.Normal;
            this.MaxState = EMouseState.Normal;
            this.MinState = EMouseState.Normal;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button != MouseButtons.Left)
                return;
            Point point = e.Location;
            if (this.CloseRect.Contains(point) && this._closeState == EMouseState.MouseDown)
            {
                this.CloseState = EMouseState.MouseMove;
                this.Close();
            }
            else
            {
                this.CloseState = EMouseState.Normal;
            }
            if (this.MiniRect.Contains(point) && this._minState == EMouseState.MouseDown)
            {
                this.MinState = EMouseState.MouseMove;
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.MinState = EMouseState.Normal;
            }
            if (this.MaxRect.Contains(point) && this._maxState == EMouseState.MouseDown)
            {
                this.MaxState = EMouseState.MouseMove;
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;
                }
                else
                {

                    this.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                this.MaxState = EMouseState.Normal;
            }
        }
        #endregion
    }
}
