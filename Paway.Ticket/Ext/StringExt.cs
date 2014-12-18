using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paway.Ticket
{
    public class StringExt
    {
        public const char D_QUOTE = '\"';
        public const char S_QUOTE = '\'';
        /// <summary>
        /// 在文档中查找 js 变量的值
        /// </summary>
        /// <param name="content">文档内容</param>
        /// <param name="key">变量名称</param>
        /// <returns></returns>
        public static string FindJsValue(string content, string key)
        {
            string result = string.Empty;
            int query_index = content.IndexOf(key);
            if (query_index != -1)
            {
                result = content.Substring(query_index, content.Length - query_index);
                query_index = result.IndexOf(";");
                result = result.Substring(0, query_index).Replace(" ", string.Empty);
                result = result.Replace(key + "=", string.Empty);
                char first = result.First();
                char last = result.Last();
                if (first == D_QUOTE || first == S_QUOTE)
                {
                    result = result.Remove(0, 1);
                    if (last == D_QUOTE || first == S_QUOTE)
                    {
                        result = result.Remove(result.Length - 1, 1);
                    }
                }
            }
            return result;
        }
    }
}
