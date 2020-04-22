using AppFVCShared.Model;
using AppFVCShared.WebService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AppFVCShared.WebRequest
{
    public class TermsMedicalGuidanceWr
    {
        protected SuccessfulAnswer ObjSuccessfulAnswer;
        public SuccessfulAnswer GetSuccessfulAnswer()
        {
            return ObjSuccessfulAnswer;
        }

        public TermsMedicalGuidance GetJsonData(string DDD)
        {
            var result = TermsMedicalGuidanceGet(DDD);
            if (result.Result == null)
            {
                ObjSuccessfulAnswer = GetSuccessfulAnswer();
                if (ObjSuccessfulAnswer.Message == "The remote server returned an error: (404) Not Found.")
                {
                    result = TermsMedicalGuidanceGet("00");
                }
            }
            return result.Result;
        }

        public async Task<TermsMedicalGuidance> TermsMedicalGuidanceGet(string DDD)
        {
            var httpWebRequest = System.Net.WebRequest.CreateHttp(Configuration.UrlBaseGit + DDD + "/" + "TermsMedicalGuidance.json");
            httpWebRequest.Method = "GET";
            httpWebRequest.UserAgent = "RequisicaoWebDemo";
            try
            {
                using (var response = httpWebRequest.GetResponse())
                {
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    object objResponse = reader.ReadToEnd();
                    var deserializeObject = JsonConvert.DeserializeObject<TermsMedicalGuidance>(objResponse.ToString());
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
