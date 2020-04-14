using AppFVCShared.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using AppFVCShared.WebService;
using System.Threading.Tasks;
using System.Net.Http;
using FCVLibWS;

namespace AppFVCShared.Teste
{
    public class NewsWr
    {

        protected SuccessfulAnswer ObjSuccessfulAnswer;
        public SuccessfulAnswer GetSuccessfulAnswer()
        {
            return ObjSuccessfulAnswer;
        }

        public StatusInformation GetJsonData(string DDD, string status)
        {


            var result = StatusInformationGet(DDD, status);
            if (result.Result == null)
            {
                var answer = GetSuccessfulAnswer();
                if (answer.Message == "The remote server returned an error: (404) Not Found.")
                {
                    result = StatusInformationGet("00", status);
                }
            }
            //Console.WriteLine("Error:" + e.Message);
            //return null;

            return result.Result;
        }

        public async Task<StatusInformation> StatusInformationGet(string DDD, string status)
        {
            HttpWebRequest httpWebRequest = WebRequest.CreateHttp(Configuration.UrlBaseGit + DDD + "/" + status + ".json");
            httpWebRequest.Method = "GET";
            httpWebRequest.UserAgent = "RequisicaoWebDemo";
            try
            {
                using (var response = httpWebRequest.GetResponse())
                {
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    object objResponse = reader.ReadToEnd();

                    var deserializeObject = JsonConvert.DeserializeObject<StatusInformation>(objResponse.ToString());

                    stream.Close();
                    response.Close();
                    return deserializeObject;
                }
            }
            catch (Exception e)
            {
                ObjSuccessfulAnswer = new SuccessfulAnswer() { TitleMessage = "Ops, erro no cadastro!", Message = e.Message, Success = false };
                return null;
            }
        }
    }

}
