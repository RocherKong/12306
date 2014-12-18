using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Paway.Ticket.UI
{
    public class PictureBoxExt : PictureBox
    {
        #region 构造函数

        public PictureBoxExt()
            : base()
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        #endregion

        #region 属性
        new public Image Image
        {
            get { return base.Image; }
            set
            {
                if (base.Image != null)
                {
                    base.Image.Dispose();
                    base.Image = null;
                }
                base.Image = value;
            }
        }
        protected override Size DefaultSize
        {
            get
            {
                return new Size(78, 28);
            }
        }
        #endregion

        #region Override Methods
        protected override void OnMouseLeave(EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        #endregion
    }
}
