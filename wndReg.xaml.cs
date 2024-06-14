using PR_21._106_HranitelPRO_Practic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для wndReg.xaml
    /// </summary>
    public partial class wndReg : Window
    {
        HranitelPRO db = HranitelPRO.GetContext();

        public wndReg()
        {
            InitializeComponent();
        }

        private void btnGoAuth_Click(object sender, RoutedEventArgs e)
        {
            wndAuth auth = new wndAuth();
            auth.Show();
            this.Close();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            string login = tbInputLogin.Text;
            string password = tbInputPassword.Text;

            var users = db.Polzovateli.ToList();
            Polzovateli curUser = new Polzovateli();

            if (db.Polzovateli.Any(p => p.Login == login && p.Parol == password))
            {
                foreach(Polzovateli user in users)
                {
                    if (user.Login == login)
                    {
                        curUser = user;
                    }
                }

                var main = new MainWindow(curUser);
                main.Show();
                this.Close();
            }
            else { MessageBox.Show("Такого пользователя не существует!", "Ошибка"); }          
        }
    }
}
