using System;
using System.Threading.Tasks;
using AppFVCShared.Model;
using AppFVCShared.Sms;
using AppFVCShared.WebService;
using FCVLibWS;
using Xunit;

namespace XUnitTestFvcWS
{
    public class UnitTestContactWs

    {

        private Client objClient;
        private ContactWs contactWs;

        public UnitTestContactWs()
        {
            objClient = new Client(Configuration.UrlBase);
            contactWs = new ContactWs(objClient);
        }

        [Fact]
        public async Task TestContatoWsAsync()
        {
            var result = await contactWs.Contacts();

            Assert.NotNull(result);
        }


        [Fact]
        public async Task TestContatoWsAsyncData()
        {
            var result = await contactWs.Contacts();
            Assert.True(result.Count > 0);
        }



        [Fact]
        public async Task TestContatoWsAsyncJournalDataContent()
        {
            var result = await contactWs.Contacts();
            Assert.Contains(result[0].Journal[0].Status, "UNLOCKED");
        }

        [Fact]
        public async Task TestContatoWsAsyncRegisterContact()
        {
            User user = new User();
            var gen = new GeneratorOtp() { NumberOfChar = 8 };
            gen.GenerateOtp();
            user.Id = gen.SmsCode;
            user.Age = 60;
            user.Name = "teste " + user.Id;
            user.DddPhoneNumber = user.Id;
            user.AcceptTerms = true;
            user.CreateRecord = DateTime.Now;


            var result = await contactWs.RegisterContact(user);
            Assert.NotNull(result);



        }

        [Fact]
        public async Task TestContatoWsAsyncRegisterContactData ()
        {
            User user = new User();
            var gen = new GeneratorOtp(){ NumberOfChar = 8};
            gen.GenerateOtp();
            user.Id = gen.SmsCode;
            user.Age = 60;
            user.Name = "teste "+ user.Id;
            user.DddPhoneNumber = user.Id;
            user.AcceptTerms = true;
            user.CreateRecord = DateTime.Now;


            var result = await contactWs.RegisterContact(user);
            Assert.NotNull(result);

            Assert.Contains(result.Age.ToString(), user.Age.ToString());
            Assert.Contains(result.Name, user.Name);
            Assert.Contains(result.Phone, user.DddPhoneNumber);
            Assert.Contains(result.Id, user.Id);



        }







    }
}