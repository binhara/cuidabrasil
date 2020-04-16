//
// Journal.cs: Assignments.
//
// Author:
//      Alessandro de Oliveira Binhara (binhara@azuris.com.br)
//
//
// Dual licensed under the terms of the MIT or GNU GPL
//
// Copyright 2019-2020 Azuris Mobile & Cloud System
//
using AppFVCShared.Interfaces;

namespace AppFVCShared.Validators
{
    public class CellphoneValidator<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            var r = value as string;
            if (value == null || r == "")
            {
                ValidationMessage = "Número digitado não é válido";
                return false;
            }

            var str = value as string;

            return ValidateCellphone(str);
        }

        private bool ValidateCellphone(string tfNumberSignup)
        {
            if (tfNumberSignup.Length >= 15)
                return true;

            ValidationMessage = "Número inválido.";
            return false;
        }


    }
}
