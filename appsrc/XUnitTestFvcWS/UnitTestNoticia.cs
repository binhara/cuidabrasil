using AppFVCShared.Teste;
using AppFVCShared.WebRequest;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestFvcWS
{
    public class UnitTestNoticia
    {

        [Fact]
        public void TesteGetJson()
        {
            NewsWr news = new NewsWr();
            var result = news.GetJsonData("41", "Unknow");

            Assert.NotNull(result);

        }
       

        [Fact]
        public void TestVerifyData()
        {
            NewsWr news = new NewsWr();
            var result = news.GetJsonData("41", "Recovered");

            Assert.NotNull(result);

            Assert.IsType<int>(result[0].Id);
            Assert.IsType<string>(result[0].Title);
            Assert.IsType<string>(result[0].Content);
            Assert.Contains("http", result[0].Uri);
            Assert.IsType<string>(result[0].PhoneNumber);
        }

        //Test Noticia FirstRun
        [Fact]
        public void TesteGetFirstRunJson()
        {
            FirstRunWr news = new FirstRunWr();
            var result = news.GetJsonFirstRunData("41");

            Assert.NotNull(result);

        }
    }
}
