using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paway.Ticket.Package.Data
{
    [Serializable]
    public class CheckOrderInfoData
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        public string errMsg { get; set; }
        /// <summary>
        /// 提交状态是否成功
        /// </summary>
        public bool submitStatus { get; set; }
    }
}
