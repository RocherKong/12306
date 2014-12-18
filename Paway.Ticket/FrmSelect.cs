using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Paway.Ticket.UI;
using Paway.Ticket.Package.Data;

namespace Paway.Ticket
{
    /// <summary>
    /// 选择 乘客、备选日期、席别、车次
    /// </summary>
    public partial class FrmSelect : _360Form
    {
        #region 变量
        private BlockContainer _blockContainer = null;
        #endregion

        #region 构造函数

        public FrmSelect(BlockContainer container)
        {
            InitializeComponent();
            this._blockContainer = container;
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            int xPos = 5, yPos = 8;
            int index = 0;
            this.panel1.SuspendLayout();
            PassengerDetail[] array = AppContext.LoginUser.Passengers;
            foreach (PassengerDetail item in array)
            {
                LinkLabel lblHotCity = new LinkLabel();
                lblHotCity.Text = item.passenger_name;
                lblHotCity.Tag = item;
                lblHotCity.Visible = true;
                lblHotCity.AutoSize = true;
                lblHotCity.TabStop = true;
                lblHotCity.LinkBehavior = LinkBehavior.HoverUnderline;
                lblHotCity.BorderStyle = BorderStyle.None;
                //lblHotCity.Size = new System.Drawing.Size(70, 25);
                lblHotCity.LinkBehavior = LinkBehavior.NeverUnderline;
                lblHotCity.Location = new Point(xPos, yPos);
                index++;
                if (index % 5 == 0)
                {
                    xPos = 5;
                    yPos += 25;
                }
                else
                {
                    xPos += 70;
                }
                //lblHotCity.Click -= new EventHandler(lblHotCity_Click);
                //lblHotCity.Click += new EventHandler(lblHotCity_Click);
                this.panel1.Controls.Add(lblHotCity);
            }
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
        }
    }
}
