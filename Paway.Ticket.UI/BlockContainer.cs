using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using Paway.Ticket.UI.Enums;

namespace Paway.Ticket.UI
{
    [ToolboxBitmap(typeof(Panel))]
    public class BlockContainer : Control
    {
        #region 常量
        private static readonly Size DEFAULT_SIZE = new Size(400, 52);
        #endregion

        #region 变量
        private bool _showBorder = true;
        private BlockCollection _blocks = null;
        private int _blockGap = 10;
        #endregion

        #region 构造函数

        public BlockContainer()
            : base()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.FixedHeight |
                ControlStyles.FixedWidth |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }

        #endregion

        #region 属性
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BlockCollection Blocks
        {
            get
            {
                if (this._blocks == null)
                    this._blocks = new BlockCollection(this);
                return this._blocks;
            }
        }
        public virtual bool ShowBorder
        {
            get { return this._showBorder; }
            set
            {
                if (this._showBorder == value)
                    return;
                this._showBorder = value;
                this.Invalidate();
            }
        }
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                this.Invalidate();
            }
        }
        protected override Size DefaultSize
        {
            get
            {
                return DEFAULT_SIZE;
            }
        }
        public override Size MinimumSize
        {
            get
            {
                return DEFAULT_SIZE;
            }
        }
        #endregion

        #region Override Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            if (this.Blocks.Count > 0)
            {
                SizeF blockSize = SizeF.Empty;
                int xPos = 0;
                for (int i = 0; i < this.Blocks.Count; i++)
                {
                    Block block = this.Blocks[i];
                    SizeF size = g.MeasureString(block.Text, this.Font);
                    block.Size = new Size((int)size.Width + 40, 32);
                    block.Location = new Point(xPos += this._blockGap, 10);
                    block.Font = this.Font;

                    block.Paint(g);

                    xPos += block.Width;
                }
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (this.ShowBorder)
            {
                Graphics g = e.Graphics;

                Rectangle rect = this.ClientRectangle;
                rect.Width--;
                rect.Height--;
                using (Brush brush = new SolidBrush(this.BackColor))
                {
                    g.FillRectangle(brush, rect);
                }
                g.DrawRectangle(Pens.Black, rect);
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!this.DesignMode && this.Blocks.Count > 0)
            {
                foreach (Block block in this.Blocks)
                {
                    if (block.Bounds.Contains(e.Location))
                    {
                        block.MouseState = EMouseState.MouseMove;
                        if (block.CloseBounds.Contains(e.Location))
                        {
                            block.CloseState = EMouseState.MouseMove;
                        }
                        else
                        {
                            block.CloseState = EMouseState.MouseLeave;
                        }
                        this.Invalidate(block.CloseBounds);
                    }
                    else
                    {
                        block.MouseState = EMouseState.MouseLeave;
                    }
                    this.Invalidate(block.Bounds);
                }
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!this.DesignMode && this.Blocks.Count > 0)
            {
                foreach (Block block in this.Blocks)
                {
                    if (block.Bounds.Contains(e.Location))
                    {
                        block.MouseState = EMouseState.MouseDown;
                        if (block.CloseBounds.Contains(e.Location))
                        {
                            block.CloseState = EMouseState.MouseDown;
                        }
                        else
                        {
                            block.CloseState = EMouseState.MouseLeave;
                        }
                        this.Invalidate(block.Bounds);
                    }
                    else
                    {
                        block.MouseState = EMouseState.MouseLeave;
                    }
                    this.Invalidate(block.Bounds);
                }
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (!this.DesignMode && this.Blocks.Count > 0)
            {
                foreach (Block block in this.Blocks)
                {
                    if (block.CloseBounds.Contains(e.Location) && block.CloseState == EMouseState.MouseDown)
                    {
                        block.CloseState = EMouseState.MouseUp;
                        this.Blocks.Remove(block);
                        this.Invalidate();
                        break;
                    }
                }
            }
        }
        #endregion
    }
}
