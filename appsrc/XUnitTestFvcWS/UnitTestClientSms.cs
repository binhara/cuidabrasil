using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AppFVCShared.Model;
using AppFVCShared.WebService;

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
            ClientSms sms = new ClientSms();
            Phone phone = new Phone();
            phone.phoneNumber = "5541996668442";
            phone.validationCode = "102030";

            var result = await sms.GetData(phone);
            Assert.True(result.IsSuccessStatusCode);
        }
    }
}
