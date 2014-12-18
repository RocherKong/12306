using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.IO;
using System.Collections;
using Paway.Ticker.Entity;
using Newtonsoft.Json;
using Paway.Ticker.Entity.Data;
using Paway.Ticker.Enums;
using System.Threading;

namespace Paway.Ticker
{
    public partial class FrmMain2 : Form
    {
        #region 构造函数

        public FrmMain2()
        {
            InitializeComponent();
            this.LoadEvents();
        }

        #endregion

        #region 方法
        /// <summary>
        /// 加载事件
        /// </summary>
        private void LoadEvents()
        {
            this.btnQuery.Click += new EventHandler(btnQuery_Click);
            this.btnAuto.Click += new EventHandler(btnAuto_Click);
            this.dgvQuery.CellContentClick += new DataGridViewCellEventHandler(dgvQuery_CellContentClick);
        }
        /// <summary>
        /// 验证查询条件
        /// </summary>
        /// <returns></returns>
        private bool ValidateQuery()
        {
            bool result = true;
            //if (this.txtFromStation.Tag == null)
            //{
            //    result = false;
            //    AppContext.Tip.Show(
            //        "请选择出发地。", 
            //        this.txtFromStation, 
            //        this.txtFromStation.Location.X, 
            //        this.txtFromStation.Location.Y - 20,
            //        2000);
            //}
            //else if (this.txtToStation.Tag == null)
            //{
            //    result = false;
            //    AppContext.Tip.Show(
            //        "请选择目的地。",
            //        this.txtToStation,
            //        this.txtToStation.Location.X,
            //        this.txtToStation.Location.Y - 20,
            //        2000);
            //}
            return result;
        }
        /// <summary>
        /// 加载查询数据
        /// </summary>
        /// <param name="dataArray"></param>
        private void LoadQueryData(QueryTrainData[] dataArray)
        {
            this.dgvQuery.Rows.Clear();
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
        /// <summary>
        /// 预定
        /// </summary>
        private void BookTheTicket(QueryTrainData data)
        {
            if (/*this.CheckUser() && */this.RequestSubmitOrder(data))
            {
                using (FrmConfirmOrder frmConfirm = new FrmConfirmOrder())
                {
                    if (frmConfirm.ShowDialog() == DialogResult.OK)
                    {
                        //this.SubmitOrder(data, frmConfirm.Code, submitToken_text, ticketInfo);
                    }
                }
            }
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
            return result;
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
                ResponseBase response = JsonConvert.DeserializeObject<ResponseBase>(jsonResult);
                if (response.status && response.httpstatus == 200)
                {
                    result = true;
                }
                else if (response.messages != null && response.messages.Length > 0)
                {
                    throw new Exception(response.messages[0]);
                }
            }
            return result;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 查询
        /// </summary>
        void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                //if (this.ValidateQuery())
                //{
                //    this.btnQuery.Enabled = false;
                //    ThreadPool.QueueUserWorkItem((obj) =>
                //    {
                //        ResponseQuery response = null;
                //        RequestPackage request = new RequestPackage();
                //        request.Method = EHttpMethod.Get.ToString();
                //        request.RefererURL = "/otn/leftTicket/init";
                //        request.RequestURL = "/otn/leftTicket/query";//otn/leftTicket/query
                //        request.Params.Add("leftTicketDTO.train_date", this.dtpTrainDate.Value.ToString("yyyy-MM-dd"));
                //        request.Params.Add("leftTicketDTO.from_station", this.txtFromStation.Tag.ToString());
                //        request.Params.Add("leftTicketDTO.to_station", this.txtToStation.Tag.ToString());
                //        request.Params.Add("purpose_codes", "ADULT");
                //    Label_002:
                //        ArrayList list = HttpContext.Send(request);
                //        if (list.Count == 2)
                //        {
                //            string jsonString = Encoding.UTF8.GetString(list[1] as byte[]);
                //            response = JsonConvert.DeserializeObject<ResponseQuery>(jsonString);
                //            if (response.status)
                //            {
                //                goto Label_001;
                //            }
                //            else if (response.messages != null && response.messages.Length > 0)
                //            {
                //                this.Invoke(new Action(() =>
                //                {
                //                    MessageBox.Show(this, response.messages[0], "提示");
                //                    this.btnQuery.Enabled = true;
                //                }));
                //                goto Label_003;
                //            }
                //            else if (!string.IsNullOrEmpty(response.c_url))
                //            {
                //                request.RequestURL = "/otn/" + response.c_url;
                //                goto Label_002;
                //            }
                //        }
                //        return;
                //    Label_001:
                //        if (response.Data != null && response.Data.Length > 0)
                //        {
                //            if (this.InvokeRequired)
                //            {
                //                this.Invoke(new Action(() =>
                //                {
                //                    this.LoadQueryData(response.Data);
                //                }));
                //            }
                //            else
                //            {
                //                this.LoadQueryData(response.Data);
                //            }
                //            goto Label_003;
                //        }
                //        return;
                //    Label_003:
                //        if (this.InvokeRequired)
                //        {
                //            this.Invoke(new Action(() => { this.btnQuery.Enabled = true; }));
                //        }
                //        else
                //        {
                //            this.btnQuery.Enabled = true;
                //        }
                //        return;
                //    });
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "错误：" + ex.Message, "系统错误");
                this.btnQuery.Enabled = true;
            }
        }
        /// <summary>
        /// 自动预定
        /// </summary>
        void btnAuto_Click(object sender, EventArgs e)
        {
            using (FrmAutoSettings frmAutoSettings = new FrmAutoSettings())
            {
                if (frmAutoSettings.ShowDialog() == DialogResult.OK)
                {

                }
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
                    if (cell.Enabled)
                    {
                        QueryTrainData data = cell.OwningRow.Tag as QueryTrainData;
                        if (data != null)
                        {
                            this.BookTheTicket(data);
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

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.dtpTrainDate.MaxDate = DateTime.Now.AddDays(20);
            this.dtpTrainDate.MinDate = DateTime.Now;
        }
        #endregion
    }
}
