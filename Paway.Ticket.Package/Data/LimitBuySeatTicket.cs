using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paway.Ticket.Package.Data
{
    [Serializable]
    public class LimitBuySeatTicket
    {
        public DataTypes[] seat_type_codes { get; set; }
        public object ticket_seat_codeMap { get; set; }
        public DataTypes[] ticket_type_codes { get; set; }
    }
}
