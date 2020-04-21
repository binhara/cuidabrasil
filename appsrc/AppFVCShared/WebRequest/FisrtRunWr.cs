//
// Journal.cs: Assignments.
//
// Author:
//      Adriano D'Luca Binhara Gonçalves (adriano@azuris.com.br)
//
//
// Dual licensed under the terms of the MIT or GNU GPL
//
// Copyright 2019-2020 Azuris Mobile & Cloud System
//
using AppFVCShared.Model;
using AppFVCShared.WebService;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AppFVCShared.WebRequest
{
    public class FirstRunWr
    {
        protected SuccessfulAnswer ObjSuccessfulAnswer;
        public SuccessfulAnswer GetSuccessfulAnswer()
        {
            return ObjSuccessfulAnswer;
        }

        public FirstRun GetJsonFirstRunData()
        {
            var result = FirstRunInformationGet();
            if (result.Result == null)
            {
                ObjSuccessfulAnswer = GetSuccessfulAnswer();
            }
            return result.Result;
        }

        public async Task<FirstRun> FirstRunInformationGet()
        {
            var httpWebRequest = System.Net.WebRequest.CreateHttp(Configuration.UrlBaseGit + "FirstRun.json");
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
                ObjSuccessfulAnswer = new SuccessfulAnswer() { TitleMessage = "Ops, erro no cadastro!", Message = e.Message, Success = false };
                return null;
            }
        }
    }
   
}
