using System;
using System.Collections.Generic;
using System.Text;

namespace Paway.Ticket.Entity
{
    [Serializable]
    public class City
    {
        //"bjb|北京北|VAP|beijingbei|bjb|0"
        public string chineseName { get; set; }
        public string code { get; set; }
        public string allPin { get; set; }
        public string simplePin { get; set; }
        public string No { get; set; }

        public override string ToString()
        {
            return this.chineseName;
        }
    }
}
