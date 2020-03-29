using System;


namespace AppFVCShared.Sms
{
    public class GeneratorOtp
    {
        public GeneratorOtp()
        {
            NumberOfChar = 6; //default value 
        }

        public int NumberOfChar { get; set; }
        public string SmsCode { get; set; }
        public void GenerateOtp()
        {
            //string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            //string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";
            string characters = numbers;

            //if (rbType.SelectedItem.Value == "1")
            //{
            //    characters += alphabets + small_alphabets + numbers;
            //}
            int length = NumberOfChar;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            SmsCode = otp;
        }

    }
}
