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
    /// Логика взаимодействия для wndLichZajavka.xaml
    /// </summary>
    public partial class wndLichZajavka : Window
    {
        Polzovateli curUser = new Polzovateli();

        public wndLichZajavka(Polzovateli user)
        {
            InitializeComponent();

            curUser = user;

            string str = user.Familia + " " + user.Imya;
            tbkFamImya.Text = str;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(curUser);
            main.Show();
            this.Close();
        }

        private void btnClearForm_Click(object sender, RoutedEventArgs e)
        {
            dpDataStart.Text = "";
            dpDataFinish.Text = "";
            dpDataRozhdenia.Text = "";

            cmbCelPos.Text = "";
            cmbPodrazdel.Text = "";

            tbFIOKompanii.Clear();
            tbFamilia.Clear();
            tbImya.Clear();
            tbOtch.Clear();
            tbEmail.Clear();
            tbPhoneNumber.Clear();
            tbOrganizacia.Clear();
            tbSeriaPas.Clear();
            tbNomerPas.Clear();
        }
    }
}
