using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paway.Ticket.Package;
using System.Collections;
using Newtonsoft.Json;
using Paway.Ticket.Package.Data;

namespace Paway.Ticket.Http
{
    public sealed class TrainTicket
    {
        public static QueryTrainData[] Query(string fromStation, string toStation, string date)
        {
            QueryTrainData[] data = null;
            ResponseQuery response = null;
            RequestPackage request = new RequestPackage();
            request.Method = EHttpMethod.Get.ToString();
            request.RefererURL = "/otn/leftTicket/init";
            request.RequestURL = "/otn/leftTicket/query";
            request.Params.Add("leftTicketDTO.train_date", date);
            request.Params.Add("leftTicketDTO.from_station", fromStation);
            request.Params.Add("leftTicketDTO.to_station", toStation);
            request.Params.Add("purpose_codes", "ADULT");
        Label_001:
            ArrayList list = HttpContext.Send(request);
            if (list.Count == 2)
            {
                string jsonString = Encoding.UTF8.GetString(list[1] as byte[]);
                response = JsonConvert.DeserializeObject<ResponseQuery>(jsonString);
                if (response.status)
                {
                    data = response.Data;
                }
                else if (response.messages != null && response.messages.Length > 0)
                {
                    throw new Exception(response.messages[0]);
                }
                else if (!string.IsNullOrEmpty(response.c_url))
                {
                    request.RequestURL = "/otn/" + response.c_url;
                    goto Label_001;
                }
            }
            else
            {
                Log.Log.Write(list);
            }
            return data;
        }
    }
}
