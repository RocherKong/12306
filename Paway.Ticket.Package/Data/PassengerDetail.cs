using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paway.Ticket.Package.Data
{
    [Serializable]
    public class PassengerDetail
    {
        public string code { get; set; }
        public string passenger_name { get; set; }
        public string sex_code { get; set; }
        public string sex_name { get; set; }
        public string born_date { get; set; }
        public string country_code { get; set; }
        public string passenger_id_type_code { get; set; }
        public string passenger_id_type_name { get; set; }
        public string passenger_id_no { get; set; }
        public string passenger_type { get; set; }
        public string passenger_flag { get; set; }
        public string passenger_type_name { get; set; }
        public string mobile_no { get; set; }
        public string phone_no { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string postalcode { get; set; }
        public string first_letter { get; set; }
        public string recordCount { get; set; }
        public string total_times { get; set; }
        public string index_id { get; set; }

        #region Override Methods
        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}", passenger_name, sex_name, passenger_id_type_name);
        }
        #endregion
    }
}
