//
// Journal.cs: Assignments.
//
// Author:
//      Alessandro de Oliveira Binhara (binhara@azuris.com.br)
//      Adriano D'Luca Binhara Gonçalves (adriano@azuris.com.br)
//  	Carol Yasue (carolina_myasue@hotmail.com)
//
//
// Dual licensed under the terms of the MIT or GNU GPL
//
// Copyright 2019-2020 Azuris Mobile & Cloud System
//
using AppFVCShared.Interfaces;
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

