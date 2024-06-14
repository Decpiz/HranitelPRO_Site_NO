using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace PR_21._106_HranitelPRO_Practic
{
    public class ValidationEmail
    {
        public bool Validation(string email)
        {
            var input = email;

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Поле E-mail не заполнено!", "Ошибка");
                return false;
            }

            var hasDotRu = new Regex(@"[.ru, .com]");
            var hasDogAndDot = new Regex(@"[@.]");

            if (!hasDotRu.IsMatch(input))
            {
                MessageBox.Show("Email некорректен", "Ошибка");
                return false;
            }

            if (!hasDogAndDot.IsMatch(input))
            {
                MessageBox.Show("Email некорректен", "Ошибка");
                return false;
            }

            else { return true; }
        }
    }
}
