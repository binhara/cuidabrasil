using AppFVCShared.Sms;
using AppFVCShared.WebService;
using FCVLibWS;
using Xunit;

namespace XUnitTestFvcWS
{
    public class UnitTestContactWs

    {
        
        private Client objClient;

        public UnitTestContactWs()
        {
            objClient = new Client(Configuration.UrlBase);

        }

        [Fact]
        public void TestContatoWs()
        {

        }
    }
}