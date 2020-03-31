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
            Assert.Single(result);
        }


        [Fact]
        public async Task TestContatoWsAsyncDataContent()
        {
            var result = await contactWs.Contacts();
            Assert.Contains(result[0].Id, "asdfasfd");
            Assert.Contains(result[0].Name, "asdfasdf");
            Assert.Contains(result[0].Phone, "asdfasfd");
            Assert.Contains(result[0].Age.ToString(), "43");


        }

        //[Fact]
        //public async Task TestContatoWsAsyncJournalDataContent()
        //{
        //    var result = await contactWs.Contacts();
        //    //result[0].Journal[0].
        //    //Assert.Contains(result[0].Journal[0]., "asdfasfd");
        //    //Assert.Contains(result[0].Journal[0]., "asdfasfd");
        //    //Assert.Contains(result[0].Journal[0].Age, "asdfasfd");
        //    //Assert.Contains(result[0].Journal[0].Age, "asdfasfd");
        //}

        [Fact]
        public async Task TestContatoWsAsyncRegisterContact()
        {
            User user = new User();
            var gen = new GeneratorOtp(){ NumberOfChar = 4};
            user.Id = gen.SmsCode;
            user.Age = 43;
            user.Name = "teste1";
            user.DddPhoneNumber = "41998003687";
            user.AcceptTerms = true;
            user.CreateRecord = DateTime.Now;
     

            var result = await contactWs.RegisterContact(user);
            Assert.NotNull(result);

        }

        [Fact]
        public async Task TestContatoWsAsyncRegisterContactData ()
        {
            User user = new User();
            var gen = new GeneratorOtp() { NumberOfChar = 4 };
            gen.GenerateOtp();

            user.Id = gen.SmsCode;
            user.Age = 60;
            user.Name = "teste2";
            user.DddPhoneNumber = "459999999999";
            user.AcceptTerms = true;
            user.CreateRecord = DateTime.Now;


            var result = await contactWs.RegisterContact(user);
            Assert.NotNull(result);

            Assert.Contains(result.Age.ToString(), user.Age.ToString());
            Assert.Contains(result.Age.ToString(), user.Age.ToString());
            Assert.Contains(result.Name, user.Name);
            Assert.Contains(result.Phone, user.DddPhoneNumber);



        }



    }
}