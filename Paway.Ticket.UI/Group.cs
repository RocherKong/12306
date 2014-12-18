using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Paway.Ticket.UI
{
    public class Group : Panel
    {
        private string _text = "标题";
        public Group()
            : base()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.ContainerControl | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();
        }

        protected override Padding DefaultPadding
        {
            get
            {
                return new Padding(1, 30, 1, 1);
            }
        }
        [Browsable(true)]
        new public string Text
        {
            get { return this._text; }
            set
            {
                this._text = value;
                this.Invalidate(new Rectangle(1, 0, this.Width - 2, 30));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(Brushes.Gray, new Rectangle(1, 0, this.Width - 2, 30));
            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, new Rectangle(1, 0, this.Width - 2, 30), this.ForeColor, TextFormatFlags.VerticalCenter);
            Rectangle rect = this.ClientRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle(Pens.Gray, rect);
        }
    }
}
