using System;
using System.Collections.Generic;
using System.Text;
using Paway.Ticket.Package.Data;
using System.Collections;
using Newtonsoft.Json;
using Paway.Ticket.Package;
using Paway.Ticket.Http;

namespace Paway.Ticket.Entity
{
    /// <summary>
    /// 登录帐户
    /// </summary>
    [Serializable]
    public class LoginUser
    {
        #region 变量
        
        private string _userName = string.Empty;
        private string _password = string.Empty;
        private PassengerDetail[] _passengers = null;

        #endregion

        #region 属性
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName
        {
            get { return this._userName; }
            set { this._userName = value; }
        }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password
        {
            get { return this._password; }
            set { this._password = value; }
        }
        /// <summary>
        /// 乘客信息
        /// </summary>
        public PassengerDetail[] Passengers 
        {
            get
            {
                if (this._passengers == null)
                {
                    RequestPackage request = new RequestPackage();
                    request.RequestURL = "/otn/confirmPassenger/getPassengerDTOs";
                    request.RefererURL = "/otn/confirmPassenger/initDc";
                    request.Method = "post";
                    request.Params.Add("_json_att", string.Empty);
                    ArrayList list = HttpContext.Send(request);
                    if (list.Count == 2)
                    {
                        string jsonResult = Encoding.UTF8.GetString(list[1] as byte[]);
                        ResponsePassenger response = JsonConvert.DeserializeObject<ResponsePassenger>(jsonResult);
                        if (response.status)
                        {
                            this._passengers = response.Data.Normal_Passengers;
                        }
                    }
                    else
                    {
                        Log.Log.Write(list);
                    }
                }
                return this._passengers;
            }
        }

        #endregion
    }
}
