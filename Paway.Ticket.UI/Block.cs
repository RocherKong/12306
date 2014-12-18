using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Paway.Ticket.UI.Enums;

namespace Paway.Ticket.UI
{
    public class Block
    {
        #region 常量
        private static readonly Image ImageCloseNormal = AppResource.GetImage("Images.block.close_normal.png");
        private static readonly Image ImageCloseHover = AppResource.GetImage("Images.block.close_hover.png");
        private static readonly Color BackColorNormal = Color.FromArgb(254, 244, 228);
        private static readonly Color BackColorHover = Color.FromArgb(254, 233, 199);
        private static readonly Color BorderColorNormal = Color.FromArgb(223, 206, 159);
        private static readonly Color BorderColorHover = Color.FromArgb(246, 186, 121);
        #endregion

        #region 变量

        private string _text = "block";
        private object _tag = null;
        private Size _size = Size.Empty;
        private Point _location = Point.Empty;
        private Font _font = new Font("宋体", 9.0f);
        private Color _foreColor = Color.Black;
        private EMouseState _closeState = EMouseState.Normal;
        private EMouseState _mouseState = EMouseState.Normal;
        #endregion

        #region 构造函数
        public Block() { }
        public Block(string text)
            : this()
        {
            this._text = text;
        }
        public Block(string text, object tag)
            : this(text)
        {
            this._tag = tag;
        }
        #endregion

        #region 属性

        internal int Height
        {
            get { return this.Size.Height; }
        }
        internal int Width
        {
            get { return this.Size.Width; }
        }
        internal Size Size
        {
            get { return this._size; }
            set { this._size = value; }
        }
        internal Point Location
        {
            get { return this._location; }
            set { this._location = value; }
        }
        internal Rectangle Bounds
        {
            get { return new Rectangle(this.Location, this.Size); }

        }
        internal Font Font
        {
            get { return this._font; }
            set { this._font = value; }
        }
        internal Color ForeColor
        {
            get { return this._foreColor; }
            set { this._foreColor = value; }
        }
        internal Rectangle TextBounds
        {
            get
            {
                Rectangle rect = this.Bounds;
                rect.X += 16;
                rect.Width -= 16;
                return rect;
            }
        }
        internal Rectangle CloseBounds
        {
            get
            {
                Rectangle rect = this.Bounds;
                rect.X = this.Bounds.Right - 16;
                rect.Y = 22;
                rect.Width = 8;
                rect.Height = 7;
                return rect;
            }
        }
        internal EMouseState MouseState
        {
            get { return this._mouseState; }
            set { this._mouseState = value; }
        }
        internal EMouseState CloseState
        {
            get { return this._closeState; }
            set { this._closeState = value; }
        }
        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }
        public object Tag
        {
            get { return this._tag; }
            set { this._tag = value; }
        }

        #endregion

        #region 方法
        internal void Paint(Graphics g)
        {
            Color backColor = BackColorNormal;
            Color borderColor = BorderColorNormal;
            Image closeImage = ImageCloseNormal;
            if (this._mouseState != EMouseState.Normal && this._mouseState != EMouseState.MouseLeave)
            {
                backColor = BackColorHover;
                borderColor = BorderColorHover;
            }
            // 填充背景色
            using (Brush brush = new SolidBrush(backColor))
            {
                g.FillRectangle(brush, this.Bounds);
            }
            // 绘制边框
            using (Pen pen = new Pen(borderColor))
            {
                Rectangle rect = this.Bounds;
                rect.Width--;
                rect.Height--;
                g.DrawRectangle(pen, rect);
            }
            // 绘制文字
            TextRenderer.DrawText(g, this.Text, this.Font, this.TextBounds, this.ForeColor, TextFormatFlags.VerticalCenter);
            // 关闭按钮
            if (this._closeState != EMouseState.Normal && this._closeState != EMouseState.MouseLeave)
                closeImage = ImageCloseHover;
            g.DrawImage(closeImage, this.CloseBounds, new Rectangle(Point.Empty, closeImage.Size), GraphicsUnit.Pixel);
        }
        #endregion
    }
}
