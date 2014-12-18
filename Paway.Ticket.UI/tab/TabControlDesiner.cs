using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using Paway.Ticket.Win32;

namespace Paway.Ticket.UI
{
    public class TabControlDesiner : ParentControlDesigner
    {
        #region 变量
        IComponentChangeService _changeService;
        #endregion

        #region 属性
        public override System.Collections.ICollection AssociatedComponents
        {
            get { return this.Control.TabPages; }
        }

        public new virtual vTabControl Control
        {
            get { return base.Control as vTabControl; }
        }
        #endregion

        #region Override Methods
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);
            this._changeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
            this._changeService.ComponentRemoving += new ComponentEventHandler(OnRemoving);
            this.Verbs.Add(new DesignerVerb("Add TabControl", new EventHandler(OnAddTabControl)));
            this.Verbs.Add(new DesignerVerb("Remove TabControl", new EventHandler(OnRemoveTabControl)));
        }

        protected override void Dispose(bool disposing)
        {
            this._changeService.ComponentRemoving -= new ComponentEventHandler(OnRemoving);
            base.Dispose(disposing);
        }
        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            base.PreFilterProperties(properties);

            properties.Remove("DockPadding");
            properties.Remove("DrawGrid");
            properties.Remove("Margin");
            properties.Remove("Padding");
            properties.Remove("BorderStyle");
            properties.Remove("ForeColor");
            properties.Remove("BackColor");
            properties.Remove("BackgroundImage");
            properties.Remove("BackgroundImageLayout");
            properties.Remove("GridSize");
            properties.Remove("ImeMode");
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == (int)WindowsMessage.WM_LBUTTONDOWN)
            {
                Point point = this.Control.PointToClient(Cursor.Position);
                vTabPage page = this.Control.GetTabPageByPoint(point);
                if (page != null)
                {
                    this.Control.SelectedPage = page;
                    ArrayList selection = new ArrayList();
                    selection.Add(page);
                    ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
                    selectionService.SetSelectedComponents(selection);
                }
            }
            base.WndProc(ref m);
        }
        #endregion

        #region 事件
        private void OnRemoving(object sender, ComponentEventArgs e)
        {
            IDesignerHost host = (IDesignerHost)this.GetService(typeof(IDesignerHost));

            if (e.Component is vTabPage)
            {
                vTabPage page = e.Component as vTabPage;
                if (this.Control.TabPages.Contains(page))
                {
                    this._changeService.OnComponentChanging(this.Control, null);
                    this.Control.RemoveTab(page);
                    this._changeService.OnComponentChanged(this.Control, null, null, null);
                    return;
                }
            }
            if (e.Component is vTabControl)
            {
                for (int i = this.Control.TabPages.Count - 1; i >= 0; i--)
                {
                    vTabPage page = this.Control.TabPages[i];
                    this._changeService.OnComponentChanging(this.Control, null);
                    this.Control.RemoveTab(page);
                    host.DestroyComponent(page);
                    this._changeService.OnComponentChanged(this.Control, null, null, null);
                }
            }
        }

        private void OnAddTabControl(object sender, EventArgs e)
        {
            IDesignerHost host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
            DesignerTransaction transaction = host.CreateTransaction("Add TabControl");
            vTabPage page = (vTabPage)host.CreateComponent(typeof(vTabPage));
            this._changeService.OnComponentChanging(this.Control, null);
            this.Control.AddTab(page);
            int index = this.Control.TabPages.IndexOf(page) + 1;
            page.Title = "TabPage" + index;
            this.Control.SelectPage(page);
            this._changeService.OnComponentChanged(this.Control, null, null, null);
            transaction.Commit();
        }

        private void OnRemoveTabControl(object sender, EventArgs e)
        {
            IDesignerHost host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
            DesignerTransaction transaction = host.CreateTransaction("Remove TabControl");
            this._changeService.OnComponentChanging(this.Control, null);
            vTabPage page = this.Control.TabPages[this.Control.TabPages.Count - 1];
            this.Control.UnSelectPage(page);
            this.Control.TabPages.Remove(page);
            this._changeService.OnComponentChanged(this.Control, null, null, null);
            transaction.Commit();
        }
        #endregion
    }
}
