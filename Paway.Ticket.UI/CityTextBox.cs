using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Paway.Ticket.Win32;
using Paway.Ticket.UI.Enums;

namespace Paway.Ticket.UI
{
    [ToolboxBitmap(typeof(TextBox))]
    public class CityTextBox : Control, ISupportInitialize
    {
        #region 常量
        private static readonly Image ImageLocation = AppResource.GetImage("Images.location.png");
        private static readonly Size DEFAULT_SIZE = new Size(150, 30);
        #endregion

        #region 变量
        private TextBoxBase BaseText;
        private Cursor _cursor = Cursors.IBeam;
        private EMouseState _mouseIcon = EMouseState.Normal;
        private Timer _timer = new Timer();
        private bool _beamState = false;
        private int _beamPos = -1;
        private int _startPosition = 3;
        private string _text = string.Empty;
        private HookMouse _hookMouse = null;
        private bool _isInit = false;
        #endregion

        #region 构造函数

        public CityTextBox()
        {
            this.BeginInit();
            this.SetStyle(ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.Selectable |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.ResizeRedraw, true);
            this.BackColor = Color.Transparent;
            this._text = this.Name;
            this.UpdateStyles();
            if (!this.DesignMode)
            {
                this._timer.Tick -= new EventHandler(_timer_Tick);
                this._timer.Tick += new EventHandler(_timer_Tick);
                this._timer.Interval = 600;
                this._timer.Start();
            }
            this.EndInit();
        }
        #endregion

