
using AppFVCShared.Model;
using Xunit;

namespace XUnitTestFvcWS
{
    public class UnitTestModel
    {
        [Fact]
        public void TestUser()
        {
            var user = new User();
            Assert.Equal(user.Comorbidities.Count, 6);
        }
    }
}
