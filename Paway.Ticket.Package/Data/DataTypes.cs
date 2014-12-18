using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paway.Ticket.Package.Data
{
    [Serializable]
    public class DataTypes
    {
        public DataTypes() { }
        public DataTypes(string id, string value)
        {
            this.id = id;
            this.value = value;
        }
        public string end_station_name { get; set; }
        public string end_time { get; set; }
        public string id { get; set; }
        public string start_station_name { get; set; }
        public string start_time { get; set; }
        public string value { get; set; }

        public override string ToString()
        {
            return this.value;
        }
    }
}
