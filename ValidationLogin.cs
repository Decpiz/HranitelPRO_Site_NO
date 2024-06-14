using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace PR_21._106_HranitelPRO_Practic
{
    public class ValidationLogin
    {
        public bool Validation(string login)
        {
            var input = login;

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Поле с логином пустое!", "Ошибка");
                return false;
            }

            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasMiniMaxChars = new Regex(@".{4,10}");

            if (!hasLowerChar.IsMatch(input))
            {
                MessageBox.Show("Логин должен содержать хотябы одну строчную (abc) букву!", "Ошибка");
                return false;
            }

            else if (!hasUpperChar.IsMatch(input))
            {
                MessageBox.Show("Логин должен содержать хотябы одну заглавную (ABC) букву!", "Ошибка");
                return false;
            }

            else if (!hasMiniMaxChars.IsMatch(input))
            {
                MessageBox.Show("Логин должен быть длиннее 4 символов!", "Ошибка");
                return false;
            }
            else { return true; }
        }
    }
}
