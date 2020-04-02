using AppFVCShared.Interfaces;
using System;
using System.Text.RegularExpressions;

namespace AppFVCShared.Validators
{
    public class NameValidator<T> : IValidationRule<T>
    {

        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            //var r = value as string;

            //if (value == null || r == "")
            //{
            //    ValidationMessage = "Digite o nome e o sobrenome de quem receberá a indicação!";
            //    return false;
            //}

            var str = value as string;

            if (!validateName(str))
            {
                ValidationMessage = "Nome inválido!";
                return false;
            }

            return true;
        }

        private bool validateName(string tfNameSignup)
        {
            //Não esta funcionando
            //TODO
            bool onlyLetters = Regex.IsMatch(tfNameSignup, (@"[^a-zA-ZáéíóúàèìòùâêîôûãõçÁÉÍÓÚÀÈÌÒÙÂÊÎÔÛÃÕÇ ]"));
            if (onlyLetters)
            {
                return false;
            }
            tfNameSignup = tfNameSignup.TrimStart();
            tfNameSignup = tfNameSignup.TrimEnd();

            String str = tfNameSignup;
            string[] spearator = { " " };
            String[] strlist = str.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
            var hasInvalidName = true;
            //List<string> partes = new List<string>();
            foreach (String s in strlist)
            {
                if (hasInvalidName)
                {
                    if (s.Length <= 1)
                    {
                        hasInvalidName = false;
                    }
                }
            }
            //if (tfNameSignup.Length <= 1)
            //{
            //    return false;
            //}

            return hasInvalidName;
        }
    }
}

