using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Paway.Ticket.Win32;
using Paway.Ticket.UI.Enums;

namespace Paway.Ticket
{
    /// <summary>
    /// 360 窗体
    /// </summary>
    public class _360Form : FormBase
    {
        #region 变量

        #region 资源图片

        /// <summary>
        /// 边框图片
        /// </summary>
        private static Image ImageBorder = AppResource.GetImage("Images.framemod.png");
        /// <summary>
        /// 关闭按钮图片
        /// </summary>
        private static Image ImageClose = AppResource.GetImage("Images.sys_button_close.png");
        /// <summary>
        /// 最小化按钮图片
        /// </summary>
        private static Image ImageMin = AppResource.GetImage("Images.sys_button_min.png");
        /// <summary>
        /// 最大化按钮图片
        /// </summary>
        private static Image ImageMax = AppResource.GetImage("Images.sys_button_max.png");
        /// <summary>
        /// 还原按钮图片
        /// </summary>
        private static Image ImageRestore = AppResource.GetImage("Images.sys_button_restore.png");
        /// <summary>
        /// 标题栏菜单按钮图片
        /// </summary>
        private static Image ImageMenu = AppResource.GetImage("Images.menu.png");

        #endregion

        /// <summary>
        /// 系统按钮与窗体右边边缘的间距
        /// </summary>
        private int _sysButtonPos = 4;
        /// <summary>
        /// 标题栏菜单按钮的鼠标状态
        /// </summary>
        private EMouseState _MenuState = EMouseState.Normal;
        private bool _showMenu = true;

        #endregion

        #region 构造函数
        /// <summary>
        /// 实例化 Paway.Windows.Forms._360form 新的实例。
        /// </summary>
        public _360Form()
            : base()
        {

        }

        #endregion

