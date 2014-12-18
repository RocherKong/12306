using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Paway.Ticket.Package
{
    [Serializable]
    public class ResponseBase
    {
        //"data":[{"queryLeftNewDTO":{"train_no":"640000K26808","station_train_code":"K268","start_station_telecode":"HHQ","start_station_name":"怀化","end_station_telecode":"BJP","end_station_name":"北京","from_station_telecode":"JIQ","from_station_name":"吉首","to_station_telecode":"BJP","to_station_name":"北京","start_time":"10:51","arrive_time":"12:12","day_difference":"1","train_class_name":"","lishi":"25:21","canWebBuy":"N","lishiValue":"1521","yp_info":"1020603000405820000010206000003036500000","control_train_day":"20991231","start_train_date":"20141006","seat_feature":"W3431333","yp_ex":"10401030","train_seat_feature":"3","seat_types":"1413","location_code":"Q7","from_station_no":"03","to_station_no":"22","control_day":19,"sale_time":"1800","is_support_card":"0","gg_num":"--","gr_num":"--","qt_num":"--","rw_num":"*","rz_num":"--","tz_num":"--","wz_num":"*","yb_num":"--","yw_num":"*","yz_num":"*","ze_num":"--","zy_num":"--","swz_num":"--"},"secretStr":"","buttonTextInfo":"18点起售"}],
        private string _validateMessagesShowId;
        public string validateMessagesShowId
        {
            get { return this._validateMessagesShowId; }
            set { this._validateMessagesShowId = value; }
        }

        private bool _status;
        public bool status
        {
            get { return this._status; }
            set { this._status = value; }
        }

        private int _httpstatus;
        public int httpstatus
        {
            get { return this._httpstatus; }
            set { this._httpstatus = value; }
        }

        private object _data;
        public virtual object data
        {
            get { return this._data; }
            set { this._data = value; }
        }

        private string[] _messages;
        public string[] messages
        {
            get { return this._messages; }
            set { this._messages = value; }
        }

        private object _validateMessages;
        public object validateMessages
        {
            get { return this._validateMessages; }
            set { this._validateMessages = value; }
        }

        #region Override Methods

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion
    }
}
