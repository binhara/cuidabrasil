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
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        internal object Validator;
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            var str = value as string;
            if (value == null || str == "")
            {
                ValidationMessage = "Informação inválida";
                return false;
            }
            return !string.IsNullOrWhiteSpace(str);
        }
    }
}
