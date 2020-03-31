using System.Threading.Tasks;
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
            Assert.Contains(result[0].Id , "asdfasfd");
            Assert.Contains(result[0].Name, "asdfasdf");
            Assert.Contains(result[0].Phone, "asdfasfd");
            Assert.Contains(result[0].Age.ToString(), "43");


        }





    }
}