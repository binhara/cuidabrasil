using AppFVCShared.Teste;
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
    }
}
