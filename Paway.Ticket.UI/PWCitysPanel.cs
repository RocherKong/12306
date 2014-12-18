using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Paway.Ticket.Entity;
using System.IO;
using Paway.Ticket.Win32;

namespace Paway.Ticket.UI
{
    public partial class PWCitysPanel : Form
    {
        private Control _owner = null;
        private static Dictionary<string, string[]> hotCitys = null;
        private static PWCitysPanel Self = new PWCitysPanel();

        public PWCitysPanel()
        {
            InitializeComponent();
            //citysPanel.ShowInTaskbar = false;
            this.LoadEvents();
            // 初始化热门城市
            int xPos = 5, yPos = 8;
            int index = 0;
            this.pnlMain.SuspendLayout();

            foreach (string item in HotCitys.Keys)
            {
                LinkLabel lblHotCity = new LinkLabel();
                string[] obj = HotCitys[item];
                lblHotCity.Text = obj[1];
                lblHotCity.Tag = obj;
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
                lblHotCity.Click -= new EventHandler(lblHotCity_Click);
                lblHotCity.Click += new EventHandler(lblHotCity_Click);
                this.pnlMain.Controls.Add(lblHotCity);
            }
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
        }

        private void LoadEvents()
        {
            this.pnlMain.Paint += new PaintEventHandler(pnlBackground_Paint);
            this.pnlTop.Paint += new PaintEventHandler(pnlBackground_Paint);
        }

        void pnlBackground_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            Rectangle bounds = panel.ClientRectangle;
            if (panel.Name == "pnlTop")
            {
                bounds.Width -= 1;
            }
            else
            {
                bounds.Width -= 1;
                bounds.Height -= 1;
            }
            Graphics g = e.Graphics;
            using (Pen pen = new Pen(Color.FromArgb(43, 140, 206)))
            {
                g.DrawRectangle(pen, bounds);
            }
        }

        void lblHotCity_Click(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                Label label = sender as Label;
                if (label.Tag != null && label.Tag is string[])
                {
                    string[] array = label.Tag as string[];
                    if (this._owner != null && this._owner is CityTextBox)
                    {
                        CityTextBox textBox = this._owner as CityTextBox;
                        textBox.SetText(array[1]);
                        textBox.Tag = array[2];
                    }
                    PWCitysPanel.HideSelf();
                }
            }
        }

        public static Dictionary<string, string[]> HotCitys
        {
            get
            {
                string text = null;
                using (Stream stream = AppResource.GetObject("jsons.hotCitys.txt"))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        text = sr.ReadToEnd();
                    }
                }
                if (!string.IsNullOrEmpty(text))
                {
                    hotCitys = new Dictionary<string, string[]>();
                    string[] array = text.Split('@');
                    foreach (string item in array)
                    {
                        if (item != string.Empty)
                        {
                            string[] cArray = item.Split('|');
                            string key = cArray[0];
                            if (!hotCitys.ContainsKey(key))
                            {
                                hotCitys.Add(cArray[0], new string[] { cArray[0], cArray[1], cArray[2], cArray[3] });
                            }
                        }
                    }
                }
                return hotCitys;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static string Show(Control owner)
        {
            if (owner == null)
                throw new Exception("owner is null!");
            Self._owner = owner;
            int xPos = owner.RectangleToScreen(Rectangle.Empty).X;
            int yPos = owner.RectangleToScreen(Rectangle.Empty).Y + Self._owner.Height + 5;
            Self.TopMost = true;
            if (!Self.Visible)
                Self.Show();
            Self.Location = new Point(xPos, yPos);
            owner.Select();
            return string.Empty;
        }

        public static void HideSelf()
        {
            if (Self != null && Self.Visible)
            {
                Self.Hide();
            }
        }

        private void CitysPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (this._owner != null)
            {
                Form form = this._owner.FindForm();
                if (form != null)
                {
                    form.Activate();
                }
            }
        }

        public static bool IsShow
        {
            get
            {
                return (Self != null && Self.Visible);
            }
        }
        public static Rectangle Bound
        {
            get
            {
                return Self == null ? Rectangle.Empty : Self.Bounds;
            }
        }
    }
}
