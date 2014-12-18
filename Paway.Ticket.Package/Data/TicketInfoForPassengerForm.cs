using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paway.Ticket.Package.Data
{
    [Serializable]
    public class TicketInfoForPassengerForm
    {
        public DataTypes[] cardTypes { get; set; }
        public string isAsync { get; set; }
        public string key_check_isChange { get; set; }
        public string[] leftDetails { get; set; }
        public string leftTicketStr { get; set; }
        public LimitBuySeatTicket limitBuySeatTicketDTO { get; set; }
        public string maxTicketNum { get; set; }
        public object orderRequestDTO { get; set; }
        public string purpose_codes { get; set; }
        public QueryLeftNewDetail queryLeftNewDetailDTO { get; set; }
        public QueryLeftTicketRequest queryLeftTicketRequestDTO { get; set; }
        public string tour_flag { get; set; }
        public string train_location { get; set; }
    }
}
