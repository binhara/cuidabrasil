//
// Journal.cs: Assignments.
//
// Author:
//      Alessandro de Oliveira Binhara (binhara@azuris.com.br)
//      Adriano D'Luca Binhara Gonçalves (adriano@azuris.com.br)
//  	Carol Yasue (carolina_myasue@hotmail.com)
//
//
// Dual licensed under the terms of the MIT or GNU GPL
//
// Copyright 2019-2020 Azuris Mobile & Cloud System
//
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AppFVCShared.Model;
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
            return ObjSuccessfulAnswer;
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
                ObjSuccessfulAnswer = new SuccessfulAnswer() { TitleMessage = "Ops, erro ao listar o cadastro!", Message = ex.Message, Success = false };
                return null;
            }
            catch (SwaggerException ex)
            {
                ObjSuccessfulAnswer = new SuccessfulAnswer() { TitleMessage = "Ops, erro ao lista o  cadastro!", Message = JsonConvert.DeserializeObject<SuccessfulAnswer>(ex.Response).Message, Success = false };
                return null;
            }
        }

        public async Task<Contact> RegisterContact( User u)
        {
            FCVLibWS.Contact body = new Contact();
            body.Name = u.Name;
            body.Age = u.Age;
            body.Phone =  u.DddPhoneNumber;
            body.Id = u.Id;
            

            try
            {
                var result = await ObjClient.PhonebookContactsPostAsync(body);
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
