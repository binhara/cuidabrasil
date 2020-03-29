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
            var r = value as string;

            if (value == null || r == "")
            {
                ValidationMessage = "Digite o nome e o sobrenome de quem receberá a indicação!";
                return false;
            }

            var str = value as string;

            return validateName(str);
        }

        private bool validateMoreThanOneWord(string Frase)
        {
            char[] delimiters = new char[] { ' ', '\r', '\n' };
            int palavras = Frase.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
            if (palavras < 2)
            {
                return true;
            }
            return false;
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
            if (validateMoreThanOneWord(tfNameSignup))
            {
                this.ValidationMessage = "Digite o sobrenome";
                return false;
            }



            /*for ( int x = 0; x < tfNameSignup.Length; x++ )
            {
                if (tfNameSignup[x] >= 'a' || tfNameSignup[x] <= 'z')
                {
                    return true;
                }
                else if (tfNameSignup[x] >= 'A' || tfNameSignup[x] <= 'Z')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }*/

            return true;
        }
    }
}

