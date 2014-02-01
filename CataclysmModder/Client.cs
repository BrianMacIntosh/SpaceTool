using System.IO;
using System.Net;
using System.Text;

namespace CataclysmModder
{
    static class Client
    {
        public static string userAgent = "SpaceTool";

        public static Stream Post(HttpWebRequest request, string data)
        {
            request.UserAgent = userAgent;
            byte[] datainflate = Encoding.UTF8.GetBytes(data);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = datainflate.Length;
            Stream newStream = request.GetRequestStream();
            newStream.Write(datainflate, 0, datainflate.Length);
            newStream.Close();
            return request.GetResponse().GetResponseStream();
        }

        public static string UrlEncode(string str)
        {
            StringBuilder response = new StringBuilder();
            for (int c = 0; c < str.Length; c++)
            {
                if ((str[c] < 'a' || str[c] > 'z') &&
                    (str[c] < 'A' || str[c] > 'Z') &&
                    (str[c] < '0' || str[c] > '9'))
                    response.Append("%" + ((int)str[c]).ToString("X2"));
                else
                    response.Append(str[c]);
            }
            return response.ToString();
        }
    }
}
