using AppFVCShared.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppFVCShared.WebService
{
    public class ClientSms
    {
        HttpClient client = new HttpClient();
        public async Task<HttpResponseMessage> GetData(Phone s)
        {
            var serializedItem = JsonConvert.SerializeObject(s);

            var result = await client.PostAsync("http://18.229.170.46/assinou/public/api/cuidamane/sendSMS", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return result;
        }


        public async Task<HttpResponseMessage> AddAsync(Phone s)
        {
            HttpClient client = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(s), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://18.229.170.46/assinou/public/api/cuidamane/sendSMS", content);

            return response;
        }
    }
  
}
