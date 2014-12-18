using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using Newtonsoft.Json;
using Paway.Ticket.Package;
using Paway.Ticket.Http;
using Paway.Ticket.UI;
using Paway.Ticket.Package.Data;
using Paway.Ticket.Entity;

namespace Paway.Ticket
{
    /// <summary>
    /// 主窗口
    /// </summary>
    public partial class FrmMain : _360Form
    {
        #region 变量

        #region 定时刷票

        private static Thread AutoThread = null;
        private static bool ThreadStatus = false;
        private static readonly object TIMER_LOCK = new object();
        private static ulong AutoCount = 0;

        #endregion

        #endregion

        #region 构造函数

        public FrmMain()
        {
            InitializeComponent();
            this.LoadEvents();
        }

        #endregion

        #region 方法

        #region 全局
        /// <summary>
        /// 加载事件
        /// </summary>
        private void LoadEvents()
        {
            // 全局
            this.tsmiAbout.Click += new EventHandler(tsmiAbout_Click);
            // 个人中心
            this.btnLogin.Click += new EventHandler(btnLogin_Click);
            this.pnlLeft.Paint += new PaintEventHandler(pnlLeft_Paint);
            // 常规购票
            this.btnQuery.Click += new EventHandler(btnQuery_Click);
            this.dgvQuery.CellContentClick += new DataGridViewCellEventHandler(dgvQuery_CellContentClick);
            // 定时刷票
            this.pnlTimerTicket.Paint += new PaintEventHandler(pnlTimerTicket_Paint);
            
            this.btnStart.Click += new EventHandler(btnStart_Click);
            this.btnPassenger.Click += new EventHandler(btnPassenger_Click);

            this.chkSetTime.CheckedChanged += new EventHandler(chkSetTime_CheckedChanged);
            this.chkAuto.CheckedChanged += new EventHandler(chkAuto_CheckedChanged);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        private DialogResult Login()
        {
            DialogResult result = DialogResult.Cancel;
            using (FrmLogin frmLogin = new FrmLogin())
            {
                if ((result = frmLogin.ShowDialog()) == DialogResult.OK)
                {
                    if (AppContext.LoginUser != null)
                    {
                        this.btnLogin.Visible = false;
                        this.lblUserName.Text = AppContext.LoginUser.UserName;
                        this.lblUserName.Tag = AppContext.LoginUser;
                        this.lblUserName.Visible = true;
                    }
                }
            }
            return result;
        }
        #endregion

        #region 常规购票
        /// <summary>
        /// 重置查询界面
        /// </summary>
        private void ResetQuery()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    this.btnQuery.Enabled = true;
                }));
            }
            else
            {
                this.btnQuery.Enabled = true;
            }
        }
        /// <summary>
        /// 验证查询条件
        /// </summary>
        /// <returns></returns>
        private bool ValidateQuery()
        {
            bool result = true;
            if (this.txtFromStation.Tag == null)
            {
                result = false;
                AppContext.Tip.Show(
                    "请选择出发地。",
                    this.txtFromStation,
                    this.txtFromStation.Location.X,
                    this.txtFromStation.Location.Y - 20,
                    2000);
            }
            else if (this.txtToStation.Tag == null)
            {
                result = false;
                AppContext.Tip.Show(
                    "请选择目的地。",
                    this.txtToStation,
                    this.txtToStation.Location.X - 300,
                    this.txtToStation.Location.Y - 20,
                    2000);
            }
            return result;
        }
        /// <summary>
        /// 加载查询数据
        /// </summary>
        /// <param name="dataArray"></param>
        private void LoadQueryData(QueryTrainData[] dataArray)
        {
            this.dgvQuery.Rows.Clear();
            if(dataArray != null && dataArray.Length > 0)
            {
                foreach (QueryTrainData item in dataArray)
                {
                    if (item.QueryLeftNewDTO != null)
                    {
                        int index = this.dgvQuery.Rows.Add(
                            item.QueryLeftNewDTO.station_train_code,
                            item.QueryLeftNewDTO.from_station_name + "-" + item.QueryLeftNewDTO.to_station_name,
                            item.QueryLeftNewDTO.start_time + "-" + item.QueryLeftNewDTO.arrive_time,
                            item.QueryLeftNewDTO.lishi,
                            item.QueryLeftNewDTO.swz_num,
                            item.QueryLeftNewDTO.tz_num,
                            item.QueryLeftNewDTO.zy_num,
                            item.QueryLeftNewDTO.ze_num,
                            item.QueryLeftNewDTO.gr_num,
                            item.QueryLeftNewDTO.rw_num,
                            item.QueryLeftNewDTO.yw_num,
                            item.QueryLeftNewDTO.rz_num,
                            item.QueryLeftNewDTO.yz_num,
                            item.QueryLeftNewDTO.wz_num,
                            item.QueryLeftNewDTO.qt_num,
                            item.buttonTextInfo);
                        DataGridViewRow row = this.dgvQuery.Rows[index];
                        for (int i = 4; i < row.Cells.Count; i++)
                        {
                            DataGridViewCell cell = row.Cells[i];
                            if (cell.Value.ToString() != "*" && cell.Value.ToString() != "--" && cell.Value.ToString() != "无")
                            {
                                cell.Style.ForeColor = Color.Green;
                            }
                        }
                        if (item.QueryLeftNewDTO.canWebBuy == "Y")
                        {
                            DataGridViewDisableButtonCell cell = row.Cells[15] as DataGridViewDisableButtonCell;
                            cell.Enabled = true;
                        }
                        else
                        {
                            DataGridViewDisableButtonCell cell = row.Cells[15] as DataGridViewDisableButtonCell;
                            cell.Enabled = false;
                        }
                        row.Tag = item;
                    }
                }
            }
        }
        /// <summary>
        /// 请求提交订单
        /// </summary>
        /// <returns></returns>
        private bool RequestSubmitOrder(QueryTrainData data)
        {
            bool result = false;
            RequestPackage request = new RequestPackage();
            request.Encoding = Encoding.UTF8;
            request.RequestURL = "/otn/leftTicket/submitOrderRequest";
            request.RefererURL = "/otn/leftTicket/init";
            request.Params.Add("secretStr", data.secretStr);
            request.Params.Add("train_date", this.dtpTrainDate.Value.ToString("yyyy-MM-dd"));
            request.Params.Add("back_train_date", DateTime.Now.ToString("yyyy-MM-dd"));
            request.Params.Add("tour_flag", "dc");
            request.Params.Add("purpose_codes", "ADULT");
            request.Params.Add("query_from_station_name", data.QueryLeftNewDTO.from_station_name);
            request.Params.Add("query_to_station_name", data.QueryLeftNewDTO.to_station_name);
            ArrayList list = HttpContext.Send(request);
            if (list.Count == 2)
            {
                string jsonResult = Encoding.UTF8.GetString(list[1] as byte[]);
                try
                {
                    ResponseBase response = JsonConvert.DeserializeObject<ResponseBase>(jsonResult);
                    if (response.status)
                    {
                        result = true;
                    }
                    else if (response.messages != null && response.messages.Length > 0)
                    {
                        throw new Exception(response.messages[0]);
                    }
                }
                catch
                {
                    throw new Exception("网络可能存在问题，请重试！");
                }
            }
            else
            {
                Log.Log.Write(list);
            }
            return result;
        }
        /// <summary>
        /// 验查用户登录状态
        /// </summary>
        /// <returns>是否需要重新登录</returns>
        private bool CheckUser()
        {
            bool result = false;
            // 检查登录状态
            RequestPackage request = new RequestPackage()
            {
                Method = "post",
                RequestURL = "/otn/login/checkUser",
                RefererURL = "/otn/leftTicket/init",
            };
            request.Params.Add("_json_att", string.Empty);
            ArrayList list = HttpContext.Send(request);
            if (list.Count == 2)
            {
                string jsonResult = Encoding.UTF8.GetString(list[1] as byte[]);
                ResponseCheckLogin response = JsonConvert.DeserializeObject<ResponseCheckLogin>(jsonResult);
                result = response.Data.flag;
            }
            else
            {
                Log.Log.Write(list);
            }
            return result;
        }
        /// <summary>
        /// 预定
        /// </summary>
        private void Reservation(QueryTrainData data)
        {
            if (data != null)
            {
                try
                {
                    //PWLoading.Show(this, "正在请求提交，请稍后...",
                    //    new Func<object>(() =>
                    //    {
                    //        return this.RequestSubmitOrder(data);
                    //    }),
                    //    new Action<object>((result) =>
                    //    {
                    //        bool res = false;
                    //        if (result != null)
                    //        {
                    //            bool.TryParse(result.ToString(), out res);
                    //        }
                    //        if (res)
                    //        {
                    //            FrmConfirmOrder frmConfirm = new FrmConfirmOrder();
                    //            //if (frmConfirm.ShowDialog() == DialogResult.OK)
                    //            //{
                    //            //}
                    //            frmConfirm.Show(this);
                    //        }
                    //    })
                    //);
                    using (FrmConfirmOrder frmConfirm = new FrmConfirmOrder(data))
                    {
                        if (frmConfirm.ShowDialog() == DialogResult.OK)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "提示");
                }
            }
        }
        #endregion

        #region 定时刷票
        /// <summary>
        /// 开始刷票
        /// </summary>
        private void BeginAuto(object[] param)
        {
            if (AutoThread == null)
            {
                AutoThread = new Thread(Auto);
                AutoThread.IsBackground = true;
                ThreadStatus = true;
            }
            AutoThread.Start(param);
            AutoCount = 0;
            this.txtFrom.Enabled = false;
            this.txtTo.Enabled = false;
            this.dtpAuto.Enabled = false;
            this.chkSetTime.Enabled = false;
            this.dtpStartDate.Enabled = this.chkSetTime.Checked;
            this.btnSeat.Enabled = false;
            this.btnDate.Enabled = false;
            this.btnTrain.Enabled = false;
            this.btnPassenger.Enabled = false;
            this.lblStatus.Text = string.Empty;
        }
        /// <summary>
        /// 停止刷票
        /// </summary>
        private void EndAuto()
        {
            if (AutoThread != null)
            {
                try
                {
                    ThreadStatus = false;
                    Thread.Sleep(100);
                    AutoThread.Abort();
                    AutoThread = null;
                }
                catch
                {
                    AutoThread = null;
                }
                finally
                {
                    AutoThread = null;
                }
            }
            AutoCount = 0;
            this.txtFrom.Enabled = true;
            this.txtTo.Enabled = true;
            this.dtpAuto.Enabled = true;
            this.chkSetTime.Enabled = true;
            this.dtpStartDate.Enabled = this.chkSetTime.Checked;
            this.btnSeat.Enabled = true;
            this.btnDate.Enabled = true;
            this.btnTrain.Enabled = true;
            this.btnPassenger.Enabled = true;
            this.lblStatus.Text = string.Empty;
        }
        /// <summary>
        /// 更新当前刷票状态
        /// </summary>
        private void UpdateStatus(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    this.lblStatus.Text = message;
                }));
            }
            else
            {
                this.lblStatus.Text = message;
            }
        }
        /// <summary>
        /// 刷票定时器
        /// </summary>
        void Auto(object obj)
        {
            object[] array = obj as object[];
            string fromStation = array[0].ToString();
            string toStation = array[1].ToString();
            string trainDate = array[2].ToString();
            while (ThreadStatus)
            {
                lock (TIMER_LOCK)
                {
                    try
                    {
                        if (AutoCount < ulong.MaxValue)
                            AutoCount++;
                        this.UpdateStatus(string.Format("状态：正在进行第 {0} 次刷票...", AutoCount));
                        Thread.Sleep(1000);
                        // 查询
                        QueryTrainData[] dataArray = TrainTicket.Query(fromStation, toStation, trainDate);
                        if (dataArray != null)
                        {
                            Console.WriteLine("data length:" + dataArray.Length);
                        }
                    }
                    catch (Exception ex)
                    {
                        ArrayList list = new ArrayList();
                        list.Add(ex.Message);
                        Log.Log.Write(list);
                    }
                }
            }
        }
        #endregion

        #endregion

        #region 事件

        #region 全局
        /// <summary>
        /// 关于我们
        /// </summary>
        void tsmiAbout_Click(object sender, EventArgs e)
        {
            using (FrmAbout frmAbout = new FrmAbout())
            {
                frmAbout.ShowDialog();
            }
        }
        #endregion

        #region 个人中心
        /// <summary>
        /// 登录
        /// </summary>
        void btnLogin_Click(object sender, EventArgs e)
        {
            this.Login();
        }
        /// <summary>
        /// 在右侧面板中绘制分隔线
        /// </summary>
        void pnlLeft_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.LightGray))
            {
                e.Graphics.DrawLine(
                    pen,
                    e.ClipRectangle.X,
                    e.ClipRectangle.Y,
                    e.ClipRectangle.X,
                    e.ClipRectangle.Height);
            }
        }
        #endregion

        #region 常规购票
        /// <summary>
        /// 查询
        /// </summary>
        void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateQuery())
                {
                    this.btnQuery.Enabled = false;
                    PWLoading.Show(this, "正在查询...", new Action(() =>
                    {
                        QueryTrainData[] data = TrainTicket.Query(
                            this.txtFromStation.Tag.ToString(),
                            this.txtToStation.Tag.ToString(),
                            this.dtpTrainDate.Value.ToString("yyyy-MM-dd"));
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new Action(() =>
                            {
                                this.LoadQueryData(data);
                            }));
                        }
                        else
                        {
                            this.LoadQueryData(data);
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "错误：" + ex.Message, "系统错误");
                this.ResetQuery();
            }
            finally
            {
                this.ResetQuery();
            }
        }
        /// <summary>
        /// 预定
        /// </summary>
        void dgvQuery_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 15)
                {
                    DataGridViewDisableButtonCell cell = this.dgvQuery[e.ColumnIndex, e.RowIndex] as DataGridViewDisableButtonCell;
                    if (cell != null && cell.Enabled)
                    {
                        if (AppContext.LoginUser != null)
                        {
                            this.Reservation(cell.OwningRow.Tag as QueryTrainData);
                        }
                        else
                        {
                            DialogResult result = MessageBox.Show(this, "请先登录。", "提示", MessageBoxButtons.OKCancel);
                            if (result == DialogResult.OK && this.Login() == DialogResult.OK)
                            {
                                this.dgvQuery_CellContentClick(sender, e);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "错误：" + ex.Message, "系统错误");
            }
            return;
        }
        #endregion

        #region 定时刷票
        /// <summary>
        /// 开始刷票 / 停止刷票
        /// </summary>
        void btnStart_Click(object sender, EventArgs e)
        {
            // 验证用户是否登录
            if (AppContext.LoginUser == null)
            {
                DialogResult result = MessageBox.Show(this, "请先登录。", "提示", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    this.Login();
                }
            }
            else
            {
                // 验证设定参数
                if (true)
                {
                    // 开始 / 结束 刷票
                    string state = this.btnStart.Text.Trim();
                    if (state == "开始刷票")
                    {
                        object[] obj = new object[10];
                        obj[0] = this.txtFrom.Tag.ToString();
                        obj[1] = this.txtTo.Tag.ToString();
                        obj[2] = this.dtpAuto.Value.ToString("yyyy-MM-dd");
                        this.btnStart.Text = "停止刷票";
                        this.BeginAuto(obj);
                    }
                    else
                    {
                        this.btnStart.Text = "开始刷票";
                        this.EndAuto();
                    }
                }
            }
        }
        /// <summary>
        /// 选择乘客
        /// </summary>
        void btnPassenger_Click(object sender, EventArgs e)
        {
            if (AppContext.LoginUser == null)
            {
                DialogResult result = MessageBox.Show(this, "请先登录。", "提示", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    if (this.Login() == DialogResult.OK)
                    {
                        goto Label_001;
                    }
                }
            }
            else
            {
                goto Label_001;
            }
            return;
        Label_001:
            using (FrmSelect frmSelect = new FrmSelect(this.bcPassenger))
            {
                frmSelect.ShowDialog();
            }
            return;
        }
        /// <summary>
        /// 是否启用设定时间
        /// </summary>
        void chkSetTime_CheckedChanged(object sender, EventArgs e)
        {
            this.dtpStartDate.Enabled = this.chkSetTime.Checked;
        }
        /// <summary>
        /// 全自动抢票-优先级
        /// </summary>
        void chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            this.cboPriority.Enabled = this.chkAuto.Checked;
        }
        /// <summary>
        /// 绘制分割线
        /// </summary>
        void pnlTimerTicket_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = this.pnlTimerTicket.ClientRectangle;
            e.Graphics.DrawLine(Pens.Gray, rect.X, rect.Y, rect.Width - 1, rect.Y);
            e.Graphics.DrawLine(Pens.Gray, rect.X, rect.Height - 1, rect.Width - 1, rect.Height - 1);
        }
        #endregion

        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.dtpTrainDate.MaxDate = DateTime.Now.AddDays(19);
            this.dtpTrainDate.MinDate = DateTime.Now;
            this.dtpAuto.MaxDate = DateTime.Now.AddDays(19);
            this.dtpAuto.MinDate = DateTime.Now;
            this.lblNotify.Image = AppResource.GetImage("Images.icon_date_notice.png");
            this.lblNotify.Text = string.Format(
                "      今天可以抢{0} - {1}的票",
                this.dtpTrainDate.MinDate.ToString("MM月dd日"),
                this.dtpTrainDate.MaxDate.ToString("MM月dd日"));
            this.tpCenter.Image = AppResource.GetImage("Images.tab.appbar.user.png");
            this.tpBuyTicket.Image = AppResource.GetImage("Images.tab.appbar.train.png");
            this.tpAuto.Image = AppResource.GetImage("Images.tab.appbar.timer.png");
            this.dtpAuto.Value = DateTime.Now;
            this.dtpStartDate.Value = DateTime.Now;
            this.cboPriority.SelectedIndex = 0;
        }
        protected override void DestroyHandle()
        {
            this.EndAuto();
            base.DestroyHandle();
        }
        #endregion
    }
}
