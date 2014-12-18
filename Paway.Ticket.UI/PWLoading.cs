using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Paway.Ticket.Win32;

namespace Paway.Ticket.UI
{
    [ToolboxItem(false)]
    public class PWLoading : Form
    {
        #region 变量
        private static PWLoading Loading = null;
        private static int _res_index = 1;
        private const string RES_PATH = "Images.Loading.{0}.png";

        private System.Windows.Forms.Timer _timer = null;
        private Image _image = null;
        #endregion

        #region 构造函数

        private PWLoading()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.FixedHeight |
                ControlStyles.FixedWidth |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }

        #endregion

        #region 属性
        protected override Size DefaultSize
        {
            get { return new Size(300, 180); }
        }
        new public virtual string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                this.Invalidate(this.TextBounds);
            }
        }
        protected virtual Rectangle ImageBounds
        {
            get { return new Rectangle(20, (this.Height - 28) / 2, 28, 28); }
        }
        protected virtual Rectangle TextBounds
        {
            get { return new Rectangle(60, 20, this.Width - 80, this.Height - 40); }
        }

        public Image Image
        {
            get { return this._image; }
            set
            {
                if (this._image != null)
                {
                    this._image.Dispose();
                    this._image = null;
                }
                this._image = value;
                this.Invalidate(this.ImageBounds);
            }
        }
        #endregion

        #region 方法
        private static void Init(IWin32Window owner, string text)
        {
            if (Loading == null)
            {
                Loading = new PWLoading();
                Loading.FormBorderStyle = FormBorderStyle.None;
                Loading.ShowIcon = false;
                Loading.ShowInTaskbar = false;
            }
            if (Loading._timer == null)
                Loading._timer = new System.Windows.Forms.Timer();
            Loading._timer.Interval = 100;
            Loading._timer.Tick -= new EventHandler(_timer_Tick);
            Loading._timer.Tick += new EventHandler(_timer_Tick);
            Loading._timer.Start();
            Loading.Text = text;
            SizeF size = TextRenderer.MeasureText(text, Loading.Font);
            Loading.Size = new Size((int)size.Width + 80, (int)size.Height + 50);
            if (owner != null)
            {
                Loading.StartPosition = FormStartPosition.CenterParent;
            }
        }
        public static void Show(IWin32Window owner, string text, Action process)
        {
            try
            {
                Init(owner, text);
                ThreadPool.QueueUserWorkItem((obj) =>
                {
                    process();
                    if (Loading.InvokeRequired)
                    {
                        Loading.Invoke(new Action(() =>
                        {
                            Release();
                        }));
                    }
                    else
                    {
                        Release();
                    }
                });
                Loading.ShowDialog();
            }
            catch (Exception ex)
            {
                if (Loading != null)
                {
                    if (Loading.InvokeRequired)
                    {
                        Loading.Invoke(new Action(() => { throw ex; }));
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }
        }
        public static void UpdateMessage(string message)
        {
            if (Loading != null)
            {
                if (Loading.InvokeRequired)
                {
                    Loading.Invoke(new Action(() => 
                    {
                        if (Loading.Text != message)
                        {
                            Loading.Text = message;
                        }
                    }));
                }
                else
                {
                    Loading.Text = message;
                }
            }
        }
        public static void Release()
        {
            if (Loading != null)
            {
                if (Loading._timer != null)
                {
                    if (Loading._timer.Enabled)
                        Loading._timer.Stop();
                    //Loading._timer.Dispose();
                    //Loading._timer = null;
                }
                Loading.DialogResult = DialogResult.OK;
                //Loading.Dispose();
                //Loading = null;
            }
        }
        #endregion

        #region 事件
        static void _timer_Tick(object sender, EventArgs e)
        {
            if (Loading != null)
            {
                Image image = AppResource.GetImage(string.Format(RES_PATH, _res_index));
                if (_res_index == 8)
                    _res_index = 1;
                else
                    _res_index++;
                Loading.Image = image;
            }
        }
        #endregion

        #region Override Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            if (this.Image != null)
            {
                g.DrawImage(this.Image, this.ImageBounds, new Rectangle(Point.Empty, this.Image.Size), GraphicsUnit.Pixel);
            }
            if (this.Text.Trim() != string.Empty)
            {
                TextRenderer.DrawText(g, this.Text.Trim(), this.Font, this.TextBounds, this.ForeColor, TextFormatFlags.VerticalCenter);
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Rectangle bounds = this.ClientRectangle;
            bounds.Width--;
            bounds.Height--;
            e.Graphics.DrawRectangle(Pens.Black, bounds);
        }
        #endregion
    }
}
