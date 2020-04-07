using AppFVCShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AppFVCShared.Validators
{
    public class AddressValidator<T> : IValidationRule<T>
    {

        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            var str = value as string;

            if (!validateWord(str))
            {
                //ValidationMessage = "Endereço inválido.";
                return false;
            }

            return true;
        }

        private bool validateWord(string tfWordSignup)
        {
            
            bool onlyLettersandNumbers = Regex.IsMatch(tfWordSignup, (@"[^a-zA-ZáéíóúàèìòùâêîôûãõçÁÉÍÓÚÀÈÌÒÙÂÊÎÔÛÃÕÇ0-9 ]"));
            if (onlyLettersandNumbers)
            {
                return false;
            }
            return true;
        }
    }
}

