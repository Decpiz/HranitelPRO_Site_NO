using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace PR_21._106_HranitelPRO_Practic
{
    public class ValidationPassword
    {
        public bool Validation(string password)
        {
            var input = password;

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Поле с паролем пустое", "Ошибка");
                return false;
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,18}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbol = new Regex(@"[!@#$%^&*()_+=/\}{[;:<>|.?]");

            if (!hasLowerChar.IsMatch(input))
            {
                MessageBox.Show("Пароль должен содержать хотябы одну строчную (abc) букву!", "Ошибка");
                return false;
            }

            else if (!hasUpperChar.IsMatch(input))
            {
                MessageBox.Show("Пароль должен содержать хотябы одну заглавную (ABC) букву!", "Ошибка");
                return false;
            }

            else if (!hasMiniMaxChars.IsMatch(input))
            {
                MessageBox.Show("Пароль должен быть длиннее 8 символов!", "Ошибка");
                return false;
            }

            else if (!hasNumber.IsMatch(input))
            {
                MessageBox.Show("Пароль должен содержать хотябы одно числовое (123) значение!", "Ошибка");
                return false;
            }

            else if (!hasSymbol.IsMatch(input))
            {
                MessageBox.Show("Пароль должен содержать хотябы один спец. (!@#) символ!", "Ошибка");
                return false;
            }
            else { return true; }
        }
    }
}
