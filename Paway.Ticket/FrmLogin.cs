using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using Paway.Ticket.Package.Data;
using Paway.Ticket.Entity;
using System.Threading;
using Paway.Ticket.Package;
using Paway.Ticket.Http;
using Paway.Ticket.UI;

namespace Paway.Ticket
{
    /// <summary>
    /// 登录窗口
    /// </summary>
    public partial class FrmLogin : _360Form
    {
        #region 变量
        private const string RES_PATH = "Images.Loading.{0}.png";
        private int _res_index = 1;
        #endregion

        #region 构造函数

        public FrmLogin()
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
            this.btnLogin.Click += new EventHandler(btnLogin_Click);
            this.picCode.Click += new EventHandler(picCode_Click);
            this.txtCode.TextChanged += new EventHandler(txtCode_TextChanged);
            this.timer.Tick += new EventHandler(timer_Tick);
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        private void GetValidateCode()
        {
            try
            {
                // 启动计时器
                if (!this.timer.Enabled)
                {
                    this._res_index = 1;
                    this.timer.Start();
                }
                this.txtCode.Text = string.Empty;
                ThreadPool.QueueUserWorkItem((obj) =>
                {
                    RequestPackage request = new RequestPackage("/otn/login/init");
                    ArrayList list = HttpContext.GetHtmlData(request);
                    if (list.Count == 3)
                    {
                        string path = Path.Combine(AppContext.ValidateCode_Path, list[2] + ".png");
                        request.RequestURL = "/otn/passcodeNew/getPassCodeNew";
                        request.Params.Add("module", "login");
                        request.Params.Add("rand", "sjrand");
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
                    else
                    {
                        Log.Log.Write(list);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "系统提示");
            }
        }
        /// <summary>
        /// 显示验证码
        /// </summary>
        private void ShowValidateCode(Stream stream)
        {
            try
            {
                if (stream == null)
                {
                    this.picCode.Image = AppResource.GetImage("Images.Loading.error.png");
                }
                if (this.timer.Enabled)
                {
                    this.timer.Stop();
                }
                this.picCode.Image = Image.FromStream(stream);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 更换验证码
        /// </summary>
        void picCode_Click(object sender, EventArgs e)
        {
            if (!this.timer.Enabled)
                this.GetValidateCode();
        }
        /// <summary>
        /// 当输入验证码的长度达到4位时，自动登录
        /// </summary>
        void txtCode_TextChanged(object sender, EventArgs e)
        {
            int length = Encoding.Default.GetBytes(this.txtCode.Text.Trim()).Length;
            if (length == 4)
            {
                this.btnLogin_Click(this.btnLogin, EventArgs.Empty);
            }
        }
        /// <summary>
        /// 登录 12306
        /// </summary>
        void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnLogin.Enabled = false;
                AppContext.Tip.Show(
                    "正在登录...",
                    this,
                    this.btnLogin.Location.X, this.btnLogin.Location.Y - 20);
                string loginName = this.txtLoginName.Text.Trim();
                string loginPwd = this.txtLoginPwd.Text.Trim();
                string code = this.txtCode.Text.Trim();
                PWLoading.Show(this, "正在登录...", new Action(() =>
                {
                    RequestPackage request = new RequestPackage();
                    request.Params.Add("loginUserDTO.user_name", System.Web.HttpUtility.UrlEncode(loginName));
                    request.Params.Add("userDTO.password", System.Web.HttpUtility.UrlEncode(loginPwd));
                    request.Params.Add("randCode", System.Web.HttpUtility.UrlEncode(code));
                    request.RequestURL = "/otn/login/loginAysnSuggest";
                    request.RefererURL = "/otn/login/init";
                    request.Method = "post";
                    ArrayList list = HttpContext.Send(request);
                    if (list.Count == 2)
                    {
                        string jsonResult = Encoding.UTF8.GetString(list[1] as byte[]);
                        ResponseLogin package = JsonConvert.DeserializeObject<ResponseLogin>(jsonResult);
                        if (package.Data != null && package.Data.loginCheck == "Y")
                        {
                            AppContext.LoginUser = new LoginUser()
                            {
                                UserName = loginName,
                                Password = loginPwd,
                            };
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            if (package.messages != null && package.messages.Length > 0)
                            {
                                if (this.InvokeRequired)
                                {
                                    this.Invoke(new Action(() =>
                                    {
                                        MessageBox.Show(this, package.messages[0], "提示");
                                        this.btnLogin.Enabled = true;
                                        this.GetValidateCode();
                                        this.txtCode.Focus();
                                    }));
                                }
                            }
                        }
                    }
                    else
                    {
                        Log.Log.Write(list);
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "错误：" + ex.Message, "系统错误");
                this.btnLogin.Enabled = true;
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

        #region override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

#if DEBUG
            this.txtLoginName.Text = "";
            this.txtLoginPwd.Text = "";
#endif
            this.GetValidateCode();
            if (this.txtLoginName.Text.Trim() == string.Empty)
            {
                this.txtLoginName.Select();
            }
            else if (this.txtLoginPwd.Text.Trim() == string.Empty)
            {
                this.txtLoginPwd.Select();
            }
            else
            {
                this.txtCode.Select();
            }
        }
        #endregion
    }
}