        #region 属性
        private Rectangle IconRect
        {
            get
            {
                Rectangle rect = new Rectangle();
                rect.X = this.Width - 26;
                rect.Y = this.Height - ImageLocation.Height <= 0 ? 2 : (this.Height - ImageLocation.Height) / 2;
                rect.Width = ImageLocation.Width;
                rect.Height = ImageLocation.Height;
                return rect;
            }
        }
        private Rectangle TextRect
        {
            get
            {
                Rectangle rect = new Rectangle(
                    1,
                    1,
                    this.Width - this.IconRect.Width - 2,
                    this.Height - 2);
                return rect;
            }
        }
        private int BeamPos
        {
            get { return this._beamPos; }
            set
            {
                if (this._beamPos == value)
                    return;
                if (this._beamPos < -1)
                    this._beamPos = -1;
                this._beamPos = value;
            }
        }
        protected virtual EMouseState MouseIcon
        {
            get { return this._mouseIcon; }
            set
            {
                if (this._mouseIcon == value)
                    return;
                this._mouseIcon = value;
                this.Invalidate(this.IconRect);
            }
        }
        protected override Size DefaultSize
        {
            get { return DEFAULT_SIZE; }
        }
        public override Size MinimumSize
        {
            get { return DEFAULT_SIZE; }
        }
        public override string Text
        {
            get { return this._text; }
            set
            {
                if (this._text != value)
                {
                    this._text = value;
                    this.BeamPos = this._text.Length - 1;
                    this.Invalidate(this.TextRect);
                    this.OnTextChanged(EventArgs.Empty);
                }
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 此方法不会触发 TextChanged 事件
        /// </summary>
        /// <param name="text"></param>
        internal void SetText(string text)
        {
            this._text = text;
            this.BeamPos = text.Length - 1;
            this.Invalidate(this.TextRect);
        }
        private void AppendText(char c)
        {
            StringBuilder sb = new StringBuilder(this.Text);
            sb.Append(c);
            this.BeamPos += 1;
            this.Text = sb.ToString();
        }
        private void RemoveText(int startPos, int length)
        {
            if (this.Text != string.Empty && startPos > -1 && length > 0)
            {
                StringBuilder sb = new StringBuilder(this.Text);
                sb.Remove(startPos, length);
                this.BeamPos -= length;
                this.Text = sb.ToString();
            }
        }
        private void InstallHook()
        {
            if (this._hookMouse == null)
                this._hookMouse = new HookMouse();
            this._hookMouse.OnMouseActivity -= new MouseEventHandler(_hookMouse_OnMouseActivity);
            this._hookMouse.OnMouseActivity += new MouseEventHandler(_hookMouse_OnMouseActivity);
            this._hookMouse.InstallHook();
        }
        private void UnInstallHook()
        {
            if (this._hookMouse != null)
            {
                this._hookMouse.OnMouseActivity -= new MouseEventHandler(_hookMouse_OnMouseActivity);
                this._hookMouse.UnInstallHook();
                this._hookMouse = null;
            }
        }
        #endregion

        #region 事件
        void _timer_Tick(object sender, EventArgs e)
        {
            if (this.Focused)
            {
                this._beamState = !this._beamState;
                this.Invalidate(this.TextRect);
            }
        }
        void _hookMouse_OnMouseActivity(object sender, MouseEventArgs e)
        {
            if (e.Clicks > 0)
            {
                if (ChooseList.IsShow && !ChooseList.Bound.Contains(e.Location))
                {
                    this.FindForm().Focus();
                    ChooseList.HideSelf();
                    this.UnInstallHook();
                }
                else if (PWCitysPanel.IsShow && !PWCitysPanel.Bound.Contains(e.Location))
                {
                    this.FindForm().Focus();
                    PWCitysPanel.HideSelf();
                    this.UnInstallHook();
                }
            }
        }
        #endregion

        #region Override Methods
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.BeamPos = this.Text.Length - 1;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectangle bounds = this.ClientRectangle;
            bounds.Width--;
            bounds.Height--;
            g.FillRectangle(Brushes.White, bounds);
            g.DrawRectangle(Pens.Gray, bounds);
            if (ImageLocation != null)
            {
                Rectangle iconRect = this.IconRect;
                if (this.MouseIcon == EMouseState.MouseDown)
                {
                    iconRect.X += 1;
                    iconRect.Y += 1;
                }
                g.DrawImage(ImageLocation, iconRect, new Rectangle(Point.Empty, ImageLocation.Size), GraphicsUnit.Pixel);
            }
            SizeF size = g.MeasureString(this.Text, this.Font);
            int xPos = (int)(this._startPosition + size.Width);
            if (this.Text != string.Empty)
            {
                TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                TextRenderer.DrawText(g, this.Text, this.Font, this.TextRect, this.ForeColor, flags);
            }
            if (!this.DesignMode)
            {
                if (this._beamState && this.Focused)
                {
                    using (Pen pen = new Pen(Color.Black, 1.5f))
                    {
                        xPos = xPos > 6 ? xPos - 4 : 2;
                        g.DrawLine(pen, xPos, 4, xPos, this.Height - 6);
                    }
                }
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!this.DesignMode)
            {
                this.MouseIcon = EMouseState.MouseMove;
                if (this.IconRect.Contains(e.Location))
                {
                    this.Cursor = Cursors.Hand;
                }
                else
                {
                    this.Cursor = Cursors.IBeam;
                }
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!this.DesignMode)
            {
                if (e.Button == MouseButtons.Left && this.IconRect.Contains(e.Location))
                {
                    this.MouseIcon = EMouseState.MouseDown;
                }
                this.Focus();
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            try
            {
                if (!this.DesignMode)
                {
                    this.MouseIcon = EMouseState.MouseUp;
                    if (e.Button == MouseButtons.Left && this.IconRect.Contains(e.Location))
                    {
                        ChooseList.HideSelf();
                        PWCitysPanel.Show(this);
                        this.InstallHook();
                    }
                }
            }
            catch { }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!this.DesignMode)
            {
                this.MouseIcon = EMouseState.MouseLeave;
                this.Cursor = Cursors.Default;
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (this._timer != null)
            {
                if (this._timer.Enabled)
                    this._timer.Stop();
                this._timer.Dispose();
                this._timer = null;
            }
            this.UnInstallHook();
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (!this.DesignMode)
            {
                int charNum = (int)e.KeyChar;
                if (charNum == 8)
                {
                    e.Handled = false;
                    this.RemoveText(this.BeamPos, 1);
                    this.Tag = null;
                    return;
                }
                if (Encoding.Default.GetBytes(this.Text).Length == 15)
                {
                    e.Handled = true;
                    return;
                }
                if (charNum < 65 || charNum > 122)
                {
                    e.Handled = true;
                    return;
                }
                if (charNum > 90 && charNum < 97)
                {
                    e.Handled = true;
                    return;
                }
                e.Handled = false;
                this.AppendText(e.KeyChar);
            }
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (!this.DesignMode && this._isInit)
            {
                this._timer.Stop();
                this.Invalidate();
            }
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (!this.DesignMode && this._isInit)
            {
                this._timer.Start();
                this._beamState = true;
                this.Invalidate();
            }
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            try
            {
                if (!this.DesignMode && this._isInit)
                {
                    ChooseList.Show(this, this.Text.Trim());
                    this.InstallHook();
                }
            }
            catch { }
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
