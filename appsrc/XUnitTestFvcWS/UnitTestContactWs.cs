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



    }
}