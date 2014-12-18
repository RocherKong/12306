using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Paway.Ticket
{
    /// <summary>
    /// 关于我们
    /// </summary>
    public partial class FrmAbout : _360Form
    {
        #region 构造函数

        public FrmAbout()
        {
            InitializeComponent();
        }

        #endregion

        #region 属性
        protected override Padding DockPadding
        {
            get
            {
                return new Padding(1, 30, 1, 30);
            }
        }
        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.pictureBox1.Image = AppResource.GetImage("Images.logo.logo.png");
        }
        #endregion
    }
}
