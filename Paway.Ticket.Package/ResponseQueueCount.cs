using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paway.Ticket.Package.Data;
using Newtonsoft.Json;

namespace Paway.Ticket.Package
{
    [Serializable]
    public class ResponseQueueCount : ResponseBase
    {
        public QueueCountData Data
        {
            get
            {
                QueueCountData data = null;
                if (base.data != null && base.data.ToString() != string.Empty)
                {
                    data = JsonConvert.DeserializeObject<QueueCountData>(base.data.ToString());
                }
                return data;
            }
        }
    }
}
