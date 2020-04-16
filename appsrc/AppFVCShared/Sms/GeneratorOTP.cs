//
// GeneratorOTP.cs: Assignments.
//
// Author:
//      Alessandro de Oliveira Binhara (binhara@azuris.com.br)
//
// Dual licensed under the terms of the MIT or GNU GPL
//
// Copyright 2019-2020 Azuris Mobile & Cloud System
//



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
            var characters = "1234567890";
            var otp = string.Empty;
            for (var i = 0; i < NumberOfChar; i++)
            {
                string character;
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
