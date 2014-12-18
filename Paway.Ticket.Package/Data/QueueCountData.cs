using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paway.Ticket.Package.Data
{
    [Serializable]
    public class QueueCountData
    {
        public string isRelogin { get; set; }
        public string count { get; set; }
        public string ticket { get; set; }
        public string op_2 { get; set; }
        public string countT { get; set; }
        public string op_1 { get; set; }
    }
}
