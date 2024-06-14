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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PR_21._106_HranitelPRO_Practic
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Polzovateli curUser;
        public MainWindow(Polzovateli user)
        {
            InitializeComponent();
            
            curUser = user;

            string str = user.Familia + " " + user.Imya;
            tbkFamImya.Text = str;
        }

        private void btnOdin_Click(object sender, RoutedEventArgs e)
        {
            wndLichZajavka wndLich = new wndLichZajavka(curUser);
            wndLich.Show();
            this.Close();
        }

        private void btnGruppa_Click(object sender, RoutedEventArgs e)
        {
            wndGrupZajavka wndGrupZajavka = new wndGrupZajavka(curUser);
            wndGrupZajavka.Show();
            this.Close();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            WindowProfile profile = new WindowProfile(curUser);
            profile.Show();
            Close();
        }
    }
}
