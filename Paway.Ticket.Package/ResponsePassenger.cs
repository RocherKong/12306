using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paway.Ticket.Package.Data;
using Newtonsoft.Json;

namespace Paway.Ticket.Package
{
    /// <summary>
    /// 旅客包
    /// </summary>
    [Serializable]
    public class ResponsePassenger : ResponseBase
    {
        public PassengerData Data
        {
            get
            {
                PassengerData data = null;
                if (base.data != null && base.data.ToString() != string.Empty)
                {
                    data = JsonConvert.DeserializeObject<PassengerData>(base.data.ToString());
                }
                return data;
            }
        }
    }
}
