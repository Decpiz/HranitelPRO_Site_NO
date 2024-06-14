using PR_21._106_HranitelPRO_Practic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PR_21._106_HranitelPRO_Practic
{
    /// <summary>
    /// Логика взаимодействия для wndAuth.xaml
    /// </summary>
    public partial class wndAuth : Window
    {
        HranitelPRO db = HranitelPRO.GetContext();
        Polzovateli polzovatel = new Polzovateli();

        public wndAuth()
        {
            InitializeComponent();
        }

        private void btnGoAuth_Click(object sender, RoutedEventArgs e)
        {
            wndReg reg = new wndReg();
            reg.Show();
            this.Close();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            if (!tbInputPassword.Text.Equals(tbInputPassTwo.Text))
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка");
                tbInputPassTwo.Clear();
                return;
            }

            ValidationPassword validP = new ValidationPassword();
            if (validP.Validation(tbInputPassword.Text) == false)
            { return; }

            if(tbInputPassword.Text.Equals(tbInputLogin.Text))
            {
                MessageBox.Show("Пароль и логин не должны совпадать!", "Ошибка");
            }

            ValidationLogin validL = new ValidationLogin();
            if (validL.Validation(tbInputLogin.Text) == false)
            { return; }

            ValidationEmail validE = new ValidationEmail();
            if(validE.Validation(tbInputEmail.Text) == false)
            { return; }

            polzovatel.Email = tbInputEmail.Text;
            polzovatel.Login = tbInputLogin.Text;
            polzovatel.Parol = tbInputPassword.Text;

            HiddenReg();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            VisibleReg();
        }

        private void btnReg2_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbInputFamilia.Text))
            {
                MessageBox.Show("Поле с фамилией не заполнено", "Ошибка");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbInputImya.Text))
            {
                MessageBox.Show("Поле с именем не заполнено", "Ошибка");
                return;
            }

            if (LowValid(tbInputImya.Text) == false)
            { return; }

            if (LowValid(tbInputFamilia.Text) == false)
            { return; }

            polzovatel.Imya = tbInputImya.Text;
            polzovatel.Familia = tbInputFamilia.Text;

            db.Polzovateli.Add(polzovatel);
            db.SaveChanges();

            wndReg auth = new wndReg();
            auth.Show();
            this.Close();

        }

        public bool LowValid(string str)
        {
            var inp = str;

            var hasCifra = new Regex(@"[0-9]+");
            var hasSymbol = new Regex(@"[!@#$%^&*()_+=/\}{[;:<>|.?]");

            if (hasCifra.IsMatch(str))
            {
                MessageBox.Show("Введены некорректные данные");
                return false;
            }

            if (hasSymbol.IsMatch(str))
            {
                MessageBox.Show("Введены некорректные данные");
                return false;
            }
            else { return true; }
        }

        public void HiddenReg()
        {
            txbEmail.Visibility = Visibility.Hidden;
            txbLogin.Visibility = Visibility.Hidden;
            txbPass.Visibility = Visibility.Hidden;
            txbPass2.Visibility = Visibility.Hidden;

            tbInputEmail.Visibility = Visibility.Hidden;
            tbInputLogin.Visibility = Visibility.Hidden;
            tbInputPassword.Visibility = Visibility.Hidden;
            tbInputPassTwo.Visibility = Visibility.Hidden;

            btnReg.Visibility = Visibility.Hidden;
            btnGoAuth.Visibility = Visibility.Hidden;

            btnReg2.Visibility = Visibility.Visible;
            btnBack.Visibility = Visibility.Visible;

            txbFam.Visibility = Visibility.Visible;
            txbImya.Visibility = Visibility.Visible;

            tbInputFamilia.Visibility = Visibility.Visible;
            tbInputImya.Visibility = Visibility.Visible;
        }

        public void VisibleReg()
        {
            txbEmail.Visibility = Visibility.Visible;
            txbLogin.Visibility = Visibility.Visible;
            txbPass.Visibility = Visibility.Visible;
            txbPass2.Visibility = Visibility.Visible;

            tbInputEmail.Visibility = Visibility.Visible;
            tbInputLogin.Visibility = Visibility.Visible;
            tbInputPassword.Visibility = Visibility.Visible;
            tbInputPassTwo.Visibility = Visibility.Visible;

            btnReg.Visibility = Visibility.Visible;
            btnGoAuth.Visibility = Visibility.Visible;

            btnReg2.Visibility = Visibility.Hidden;
            btnBack.Visibility = Visibility.Hidden;

            txbFam.Visibility = Visibility.Hidden;
            txbImya.Visibility = Visibility.Hidden;

            tbInputFamilia.Visibility = Visibility.Hidden;
            tbInputImya.Visibility = Visibility.Hidden;
        }
    }
}
