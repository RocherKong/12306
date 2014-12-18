using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paway.Ticket.Package.Data
{
    [Serializable]
    public class QueryLeftTicketRequest
    {
        public string arrive_time { get; set; }
        public string bigger20 { get; set; }
        public string from_station { get; set; }
        public string from_station_name { get; set; }
        public string from_station_no { get; set; }
        public string lishi { get; set; }
        public string MyProperty { get; set; }
        public string purpose_codes { get; set; }
        public string seat_types { get; set; }
        public string start_time { get; set; }
        public string station_train_code { get; set; }
        public string to_station { get; set; }
        public string to_station_name { get; set; }
        public string to_station_no { get; set; }
        public string train_date { get; set; }
        public string train_no { get; set; }
        public bool useMasterPool { get; set; }
        public bool useWB10LimitTime { get; set; }
        public bool usingGemfireCache { get; set; }
        public string ypInfoDetail { get; set; }

        public DateTime TrainDate
        {
            get
            {
                DateTime train_date = DateTime.Now;
                if (!string.IsNullOrEmpty(this.train_date) && this.train_date.Length == 8)
                {
                    int year = int.Parse(this.train_date.Substring(0, 4));
                    int month = int.Parse(this.train_date.Substring(4, 2));
                    int day = int.Parse(this.train_date.Substring(6, 2));
                    train_date = new DateTime(year, month, day);
                }
                return train_date;
            }
        }
    }
}
