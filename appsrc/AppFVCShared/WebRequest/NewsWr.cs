using AppFVCShared.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppFVCShared.Teste
{
    public class NewsWr
    {
        public List<News> GetJsonData(string DDD, string status)
        {
            var requisicaoWeb = WebRequest.CreateHttp("https://raw.githubusercontent.com/cuidabrasil/testenoticias/master/" + DDD + ".json");
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";
            try
            {
                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(streamDados);
                    object objResponse = reader.ReadToEnd();

                    var noticia = JsonConvert.DeserializeObject<List<News>>(objResponse.ToString());

                    streamDados.Close();
                    resposta.Close();
                    return noticia;
                }

            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }
    }

}
