using AppFVCShared.Model;
using AppFVCShared.WebService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppFVCShared.WebRequest
{
    public class FirstRunWr
    {
        public FirstRun GetJsonFirstRunData(string DDD)
        {
            var httpWebRequest = System.Net.WebRequest.CreateHttp(Configuration.UrlBaseGit + DDD + "/" + "FirstRun.json");
            httpWebRequest.Method = "GET";
            httpWebRequest.UserAgent = "RequisicaoWebDemo";
            try
            {
                using (var response = httpWebRequest.GetResponse())
                {
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    object objResponse = reader.ReadToEnd();

                    var deserializeObject = JsonConvert.DeserializeObject<FirstRun>(objResponse.ToString());

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
