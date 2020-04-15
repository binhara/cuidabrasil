using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AppFVCShared.Model;
using AppFVCShared.WebService;
using FCVLibWS;

namespace XUnitTestFvcWS
{
    public class UnitTestClientSms
    {
        [Fact]
        public async void TestSms()
        {
            ClientSms sms = new ClientSms(); 
            Phone phone = new Phone();
            phone.phoneNumber = "5541996668442";
            phone.validationCode = "102030";

            var result = await sms.AddAsync(phone);
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async void TestSms1()
        {
            Client objClient = new Client(Configuration.UrlBase);
            ContactWs contactWs = new ContactWs(objClient);
            User user = new User();
            user.Age = 30;
            user.Name = "Julieta";
            user.AcceptTerms = true;
            user.CreateRecord = DateTime.Now;
            user.DddPhoneNumber ="41996668442";
            var result = await contactWs.RegisterContact(user);
            
            Assert.NotNull(result);
        }
    }
}
