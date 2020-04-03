using AppFVCShared.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppFVCShared.WebService
{
    public class DataJs
    {
        public string phoneNumber { get; set; }
        public string validationCode { get; set; }
    }

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
            client=  new HttpClient();
        }

        private  HttpClient client ;
        public async Task<HttpResponseMessage> GetData(Phone s)
        {
            var serializedItem = JsonConvert.SerializeObject(s);

            var result = await client.PostAsync("http://18.229.170.46/assinou/public/api/cuidamane/sendSMS", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return result;
        }

        public async Task<string> SendSMSAsync(string  phone, string cod)
        {
            var url = "http://18.229.170.46/assinou/public/api/cuidamane/sendSMS";

            DataJs data = new DataJs {phoneNumber = phone, validationCode = cod};

            StringContent content = new StringContent( JsonConvert.SerializeObject(data)
            ,Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<string>(
                    await response.Content.ReadAsStringAsync());
            }

            return null;
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
