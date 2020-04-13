using AppFVCShared.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using AppFVCShared.WebService;

namespace AppFVCShared.Teste
{
    public class NewsWr
    {
        public List<News> GetJsonData(string DDD, string status)
        {
            var httpWebRequest = WebRequest.CreateHttp(Configuration.UrlBaseGit + DDD + "/" + status + ".json");
            httpWebRequest.Method = "GET";
            httpWebRequest.UserAgent = "RequisicaoWebDemo";
            try
            {
                using (var response = httpWebRequest.GetResponse())
                {
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    object objResponse = reader.ReadToEnd();

                    var deserializeObject = JsonConvert.DeserializeObject<List<News>>(objResponse.ToString());

                    stream.Close();
                    response.Close();
                    return deserializeObject;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                return null;
            }
        }
    }

}
