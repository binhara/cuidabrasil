using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppFVCShared.WebService.Interface;
using FCVLibWS;
using Newtonsoft.Json;

namespace AppFVCShared.WebService
{
    public class ContactWs : BaseWs, IErro
    {
        public ContactWs(Client objClient) : base( objClient)
        {

        }

        public SuccessfulAnswer GetSuccessfulAnswer()
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<Contact>> Contacts()
        {
            try
            {
                var result = await ObjClient.PhonebookContactsGetAsync(new CancellationToken());
                return result;
            }
            catch (HttpRequestException ex)
            {
                ObjSuccessfulAnswer = new SuccessfulAnswer() { TitleMessage = "Ops, erro no cadastro!", Message = ex.Message, Success = false };
                return null;
            }
            catch (SwaggerException ex)
            {
                ObjSuccessfulAnswer = new SuccessfulAnswer() { TitleMessage = "Ops, erro no cadastro!", Message = JsonConvert.DeserializeObject<SuccessfulAnswer>(ex.Response).Message, Success = false };
                return null;
            }
        }
    }
}