        #region 属性
        /// <summary>
        /// 系统控制按钮与右边框之间的距离
        /// </summary>
        [Description("系统控制按钮与右边框之间的距离")]
        public int SysButtonPos
        {
            get { return this._sysButtonPos; }
            set
            {
                this._sysButtonPos = value;
                this.Invalidate(this.MenuRect);
            }
        }
        /// <summary>
        /// 关闭按钮的矩形区域
        /// </summary>
        protected override Rectangle CloseRect
        {
            get
            {
                int width = ImageClose.Width / 4;
                int height = ImageClose.Height;
                int x = this.Width - width - this._sysButtonPos;
                int y = -1;

                return new Rectangle(x, y, width, height);
            }
        }
        /// <summary>
        /// 最大化按钮的矩形区域
        /// </summary>
        protected override Rectangle MaxRect
        {
            get
            {
                int width = ImageMax.Width / 4;
                int height = ImageMax.Height;
                int x = 0;
                int y = this.CloseRect.Y;
                if (this.MaximizeBox)
                {
                    x = this.Width - width - this.CloseRect.Width - this._sysButtonPos;
                }
                return new Rectangle(x, y, width, height);
            }
        }
        /// <summary>
        /// 最小化按钮的矩形区域
        /// </summary>
        protected override Rectangle MiniRect
        {
            get
            {
                int width = ImageMin.Width / 4;
                int height = ImageMin.Height;
                int x = 0;
                int y = this.CloseRect.Y;
                if (this.MaximizeBox)
                {
                    x = this.Width - width - this.CloseRect.Width - this.MaxRect.Width - this._sysButtonPos;
                }
                else
                {
                    x = this.Width - width - this.CloseRect.Width - this._sysButtonPos;
                }
                return new Rectangle(x, y, width, height);
            }
        }
        /// <summary>
        /// 标题栏菜单按钮的矩形区域
        /// </summary>
        protected virtual Rectangle MenuRect
        {
            get
            {
                int width = ImageMenu.Width / 4;
                int height = ImageMenu.Height;
                int x = 0;
                int y = this.CloseRect.Y;
                
                if (!this.MaximizeBox && !this.MinimizeBox)
                {
                    x = this.CloseRect.X - width;
                }
                else
                {
                    x = this.MiniRect.X - width;
                }
                return new Rectangle(x, y, width, height);
            }
        }
        /// <summary>
        /// 标题栏菜单按钮的鼠标的状态
        /// </summary>
        protected virtual EMouseState MenuState
        {
            get { return this._MenuState; }
            set
            {
                this._MenuState = value;
                this.Invalidate(this.MenuRect);
            }
        }
        protected override Rectangle SysBtnRect
        {
            get
            {
                return new Rectangle(
                    this.MenuRect.X, 
                    0,
                    this.MenuRect.Width + this.MaxRect.Width + this.MiniRect.Width + this.CloseRect.Width,
                    this.CloseRect.Height);
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= ClassStyles.CS_DROPSHADOW;
                return cp;
            }
        }
        protected override Padding DockPadding
        {
            get
            {
                return new Padding(0, 30, 0, 30);
            }
        }
        public virtual bool ShowMenu
        {
            get { return this._showMenu; }
            set
            {
                if (this._showMenu == value)
                    return;
                this._showMenu = value;
                this.Invalidate(this.TitleBarRect);
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 绘制窗体的系统控制按钮
        /// </summary>
        /// <param name="g">画板</param>
        /// <param name="rect">按钮所在的区域</param>
        /// <param name="image">图片</param>
        /// <param name="state">鼠标状态</param>
        private void DrawSysButton(Graphics g, Rectangle rect, Image image, EMouseState state)
        {
            Rectangle imageRect = Rectangle.Empty;
            switch (state)
            {
                case EMouseState.Normal:
                case EMouseState.MouseLeave:
                    imageRect = new Rectangle(0, 0, rect.Width, rect.Height);
                    break;
                case EMouseState.MouseMove:
                case EMouseState.MouseUp:
                    imageRect = new Rectangle(rect.Width, 0, rect.Width, rect.Height);
                    break;
                case EMouseState.MouseDown:
                    imageRect = new Rectangle(rect.Width * 2, 0, rect.Width, rect.Height);
                    break;
            }
            g.DrawImage(image, rect, imageRect, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// 绘制窗体边框
        /// </summary>
        /// <param name="g"></param>
        private void DrawFrameBorder(Graphics g)
        {
            Rectangle rect = this.ClientRectangle;
            rect.Width--;
            rect.Height--;
            g.DrawRectangle(Pens.LightSlateGray, rect);
            //int cut1 = 1;
            //int cut2 = 5;
            ////左上角
            //g.DrawImage(ImageBorder, new Rectangle(rect.X, rect.Y, cut2, cut2), 0, 0, cut2, cut2, GraphicsUnit.Pixel);
            ////上边
            //g.DrawImage(ImageBorder, new Rectangle(rect.X + cut2, rect.Y, rect.Width - cut2 * 2, cut1), cut2, 0, ImageBorder.Width - cut2 * 2, cut2, GraphicsUnit.Pixel);
            ////右上角
            //g.DrawImage(ImageBorder, new Rectangle(rect.X + rect.Width - cut2, rect.Y, cut2, cut2), ImageBorder.Width - cut2, 0, cut2, cut2, GraphicsUnit.Pixel);
            ////左边
            //g.DrawImage(ImageBorder, new Rectangle(rect.X, rect.Y + cut2, cut1, rect.Height - cut2 * 2), 0, cut2, cut1, ImageBorder.Height - cut2 * 2, GraphicsUnit.Pixel);
            ////左下角
            //g.DrawImage(ImageBorder, new Rectangle(rect.X, rect.Y + rect.Height - cut2, cut2, cut2), 0, ImageBorder.Height - cut2, cut2, cut2, GraphicsUnit.Pixel);
            ////右边
            //g.DrawImage(ImageBorder, new Rectangle(rect.X + rect.Width - cut1, rect.Y + cut2, cut1, rect.Height - cut2 * 2), ImageBorder.Width - cut1, cut2, cut1, ImageBorder.Height - cut2 * 2, GraphicsUnit.Pixel);
            ////右下角
            //g.DrawImage(ImageBorder, new Rectangle(rect.X + rect.Width - cut2, rect.Y + rect.Height - cut2, cut2, cut2), ImageBorder.Width - cut2, ImageBorder.Height - cut2, cut2, cut2, GraphicsUnit.Pixel);
            ////下边
            //g.DrawImage(ImageBorder, new Rectangle(rect.X + cut2, rect.Y + rect.Height - cut1, rect.Width - cut2 * 2, cut1), cut2, ImageBorder.Height - cut1, ImageBorder.Width - cut2 * 2, cut1, GraphicsUnit.Pixel);
        }
        #endregion

        #region Override Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = this.ClientRectangle;
            if (this.MaximizeBox && this.MinimizeBox)
            {
                this.DrawSysButton(g, this.CloseRect, ImageClose, base.CloseState);
                if (base.WindowState != FormWindowState.Maximized)
                    this.DrawSysButton(g, this.MaxRect, ImageMax, base.MaxState);
                else
                    this.DrawSysButton(g, this.MaxRect, ImageRestore, base.MaxState);
                this.DrawSysButton(g, this.MiniRect, ImageMin, base.MinState);
            }
            else if (!this.MaximizeBox && this.MinimizeBox)
            {
                this.DrawSysButton(g, this.CloseRect, ImageClose, base.CloseState);
                this.DrawSysButton(g, this.MiniRect, ImageMin, base.MinState);
            }
            else
            {
                this.DrawSysButton(g, this.CloseRect, ImageClose, base.CloseState);
            }
            if (this.ShowMenu)
            {
                // 绘制标题栏菜单按钮
                this.DrawSysButton(g, this.MenuRect, (Bitmap)ImageMenu, this._MenuState);
            }
            this.DrawFrameBorder(g);
            base.OnPaint(e);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //int rgn = NativeMethods.CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 6, 6);
            //NativeMethods.SetWindowRgn(this.Handle, rgn, true);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.ShowMenu)
            {
                if (this.MenuRect.Contains(e.Location) && this._MenuState != EMouseState.MouseDown)
                {
                    this.MenuState = EMouseState.MouseMove;
                }
                else
                {
                    this.MenuState = EMouseState.Normal;
                }
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.ShowMenu)
            {
                if (this.MenuRect.Contains(e.Location))
                {
                    this.MenuState = EMouseState.MouseDown;
                    if (this.ContextMenuStrip != null)
                    {
                        this.ContextMenuStrip.Show(this, this.MenuRect.X, this.MenuRect.Bottom);
                    }
                }
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            {
                if (this.MenuRect.Contains(e.Location))
                {
                    this.MenuState = EMouseState.MouseUp;
                }
            }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            {
                this.MenuState = EMouseState.MouseLeave;
            }
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)WindowsMessage.WM_CONTEXTMENU:
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
            //base.WndProc(ref m);
        }
        #endregion
    }
}
