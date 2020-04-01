using AppFVCShared.Sms;
using AppFVCShared.WebService;
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

        [Fact]
        public void TestSendSms()
        {
            ClientSms cSms = new ClientSms();
            var result = cSms.SendSMSAsync("5548988319395", "111111");
            result = cSms.SendSMSAsync("5541998003687", "11111");

            Assert.NotNull(result);

        }

    }
    }