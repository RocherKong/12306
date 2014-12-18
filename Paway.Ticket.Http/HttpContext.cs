using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;
using System.IO;
using System.Threading;
using Paway.Ticket.Package;

namespace Paway.Ticket.Http
{
    public static class HttpContext
    {
        #region 变量
        private const char PARAM_SIGN = '&';
        public static string HOST_URL = "https://kyfw.12306.cn";
        public static CookieContainer Cookie = new CookieContainer();
        #endregion

        #region 方法
        public static ArrayList GetHtmlData(RequestPackage package)
        {
            HttpWebRequest request;
            HttpWebResponse response;
            ArrayList list = new ArrayList();
            request = WebRequest.Create(HOST_URL + package.RequestURL) as HttpWebRequest;
            request.Referer = HOST_URL + package.RefererURL;
            request.Method = EHttpMethod.Get.ToString();
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; MALCJS; rv:11.0) like Gecko";
            request.KeepAlive = true;
            request.CookieContainer = Cookie;
            try
            {
                //获取服务器返回的资源
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        Cookie.Add(response.Cookies);
                        //保存Cookies
                        list.Add(Cookie);
                        list.Add(reader.ReadToEnd());
                        list.Add(Guid.NewGuid().ToString());//图片名
                    }
                }
            }
            catch (WebException ex)
            {
                list.Clear();
                list.Add("发生异常/n/r");
                WebResponse wr = ex.Response;
              
                using (Stream st = wr.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(st, System.Text.Encoding.Default))
                    {
                        list.Add(sr.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                list.Add("5");
                list.Add("发生异常：" + ex.Message);
            }
            return list;
        }
        public static Stream DownloadCode(RequestPackage package)
        {
            Stream stream = null;
            string url = HOST_URL + package.RequestURL;
            if (package.Params.Count > 0)
                url = string.Format("{0}?{1}", url, JoinParams(package.Params));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //属性配置
            request.AllowWriteStreamBuffering = true;
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;
            request.MaximumResponseHeadersLength = -1;
            request.Accept = "image/png, image/svg+xml, image/*;q=0.8, */*;q=0.5";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; MALCJS; rv:11.0) like Gecko";
            request.Method = EHttpMethod.Get.ToString();
            request.Headers.Add("Accept-Language", "zh-Hans-CN,zh-Hans;q=0.5");
            request.Headers.Add("Accept-Encoding", "gzip,deflate");
            request.KeepAlive = true;
            request.CookieContainer = Cookie;
            try
            {
                //获取服务器返回的资源
                using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream st = webResponse.GetResponseStream())
                    {
                        stream = new MemoryStream();
                        while (true)
                        {
                            int data = st.ReadByte();
                            if (data == -1)
                                break;
                            stream.WriteByte((byte)data);
                        }
                    }
                }
            }
            catch (WebException)
            {
                stream = null;
            }
            catch (Exception)
            {
                stream = null;
            }
            return stream;
        }
        public static ArrayList Send(RequestPackage package)
        {
            ArrayList list = new ArrayList();
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            string url = HOST_URL + package.RequestURL;
            string method = package.Method.ToString().ToLower();
            if (package.Method.ToLower() == EHttpMethod.Get.ToString().ToLower())
            {
                if (package.Params.Count > 0)
                    url = string.Format("{0}?{1}", url, JoinParams(package.Params));
                request = WebRequest.Create(url) as HttpWebRequest;
                request.Accept = package.Accept;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; MALCJS; rv:11.0) like Gecko";
                request.CookieContainer = Cookie;
                request.Referer = HOST_URL + package.RefererURL;
                request.Method = method;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
                string data = JoinParams(package.Params);
                byte[] b = package.Encoding.GetBytes(data);
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Accept = package.Accept;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; MALCJS; rv:11.0) like Gecko";
                request.CookieContainer = Cookie;
                request.Referer = HOST_URL + package.RefererURL;
                request.Method = method;
                request.ContentLength = b.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(b, 0, b.Length);
                }
            }
            try
            {
                //获取服务器返回的资源
                using (response = request.GetResponse() as HttpWebResponse)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        if (response.Cookies.Count > 0)
                        {
                            Cookie.Add(response.Cookies);
                        }
                        list.Add(Cookie);
                        List<byte> dataList = new List<byte>();
                        while (true)
                        {
                            int data = stream.ReadByte();
                            if (data == -1)
                                break;
                            dataList.Add((byte)data);
                        }
                        list.Add(dataList.ToArray());
                    }
                }
            }
            catch (WebException wex)
            {
                WebResponse wr = wex.Response;
                using (Stream st = wr.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(st, System.Text.Encoding.UTF8))
                    {
                        list.Add(sr.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                list.Add("发生异常/n/r" + ex.Message);
            }
            return list;
        }
        #endregion

        #region Private Methods
        private static string JoinParams(Dictionary<string, string> param)
        {
            StringBuilder data = new StringBuilder();
            if (param != null && param.Count > 0)
            {
                string lastKey = param.Keys.Last();
                foreach (string key in param.Keys)
                {
                    string value = param[key];
                    data.AppendFormat("{0}={1}", key, value);
                    if (key != lastKey)
                        data.Append(PARAM_SIGN);
                }
            }
            return data.ToString();
        }
        #endregion
    }
}
