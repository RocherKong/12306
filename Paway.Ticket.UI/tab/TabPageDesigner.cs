using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Drawing;

namespace Paway.Ticket.UI
{
    public class TabPageDesigner : ParentControlDesigner
    {
        #region 变量
        vTabPage _tabPage;
        #endregion

        #region Override Methods
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);
            this._tabPage = component as vTabPage;
        }

        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            base.PreFilterProperties(properties);

            properties.Remove("Dock");
            properties.Remove("AutoScroll");
            properties.Remove("AutoScrollMargin");
            properties.Remove("AutoScrollMinSize");
            properties.Remove("DockPadding");
            properties.Remove("DrawGrid");
            properties.Remove("Font");
            properties.Remove("Padding");
            properties.Remove("MinimumSize");
            properties.Remove("MaximumSize");
            properties.Remove("Margin");
            properties.Remove("ForeColor");
            properties.Remove("BackColor");
            properties.Remove("BackgroundImage");
            properties.Remove("BackgroundImageLayout");
            properties.Remove("RightToLeft");
            properties.Remove("GridSize");
            properties.Remove("ImeMode");
            properties.Remove("BorderStyle");
            properties.Remove("AutoSize");
            properties.Remove("AutoSizeMode");
            properties.Remove("Location");
        }

        public override bool CanBeParentedTo(System.ComponentModel.Design.IDesigner parentDesigner)
        {
            return (parentDesigner.Component is vTabControl);
        }

        protected override void OnPaintAdornments(System.Windows.Forms.PaintEventArgs pe)
        {
            if (this._tabPage != null)
            {
                using (Pen pen = new Pen(SystemColors.ControlDark))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    pe.Graphics.DrawRectangle(pen, 0, 0, this._tabPage.Width - 1, this._tabPage.Height - 1);
                }
            }
        }
        #endregion

        #region 属性
        public override SelectionRules SelectionRules
        {
            get { return 0; }
        }

        #endregion
    }
}
