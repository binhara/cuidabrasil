//
// Journal.cs: Assignments.
//
// Author:
//      Alessandro de Oliveira Binhara (binhara@azuris.com.br)
//
//
// Dual licensed under the terms of the MIT or GNU GPL
//
// Copyright 2019-2020 Azuris Mobile & Cloud System
//
using AppFVCShared.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppFVCShared.WebService
{
    public class ClientSms
    {
        // http://18.229.170.46/assinou/public/api/cuidamane/sendSMS
        // http://18.229.170.46/assinou/public/api/cuidamane/sendSMS
        // parâmetros: phoneNumber: 5543999451192 e validationCode: 102030
        // phoneNumber: formato Código do Pais + DDD+ número
        // MEtodo post
        // http://18.229.170.46/assinou/public/api/cuidamane/sendSMS?phoneNumber=5541998003687&validationCode=11111
        
        public ClientSms()
        {
            _client=  new HttpClient();
            _url = Configuration.UrlBaseSms;
        }

        private  HttpClient _client ;
        private string _url;

        public async Task<HttpResponseMessage> GetData(Phone s)
        {
            var serializedItem = JsonConvert.SerializeObject(s);
            var result = await _client.PostAsync(_url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return result;
        }

        public async Task<string> SendSMSAsync(string  phone, string cod)
        {
            var data = new SmsDataJs {phoneNumber = phone, validationCode = cod};
            var content = new StringContent( JsonConvert.SerializeObject(data),Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_url, content);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<string>(
                    await response.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<HttpResponseMessage> AddAsync(Phone s)
        {
            var client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(s), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_url, content);
            return response;
        }
    }
  
}
