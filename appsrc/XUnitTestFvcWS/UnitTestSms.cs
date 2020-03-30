using AppFVCShared.Sms;
using Xunit;

namespace XUnitTestFvcWS
{
    public class UnitTestSms

    {
        [Fact]
        public void TestOTP()
        {
            var otp = new GeneratorOtp();
            otp.GenerateOtp();
            Assert.Equal(expected: otp.SmsCode.Length , 6);
        }

        [Fact]
        public void TestOTPRandom()
        {
            var otp = new GeneratorOtp();
            otp.GenerateOtp();

            var otp1 = new GeneratorOtp();
            otp1.GenerateOtp();
            Assert.True(otp.SmsCode != otp1.SmsCode);
        }
    }
}