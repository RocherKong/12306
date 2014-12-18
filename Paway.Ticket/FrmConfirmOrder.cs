using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Paway.Ticket.Entity;
using System.Collections;
using Newtonsoft.Json;
using Paway.Ticket.Package.Data;
using System.Threading;
using System.IO;
using Paway.Ticket.Package;
using Paway.Ticket.Http;
using Paway.Ticket.UI;

namespace Paway.Ticket
{
    /// <summary>
    /// 确认订单
    /// </summary>
    public partial class FrmConfirmOrder : _360Form
    {
        #region 变量
        private static TicketInfoForPassengerForm TicketInfo = null;
        private static QueryTrainData Data = null;
        private static string SubmitToken = null;
        private static bool IsInit = false;

        private const string RES_PATH = "Images.Loading.{0}.png";
        private int _res_index = 1;
        #endregion

        #region 构造函数
        public FrmConfirmOrder(QueryTrainData data)
        {
            this.InitializeComponent();
            this.LoadEvents();
            Data = data;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 加载事件
        /// </summary>
        private void LoadEvents()
        {
            this.btnEnter.Click += new EventHandler(btnEnter_Click);
            this.picCode.Click += new EventHandler(picCode_Click);
            this.txtCode.TextChanged += new EventHandler(txtCode_TextChanged);
            this.timer.Tick += new EventHandler(timer_Tick);
        }
        /// <summary>
        /// 更新界面的乘客信息
        /// </summary>
        /// <param name="array"></param>
        private void UpdatePassengers()
        {
            this.pnlPassenger.Controls.Clear();
            if (AppContext.LoginUser != null)
            {
                PassengerDetail[] array = AppContext.LoginUser.Passengers;
                if (array != null && array.Length > 0)
                {
                    this.pnlPassenger.SuspendLayout();
                    int xPos = 5, yPos = 5, index = 0;
                    foreach (PassengerDetail passenger in array)
                    {
                        CheckBox chkPassenger = new CheckBox();
                        chkPassenger.Text = passenger.passenger_name;
                        chkPassenger.Tag = passenger;
                        chkPassenger.Size = new System.Drawing.Size(80, 20);
                        chkPassenger.Location = new Point(xPos, yPos);
                        chkPassenger.CheckedChanged -= new EventHandler(chkPassenger_CheckedChanged);
                        chkPassenger.CheckedChanged += new EventHandler(chkPassenger_CheckedChanged);
                        index++;
                        if (index % 6 == 0)
                        {
                            xPos = 5;
                            yPos += 30;
                        }
                        else
                        {
                            xPos += 100;
                        }
                        this.pnlPassenger.Controls.Add(chkPassenger);
                    }
                    this.pnlPassenger.ResumeLayout(false);
                    this.pnlPassenger.PerformLayout();
                }
            }
            else
            {
                MessageBox.Show("请先登录");
            }
        }
        /// <summary>
        /// 启动计时器
        /// </summary>
        private void StartTimer()
        {
            if (!this.timer.Enabled)
            {
                this._res_index = 1;
                this.timer.Start();
            }
        }
        /// <summary>
        /// 停止计时器
        /// </summary>
        private void StopTimer()
        {
            if (this.timer.Enabled)
            {
                this._res_index = 1;
                this.timer.Stop();
            }
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        private void GetValidateCode()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.StartTimer();
                        this.txtCode.Text = string.Empty;
                    }));
                }
                else
                {
                    this.StartTimer();
                    this.txtCode.Text = string.Empty;
                }
                // 获取验证码
                RequestPackage request = new RequestPackage();
                request.RequestURL = "/otn/passcodeNew/getPassCodeNew";
                request.Params.Add("module", "passenger");
                request.Params.Add("rand", "randp");
                using (Stream stream = HttpContext.DownloadCode(request))
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            this.ShowValidateCode(stream);
                        }));
                    }
                    else
                    {
                        this.ShowValidateCode(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "系统提示");
            }
        }
        /// <summary>
        /// 显示验证码
        /// </summary>
        /// <param name="stream">验证码数据流</param>
        private void ShowValidateCode(Stream stream)
        {
            try
            {
                if (stream == null)
                {
                    this.picCode.Image = AppResource.GetImage("Images.Loading.error.png");
                }
                this.StopTimer();
                this.picCode.Image = Image.FromStream(stream);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            if (TicketInfo != null)
            {
                // 显示列车信息
                if (TicketInfo.queryLeftTicketRequestDTO != null)
                {
                    DateTime start_date = TicketInfo.queryLeftTicketRequestDTO.TrainDate;
                    this.lblTrainInfo.Text = string.Format(
                        "{0}（{1}）  {2}次 {3}站（{4}开）-- {5}站（{6}到）",
                        start_date.ToString("yyyy-MM-dd"),
                        DateExt.GetWeekSimpleChineseStr(start_date.DayOfWeek),
                        TicketInfo.queryLeftTicketRequestDTO.station_train_code,
                        TicketInfo.queryLeftTicketRequestDTO.from_station_name,
                        TicketInfo.queryLeftTicketRequestDTO.start_time,
                        TicketInfo.queryLeftTicketRequestDTO.to_station_name,
                        TicketInfo.queryLeftTicketRequestDTO.arrive_time);
                }
                // 显示车票信息
                StringBuilder sbTicketInfo = new StringBuilder();
                if (TicketInfo.leftDetails != null && TicketInfo.leftDetails.Length > 0)
                {
                    foreach (string item in TicketInfo.leftDetails)
                    {
                        sbTicketInfo.Append(item);
                        sbTicketInfo.Append("    ");
                    }
                }
                this.lblTicketInfo.Text = sbTicketInfo.ToString();
                if (TicketInfo.limitBuySeatTicketDTO != null)
                {
                    // 加载证件类型
                    if (TicketInfo.cardTypes != null && TicketInfo.cardTypes.Length > 0)
                    {
                        DataGridViewComboBoxColumn colCard = this.dgvPassenger.Columns["colCardType"] as DataGridViewComboBoxColumn;
                        colCard.DisplayMember = "value";
                        colCard.ValueMember = "id";
                        colCard.DataSource = TicketInfo.cardTypes;
                    }
                    // 加载车票类型
                    if (TicketInfo.limitBuySeatTicketDTO.ticket_type_codes != null)
                    {
                        DataGridViewComboBoxColumn colType = this.dgvPassenger.Columns["colType"] as DataGridViewComboBoxColumn;
                        colType.DisplayMember = "value";
                        colType.ValueMember = "id";
                        //colType.DefaultCellStyle.NullValue = TicketInfo.limitBuySeatTicketDTO.ticket_type_codes[0].value;
                        colType.DataSource = TicketInfo.limitBuySeatTicketDTO.ticket_type_codes;
                    }
                    // 获取座位价格等信息
                    if (TicketInfo.limitBuySeatTicketDTO.seat_type_codes != null)
                    {
                        DataGridViewComboBoxColumn colSeat = this.dgvPassenger.Columns["colSeat"] as DataGridViewComboBoxColumn;
                        colSeat.DisplayMember = "value";
                        colSeat.ValueMember = "id";
                        colSeat.DefaultCellStyle.NullValue = TicketInfo.limitBuySeatTicketDTO.seat_type_codes[0].value;
                        colSeat.DataSource = TicketInfo.limitBuySeatTicketDTO.seat_type_codes;
                    }
                }
            }
        }
        /// <summary>
        /// 请求提交订单
        /// </summary>
        /// <returns></returns>
        private bool RequestSubmitOrder()
        {
            bool result = false;
            if (Data != null)
            {
                RequestPackage request = new RequestPackage();
                request.Encoding = Encoding.UTF8;
                request.RequestURL = "/otn/leftTicket/submitOrderRequest";
                request.RefererURL = "/otn/leftTicket/init";
                request.Params.Add("secretStr", Data.secretStr);
                request.Params.Add("train_date", Data.QueryLeftNewDTO.StartTrainDate.ToString("yyyy-MM-dd"));
                request.Params.Add("back_train_date", DateTime.Now.ToString("yyyy-MM-dd"));
                request.Params.Add("tour_flag", "dc");
                request.Params.Add("purpose_codes", "ADULT");
                request.Params.Add("query_from_station_name", Data.QueryLeftNewDTO.from_station_name);
                request.Params.Add("query_to_station_name", Data.QueryLeftNewDTO.to_station_name);
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
            }
            return result;
        }
        /// <summary>
        /// 提交订单
        /// </summary>
        private bool SubmitOrder(string passengerTicketStr, string oldPassengerStr, string code)
        {
            bool result = false;
            PWLoading.Show(this, "正在检查订单，请稍后...", new Action(() =>
            {
                RequestPackage request = new RequestPackage();
                request.Encoding = Encoding.UTF8;
                request.Method = "post";
                request.RefererURL = "/otn/confirmPassenger/initDc";
                request.RequestURL = "/otn/confirmPassenger/checkOrderInfo";
                request.Params.Add("cancel_flag", "2");
                request.Params.Add("bed_level_order_num", "000000000000000000000000000000");
                request.Params.Add("passengerTicketStr", passengerTicketStr);
                request.Params.Add("oldPassengerStr", oldPassengerStr);
                request.Params.Add("tour_flag", TicketInfo.tour_flag);
                request.Params.Add("randCode", code);
                request.Params.Add("_json_att", string.Empty);
                request.Params.Add("REPEAT_SUBMIT_TOKEN", SubmitToken);
                ArrayList list = HttpContext.Send(request);
                if (list.Count == 2)
                {
                    PWLoading.UpdateMessage("正在查询余票数量...");
                    string jsonResult = Encoding.UTF8.GetString(list[1] as byte[]);
                    ResponseBase response = JsonConvert.DeserializeObject<ResponseCheckOrderInfo>(jsonResult);
                    if (response.status)
                    {
                        ResponseCheckOrderInfo res_check_order = response as ResponseCheckOrderInfo;
                        if (res_check_order.Data.submitStatus)
                        {
                            string date = TicketInfo.queryLeftTicketRequestDTO.TrainDate.ToString("ddd MMM dd yyyy 00:00:00 ",
                                System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + "GMT + 0800(中国标准时间)";
                            string train_date = Data.QueryLeftNewDTO.StartTrainDate.ToString(
                                "ddd MMM dd yyyy 00:00:00 ",
                                System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + "GMT + 0800(中国标准时间)";
                            request.RequestURL = "/otn/confirmPassenger/getQueueCount";
                            request.RefererURL = "/otn/confirmPassenger/initDc";
                            request.Params.Clear();
                            request.Params.Add("train_date", System.Web.HttpUtility.UrlEncode(train_date));
                            request.Params.Add("train_no", System.Web.HttpUtility.UrlEncode(Data.QueryLeftNewDTO.train_no));
                            request.Params.Add("stationTrainCode", System.Web.HttpUtility.UrlEncode(Data.QueryLeftNewDTO.station_train_code));
                            request.Params.Add("seatType", System.Web.HttpUtility.UrlEncode("0"));
                            request.Params.Add("fromStationTelecode", System.Web.HttpUtility.UrlEncode(Data.QueryLeftNewDTO.from_station_telecode));
                            request.Params.Add("toStationTelecode", System.Web.HttpUtility.UrlEncode(Data.QueryLeftNewDTO.to_station_telecode));
                            request.Params.Add("leftTicket", System.Web.HttpUtility.UrlEncode(Data.QueryLeftNewDTO.yp_info));
                            request.Params.Add("purpose_codes", System.Web.HttpUtility.UrlEncode("00"));
                            request.Params.Add("_json_att", string.Empty);
                            request.Params.Add("REPEAT_SUBMIT_TOKEN", System.Web.HttpUtility.UrlEncode(SubmitToken));
                            list = HttpContext.Send(request);
                            if (list.Count == 2)
                            {
                                PWLoading.UpdateMessage("正在确认订单...");
                                jsonResult = Encoding.UTF8.GetString(list[1] as byte[]);
                                response = JsonConvert.DeserializeObject<ResponseQueueCount>(jsonResult);
                                if (response.status)
                                {
                                    request.RequestURL = "/otn/confirmPassenger/confirmSingleForQueue";
                                    request.RefererURL = "/otn/confirmPassenger/initDc";
                                    request.Params.Clear();
                                    request.Params.Add("passengerTicketStr", passengerTicketStr);
                                    request.Params.Add("oldPassengerStr", oldPassengerStr);
                                    request.Params.Add("randCode", code);
                                    request.Params.Add("purpose_codes", TicketInfo.purpose_codes);
                                    request.Params.Add("key_check_isChange", TicketInfo.key_check_isChange);
                                    request.Params.Add("leftTicketStr", TicketInfo.leftTicketStr);
                                    request.Params.Add("train_location", TicketInfo.train_location);
                                    request.Params.Add("_json_att", string.Empty);
                                    request.Params.Add("REPEAT_SUBMIT_TOKEN", SubmitToken);
                                    list = HttpContext.Send(request);
                                    jsonResult = Encoding.UTF8.GetString(list[1] as byte[]);
                                    response = JsonConvert.DeserializeObject<ResponseSubmit>(jsonResult);
                                    if (response.status)
                                    {
                                        if ((response as ResponseSubmit).Data.submitStatus)
                                        {
                                            result = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Log.Log.Write(list);
                            }
                        }
                        else
                        {
                            Log.Log.Write(list);
                            throw new Exception(res_check_order.Data.errMsg);
                        }
                    }
                    else if (response.messages != null && response.messages.Length > 0)
                    {
                        string message = response.messages[0];
                        Log.Log.Write(list);
                        throw new Exception(message);
                    }
                }
                else
                {
                    Log.Log.Write(list);
                }
            }));
            return result;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 更换验证码
        /// </summary>
        void picCode_Click(object sender, EventArgs e)
        {
            if (IsInit && !this.timer.Enabled)
            {
                GetValidateCode();
            }
        }
        /// <summary>
        /// 当输入验证码长度达到4位时自动确认订单
        /// </summary>
        void txtCode_TextChanged(object sender, EventArgs e)
        {
            int length = Encoding.Default.GetBytes(this.txtCode.Text.Trim()).Length;
            if (length == 4)
            {
                this.btnEnter_Click(this.btnEnter, EventArgs.Empty);
            }
        }
        /// <summary>
        /// 确认订单
        /// </summary>
        void btnEnter_Click(object sender, EventArgs e)
        {
            if (this.dgvPassenger.Rows.Count == 0)
            {
                MessageBox.Show(this, "请选择联系人。", "提示");
                return;
            }
            string code = this.txtCode.Text.Trim();

            StringBuilder passengerTicketStr = new StringBuilder();
            StringBuilder oldPassengerStr = new StringBuilder();

            string type, name, idType, idNo;
            foreach (DataGridViewRow item in this.dgvPassenger.Rows)
            {
                type = item.Cells[3].Value.ToString();
                name = item.Cells[4].Value.ToString();
                idType = item.Cells[5].Value.ToString();
                idNo = item.Cells[6].Value.ToString();
                passengerTicketStr.AppendFormat(
                    "{0},0,{1},{2},{3},{4},{5},N",
                    item.Cells[2].Value.ToString(),
                    type,
                    name,
                    idType,
                    idNo,
                    item.Cells[7].Value.ToString());
                oldPassengerStr.AppendFormat(
                    "{0},{1},{2},{3}_",
                    name, idType, idNo, type);
                if (item.Index != this.dgvPassenger.Rows.Count - 1)
                {
                    passengerTicketStr.Append("_");
                }
            }
            try
            {
                if (this.SubmitOrder(passengerTicketStr.ToString(), oldPassengerStr.ToString(), code))
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageBox.Show(this, "预定成功，请到未完成订单中查看。", "提示");
                            this.DialogResult = DialogResult.OK;
                        }));
                    }
                    else
                    {
                        MessageBox.Show(this, "预定成功，请到未完成订单中查看。", "提示");
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show(this, ex.Message, "提示");
                    }));
                }
                else
                {
                    MessageBox.Show(this, ex.Message, "提示");
                }
            }
        }
        /// <summary>
        /// 添加乘客
        /// </summary>
        void chkPassenger_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                PassengerDetail detail = checkBox.Tag as PassengerDetail;
                if (detail != null)
                {
                    if (checkBox.Checked)
                    {
                        int row_index = this.dgvPassenger.Rows.Add(new DataGridViewRow());
                        DataGridViewRow row = this.dgvPassenger.Rows[row_index];
                        row.Cells[0].Value = detail.code;
                        row.Cells[1].Value = this.dgvPassenger.Rows.Count;
                        row.Cells[2].Value = TicketInfo.limitBuySeatTicketDTO.seat_type_codes[0].id;
                        row.Cells[3].Value = detail.passenger_type;
                        row.Cells[3].OwningColumn.DefaultCellStyle.NullValue = detail.passenger_type_name;
                        row.Cells[4].Value = detail.passenger_name;
                        row.Cells[5].Value = detail.passenger_id_type_code;
                        row.Cells[5].OwningColumn.DefaultCellStyle.NullValue = detail.passenger_id_type_name;
                        row.Cells[6].Value = detail.passenger_id_no;
                        row.Cells[7].Value = detail.mobile_no;
                    }
                    else
                    {
                        DataGridViewRow row = null;
                        foreach (DataGridViewRow item in this.dgvPassenger.Rows)
                        {
                            if (item.Cells[0].Value.ToString() == detail.code)
                            {
                                row = item;
                                break;
                            }
                        }
                        if (row != null)
                            this.dgvPassenger.Rows.Remove(row);
                    }
                }
            }
        }
        /// <summary>
        /// 显示加载图片
        /// </summary>
        void timer_Tick(object sender, EventArgs e)
        {
            Image image = AppResource.GetImage(string.Format(RES_PATH, _res_index));
            if (_res_index == 8)
                _res_index = 1;
            else
                _res_index++;
            this.picCode.Image = image;
        }
        #endregion

        #region Override Methods

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            PWLoading.Show(this, "正在获取相关数据，请稍后...", new Action(() =>
            {
                if (RequestSubmitOrder())
                {
                    // 获取页面信息
                    RequestPackage request = new RequestPackage();
                    request.RequestURL = "/otn/confirmPassenger/initDc";
                    request.RefererURL = "/otn/leftTicket/init";
                    request.Params.Add("_json_att=", string.Empty);
                    request.Method = "post";
                    ArrayList list = HttpContext.GetHtmlData(request);
                    if (IsInit = (list.Count == 3))
                    {
                        GetValidateCode();
                        string html_text = list[1].ToString();
                        // 获取 Token
                        SubmitToken = StringExt.FindJsValue(html_text, "globalRepeatSubmitToken");
                        // 获取 ticketInfoForPassengerForm
                        string ticketInfoForPassenger = StringExt.FindJsValue(html_text, "ticketInfoForPassengerForm");
                        TicketInfo = JsonConvert.DeserializeObject<TicketInfoForPassengerForm>(ticketInfoForPassenger);
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new Action(() =>
                            {
                                this.LoadData();
                                this.UpdatePassengers();
                            }));
                        }
                        else
                        {
                            this.LoadData();
                            this.UpdatePassengers();
                        }
                    }
                    else
                    {
                        Log.Log.Write(list);
                    }
                }
            }));
        }

        #endregion
    }
}
