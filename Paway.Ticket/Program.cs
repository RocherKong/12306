using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using Paway.Ticket.UI;

namespace Paway.Ticket
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //FrmLogin frmLogin = new FrmLogin();
            //if (frmLogin.ShowDialog() == DialogResult.OK)
            //{
            //    Application.Run(new FrmMain());
            //}


            //Application.Run(new FrmConfirmOrder());


            //Application.Run(new FrmLogin());


            //Application.Run(new FrmConfirmOrder());

            Application.Run(new FrmMain());


            //Application.Run(new CitysPanel());

            //UI.ChooseList list = new UI.ChooseList();
            //list.Items.Add(new ChooseListItem() { Text = "北京北" });
            //list.Items.Add(new ChooseListItem() { Text = "北京东" });
            //list.Items.Add(new ChooseListItem() { Text = "北京" });
            //list.Items.Add(new ChooseListItem() { Text = "北京南" });
            //list.Items.Add(new ChooseListItem() { Text = "北京西" });
            //list.Items.Add(new ChooseListItem() { Text = "北安" });
            //Application.Run(list);
        }
    }
}
