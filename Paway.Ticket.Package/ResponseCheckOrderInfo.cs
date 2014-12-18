using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Paway.Ticket.Package.Data;

namespace Paway.Ticket.Package
{
    [Serializable]
    public class ResponseCheckOrderInfo : ResponseBase
    {
        public CheckOrderInfoData Data
        {
            get
            {
                CheckOrderInfoData data = null;
                if (base.data != null && base.data.ToString() != string.Empty)
                {
                    data = JsonConvert.DeserializeObject<CheckOrderInfoData>(base.data.ToString());
                }
                return data;
            }
        }
    }
}
