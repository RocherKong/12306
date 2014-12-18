using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Paway.Ticket.Package.Data
{
    [Serializable]
    public class QueryTrainData
    {
        //"data":[{"queryLeftNewDTO":{"train_no":"640000K26808","station_train_code":"K268","start_station_telecode":"HHQ","start_station_name":"怀化","end_station_telecode":"BJP","end_station_name":"北京","from_station_telecode":"JIQ","from_station_name":"吉首","to_station_telecode":"BJP","to_station_name":"北京","start_time":"10:51","arrive_time":"12:12","day_difference":"1","train_class_name":"","lishi":"25:21","canWebBuy":"N","lishiValue":"1521","yp_info":"1020603000405820000010206000003036500000","control_train_day":"20991231","start_train_date":"20141006","seat_feature":"W3431333","yp_ex":"10401030","train_seat_feature":"3","seat_types":"1413","location_code":"Q7","from_station_no":"03","to_station_no":"22","control_day":19,"sale_time":"1800","is_support_card":"0","gg_num":"--","gr_num":"--","qt_num":"--","rw_num":"*","rz_num":"--","tz_num":"--","wz_num":"*","yb_num":"--","yw_num":"*","yz_num":"*","ze_num":"--","zy_num":"--","swz_num":"--"},"secretStr":"","buttonTextInfo":"18点起售"}],
        public object queryLeftNewDTO { get; set; }
        public string secretStr { get; set; }
        public string buttonTextInfo { get; set; }

        public DetailData QueryLeftNewDTO
        {
            get
            {
                DetailData data = null;
                if (this.queryLeftNewDTO != null && this.queryLeftNewDTO.ToString() != string.Empty)
                {
                    data = JsonConvert.DeserializeObject<DetailData>(this.queryLeftNewDTO.ToString());
                }
                return data;
            }
        }

        [Serializable]
        public class DetailData
        {
            /// <summary>
            /// 车次编号
            /// </summary>
            public string train_no { get; set; }
            /// <summary>
            /// 车次
            /// </summary>
            public string station_train_code { get; set; }
            public string start_station_telecode { get; set; }
            public string start_station_name { get; set; }
            public string end_station_telecode { get; set; }
            public string end_station_name { get; set; }
            public string from_station_telecode { get; set; }
            public string from_station_name { get; set; }
            public string to_station_telecode { get; set; }
            public string to_station_name { get; set; }
            public string start_time { get; set; }
            public string arrive_time { get; set; }
            public string day_difference { get; set; }
            public string train_class_name { get; set; }
            /// <summary>
            /// 历时
            /// </summary>
            public string lishi { get; set; }
            public string canWebBuy { get; set; }
            public string lishiValue { get; set; }
            public string yp_info { get; set; }
            public string control_train_day { get; set; }
            public string start_train_date { get; set; }
            public DateTime StartTrainDate
            {
                get
                {
                    DateTime train_date = DateTime.Now;
                    if (!string.IsNullOrEmpty(this.start_train_date) && this.start_train_date.Length == 8)
                    {
                        int year = int.Parse(this.start_train_date.Substring(0, 4));
                        int month = int.Parse(this.start_train_date.Substring(4, 2));
                        int day = int.Parse(this.start_train_date.Substring(6, 2));
                        train_date = new DateTime(year, month, day);
                    }
                    return train_date;
                }
            }
            public string seat_feature { get; set; }
            public string yp_ex { get; set; }
            public string train_seat_feature { get; set; }
            public string seat_types { get; set; }
            public string location_code { get; set; }
            public string from_station_no { get; set; }
            public string to_station_no { get; set; }
            public int control_day { get; set; }
            public string sale_time { get; set; }
            public string is_support_card { get; set; }

            public string gg_num { get; set; }
            /// <summary>
            /// 高级软卧
            /// </summary>
            public string gr_num { get; set; }
            /// <summary>
            /// 其它
            /// </summary>
            public string qt_num { get; set; }
            /// <summary>
            /// 软卧
            /// </summary>
            public string rw_num { get; set; }
            /// <summary>
            /// 软座
            /// </summary>
            public string rz_num { get; set; }
            /// <summary>
            /// 特等座
            /// </summary>
            public string tz_num { get; set; }
            /// <summary>
            /// 无座
            /// </summary>
            public string wz_num { get; set; }
            public string yb_num { get; set; }
            /// <summary>
            /// 硬卧
            /// </summary>
            public string yw_num { get; set; }
            /// <summary>
            /// 硬座
            /// </summary>
            public string yz_num { get; set; }
            /// <summary>
            /// 二等座
            /// </summary>
            public string ze_num { get; set; }
            /// <summary>
            /// 一等座
            /// </summary>
            public string zy_num { get; set; }
            /// <summary>
            /// 商务座
            /// </summary>
            public string swz_num { get; set; }
        }
    }
}
