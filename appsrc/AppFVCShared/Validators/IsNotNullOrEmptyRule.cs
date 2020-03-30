using AppFVCShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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
