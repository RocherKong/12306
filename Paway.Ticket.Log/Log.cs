using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.IO;

namespace Paway.Ticket.Log
{
    public static class Log
    {
        public static readonly string PATH = Application.StartupPath + @"\log\{0}.log";
        public static void Write(ArrayList list)
        {
            string directory = Path.GetDirectoryName(PATH);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            string path = string.Format(PATH, DateTime.Now.ToString("yyyyMMddHHmmssms"));
            using (FileStream fs = File.Create(path))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(string.Format(
                        "记录日期：{0}\r\n", 
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                    sw.WriteLine(string.Format("list count：{0}\r\n\r\n", list.Count));
                    for (int i = 0; i < list.Count; i++)
                    {
                        object item = list[i];
                        sw.WriteLine(string.Format("list_{0}\r\n", i + 1));
                        if (item is byte[])
                        {
                            sw.WriteLine(Encoding.Default.GetString(item as byte[]));
                        }
                        else
                        {
                            sw.WriteLine(item.ToString());
                        }
                    }
                }
            }
        }
    }
}
