using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Paway.Ticket.Package.Data
{
    [Serializable]
    public class PassengerData
    {
        public bool isExist { get; set; }
        public string exMsg { get; set; }
        public string[] two_isOpenClick { get; set; }
        public string[] other_isOpenClick { get; set; }
        public object[] normal_passengers { get; set; }
        public string[] dj_passengers { get; set; }

        public PassengerDetail[] Normal_Passengers
        {
            get
            {
                PassengerDetail[] array = null;
                if (this.normal_passengers != null)
                {
                    array = new PassengerDetail[this.normal_passengers.Length];
                    for (int i = 0; i < this.normal_passengers.Length; i++)
                    {
                        string temp = this.normal_passengers[i].ToString();
                        array[i] = JsonConvert.DeserializeObject<PassengerDetail>(temp);
                    }
                }
                return array;
            }
        }
    }
}
