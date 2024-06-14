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
    /// Логика взаимодействия для WindowProfile.xaml
    /// </summary>
    public partial class WindowProfile : Window
    {
        //Colors
        Brush black = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
        Brush green = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00ff00"));
        Brush gray = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF808080"));
        //Colors

        Polzovateli currentUser = new Polzovateli();
        HranitelPRO db = HranitelPRO.GetContext();
        Organizacii currentOrganization = new Organizacii();
        public WindowProfile(Polzovateli curUser)
        {
            currentUser = curUser;

            InitializeComponent();

            FillDataGrid();

            FillCurrentOrganization();
            FillUserInfo();
            CheckTextBoxes();           
        }

        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            spEditPanel.IsEnabled = true;
            tbFirstName.Visibility = Visibility.Visible;
            tbLastName.Visibility = Visibility.Visible;
            tbPatronymic.Visibility = Visibility.Visible;
            btnEditProfile.Visibility = Visibility.Collapsed;
            btnCancelEdit.Visibility = Visibility.Visible;
            btnSaveEditFIO.Visibility = Visibility.Visible;
            btnSaveEditOrganization.Visibility = Visibility.Visible;

            if (currentOrganization != null) 
            {
                btnSaveEditOrganization.Visibility = Visibility.Collapsed;
                tbOrganizationGenDirector.IsEnabled = false;
                tbOrganizationINN.IsEnabled = false;
                tbOrganizationName.IsEnabled = false;
                tbOrganizationOGRN.IsEnabled = false;
            }            
        }

        private void btnSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            HideEdit();
        }

        private void btnSaveEditFIO_Click(object sender, RoutedEventArgs e)
        {
            var updateUser = db.Polzovateli.Where(u => u.ID_Polzovatelia == currentUser.ID_Polzovatelia).FirstOrDefault();
            updateUser.Familia = tbLastName.Text;
            updateUser.Imya = tbFirstName.Text;

            if (tbPatronymic.Text != "Отчество" && tbkPatronymic.Text != null)
            { updateUser.Otchestvo = tbPatronymic.Text; }  

            db.SaveChanges();

            MessageBox.Show("Информация успешно сохранена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            CheckTextBoxes();
            FillUserInfo();
            HideEdit();
        }

        private void btnSaveEditOrganization_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на заполнение полей
            if (tbOrganizationGenDirector.Text == "Генеральный директор" || tbOrganizationINN.Text == "ИНН организации"
                || tbOrganizationName.Text == "Название организацции" || tbOrganizationOGRN.Text == "ОГРН номер"
                || tbOrganizationGenDirector.Text == "" || tbOrganizationINN.Text == "" || tbOrganizationName.Text == "" 
                || tbOrganizationOGRN.Text == "") 
            { MessageBox.Show("Нужно заполнить все поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            //Проверка на заполнение полей

            //Проверка ИНН
            if (tbOrganizationINN.Text.Length != 10)
            {
                tbOrganizationINN.Text = "ИНН организации";
                MessageBox.Show("ИНН организации должен состоять из 10 цифр.\nПровверьте праввильность заполнения!",
                    "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //Проверка ИНН

            //Провверка ОГРН
            if(tbOrganizationOGRN.Text.Length != 13)
            {
                tbOrganizationOGRN.Text = "ОГРН номер";
                MessageBox.Show("ОГРН номер должен состоять из 13 цифр.\nПровверьте праввильность заполнения!",
                    "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //Провверка ОГРН

            var organization = new Organizacii();

            organization.Nazvanie = tbOrganizationName.Text;
            organization.INN = tbOrganizationINN.Text;
            organization.OGRN = tbOrganizationOGRN.Text;
            organization.GenDir_FIO = tbOrganizationGenDirector.Text;

            if(!(db.Organizacii.Any(o => o.INN == organization.INN)))
            {
                db.Organizacii.Add(organization);
                db.SaveChanges();
            }

            var curOrg = db.Organizacii.Where(o => o.INN == organization.INN).FirstOrDefault();
            var cU = db.Polzovateli.Where(p => p.ID_Polzovatelia == currentUser.ID_Polzovatelia).FirstOrDefault();

            cU.ID_Organizacii = curOrg.ID_Organizacii;
            db.SaveChanges();

            MessageBox.Show("Информация успешно сохранена.\nОжидайте подтверждения.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            FillCurrentOrganization();
            CheckTextBoxes();
            FillUserInfo();
            HideEdit();           
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(currentUser);
            main.Show();
            Close();
        }

        private void CheckTextBoxes()
        {
            if (tbFirstName.Text != "" && tbFirstName.Text != null && tbFirstName.Text != "Имя")
            { tbFirstName.Foreground = black; }

            if (tbLastName.Text != "" && tbLastName.Text != null && tbLastName.Text != "Фамилия")
            { tbLastName.Foreground = black; }

            if (tbPatronymic.Text != "" && tbPatronymic.Text != null && tbPatronymic.Text != "Отчество")
            { tbPatronymic.Foreground = black; }

            if (tbOrganizationName.Text != "" && tbOrganizationName.Text != null && tbOrganizationName.Text != "Название организации")
            { tbOrganizationName.Foreground = black; }

            if (tbOrganizationINN.Text != "" && tbOrganizationINN.Text != null && tbOrganizationINN.Text != "ИНН организации")
            { tbOrganizationINN.Foreground = black; }

            if (tbOrganizationGenDirector.Text != "" && tbOrganizationGenDirector.Text != null && tbOrganizationGenDirector.Text != "Генеральный директор")
            { tbOrganizationGenDirector.Foreground = black; }

            if (tbOrganizationOGRN.Text != "" && tbOrganizationOGRN.Text != null && tbOrganizationOGRN.Text != "ОГРН номер")
            { tbOrganizationOGRN.Foreground = black; }
        }

        private void FillUserInfo()
        {
            if (currentOrganization != null)
            {
                tbOrganizationName.Text = currentOrganization.Nazvanie;
                tbOrganizationINN.Text = currentOrganization.INN;
                tbOrganizationGenDirector.Text = currentOrganization.GenDir_FIO;
                tbOrganizationOGRN.Text = currentOrganization.OGRN;
            }                               

            tbkName.Text = currentUser.Imya;
            tbkSurname.Text = currentUser.Familia;
            tbLastName.Text = currentUser.Familia;
            tbFirstName.Text = currentUser.Imya;

            if (currentUser.Otchestvo != null)
            {
                tbPatronymic.Text = currentUser.Otchestvo;
                tbkPatronymic.Text = currentUser.Otchestvo;
            }
        }

        private void HideEdit()
        {
            spEditPanel.IsEnabled = false;
            tbFirstName.Visibility = Visibility.Hidden;
            tbLastName.Visibility = Visibility.Hidden;
            tbPatronymic.Visibility = Visibility.Hidden;
            btnEditProfile.Visibility = Visibility.Visible;
            btnCancelEdit.Visibility = Visibility.Collapsed;
            btnSaveEditFIO.Visibility = Visibility.Collapsed;
            btnSaveEditOrganization.Visibility = Visibility.Collapsed;
        }

        private void FillCurrentOrganization()
        { 
            currentOrganization = db.Organizacii.Where(o => o.ID_Organizacii == currentUser.ID_Organizacii).FirstOrDefault();
            if (currentOrganization != null && currentUser.Status == "+") 
            {
                elipsOtmetka.Fill = green;
                elipsOtmetka.ToolTip = "Подтверждена";
            }
        }

        private void FillDataGrid()
        {
            var appli = new List<Zajavki>();
            foreach (Zajavki z in db.Zajavki)
            {
                if(z.ID_Polzovatelia == currentUser.ID_Polzovatelia)
                {
                    appli.Add(z);
                }
            }

            var _appliCurUser = appli.Join(db.Statusi, z => z.ID_Statusa, s => s.ID_Statusa, (z, s) => new
            {
                ID_Zajavki = z.ID_Zajavki,
                Nomer_zajavki = z.Nomer_zajavki,
                ID_Polzovatelia = z.ID_Polzovatelia,
                ID_Podrazdelenia = z.ID_Podrazdelenia,
                Status = s.Nazvanie,
                StatusColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(s.Color)),
                Message = z.Soobshenie
            });

            var appliCurUser = _appliCurUser.Join(db.Podrazdelenia, z => z.ID_Podrazdelenia, p => p.ID_Podrazdelenia, (z, p) => new
            {
                ID_Zajavki = z.ID_Zajavki,
                Nomer_zajavki = z.Nomer_zajavki,
                ID_Polzovatelia = z.ID_Polzovatelia,
                Division = p.Nazvanie_goroda + " " + p.Nazvanie_ylici + " " + p.Nomer_stroenia,
                Status = z.Status,
                StatusColor = z.StatusColor,
                Message = z.Message
            });

            if (appliCurUser != null) 
            { lvApplications.ItemsSource = appliCurUser.ToList(); }

            if (lvApplications.Items.Count == 0)
            {
                lvApplications.Visibility = Visibility.Collapsed;
                tbEmptyAppli_1.Visibility = Visibility.Visible;
                tbEmptyAppli_2.Visibility = Visibility.Visible;
            }                      
        }


        //Фокус текстбоксов

        private void tbOrganizationName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbOrganizationName.Text == "Название организации")
            { tbOrganizationName.Clear(); tbOrganizationName.Foreground = black; }
        }

        private void tbOrganizationName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (currentOrganization != null)
            {
                if (tbOrganizationName.Text == "")
                { tbOrganizationName.Text = currentOrganization.Nazvanie; return; }
            }

            if (tbOrganizationName.Text == "")
            { tbOrganizationName.Text = "Название организации"; tbOrganizationName.Foreground = gray; }

            if (tbOrganizationName.Text != "" && tbOrganizationName.Text != "Название организации")
            { tbOrganizationName.Foreground = black; }
        }

        private void tbOrganizationINN_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbOrganizationINN.Text == "ИНН организации")
            { tbOrganizationINN.Clear(); tbOrganizationINN.Foreground = black; }
        }

        private void tbOrganizationINN_LostFocus(object sender, RoutedEventArgs e)
        {
            if (currentOrganization != null)
            {
                if (tbOrganizationINN.Text == "")
                { tbOrganizationINN.Text = currentOrganization.INN; return; }
            }

            if (tbOrganizationINN.Text == "")
            { tbOrganizationINN.Text = "ИНН организации"; tbOrganizationINN.Foreground = gray; }

            if (tbOrganizationINN.Text != "" && tbOrganizationINN.Text != "ИНН организации")
            { tbOrganizationINN.Foreground = black; }
        }

        private void tbOrganizationGenDirector_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbOrganizationGenDirector.Text == "Генеральный директор")
            { tbOrganizationGenDirector.Clear(); tbOrganizationGenDirector.Foreground = black; }
        }

        private void tbOrganizationGenDirector_LostFocus(object sender, RoutedEventArgs e)
        {
            if (currentOrganization != null)
            {
                if (tbOrganizationGenDirector.Text == "")
                { tbOrganizationGenDirector.Text = currentOrganization.GenDir_FIO; return; }
            }

            if (tbOrganizationGenDirector.Text == "")
            { tbOrganizationGenDirector.Text = "Генеральный директор"; tbOrganizationGenDirector.Foreground = gray; }

            if (tbOrganizationGenDirector.Text != "" && tbOrganizationGenDirector.Text != "Генеральный директор")
            { tbOrganizationGenDirector.Foreground = black; }
        }

        private void tbOrganizationOGRN_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbOrganizationOGRN.Text == "ОГРН номер")
            { tbOrganizationOGRN.Clear(); tbOrganizationOGRN.Foreground = black; }
        }

        private void tbOrganizationOGRN_LostFocus(object sender, RoutedEventArgs e)
        {
            if (currentOrganization != null)
            {
                if (tbOrganizationOGRN.Text == "")
                { tbOrganizationOGRN.Text = currentOrganization.OGRN; return; }
            }

            if (tbOrganizationOGRN.Text == "")
            { tbOrganizationOGRN.Text = "ОГРН номер"; tbOrganizationOGRN.Foreground = gray; }

            if (tbOrganizationOGRN.Text != "" && tbOrganizationOGRN.Text != "ОГРН номер")
            { tbOrganizationOGRN.Foreground = black; }
        }

        private void tbLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbLastName.Text == "Фамилия")
            { tbLastName.Clear(); }
        }

        private void tbLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if(currentUser.Familia != null)
            {
                if (tbLastName.Text == "")
                { tbLastName.Text = currentUser.Familia; return; }
            }

            if (tbLastName.Text == "")
            { tbLastName.Text = "Фамилия"; }
        }

        private void tbFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbFirstName.Text == "Имя")
            { tbFirstName.Clear(); }
        }

        private void tbFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (currentUser.Imya != null)
            {
                if (tbFirstName.Text == "")
                { tbFirstName.Text = currentUser.Imya; return; }
            }

            if (tbFirstName.Text == "")
            { tbFirstName.Text = "Имя"; }
        }

        private void tbPatronymic_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbPatronymic.Text == "Отчество")
            { tbPatronymic.Clear(); tbPatronymic.Foreground = black; }
        }

        private void tbPatronymic_LostFocus(object sender, RoutedEventArgs e)
        {
            if (currentUser.Otchestvo != null)
            {
                if (tbPatronymic.Text == "")
                { tbPatronymic.Text = currentUser.Otchestvo; return; }
            }

            if (tbPatronymic.Text == "")
            { tbPatronymic.Text = "Отчество"; tbPatronymic.Foreground = gray; }

            if (tbPatronymic.Text != "" && tbPatronymic.Text != "Отчество")
            { tbPatronymic.Foreground = black; }
        }

        //Фокус текстбоксов



        //SelectionChanged Текстбоксов организацции и редактирования

        private void tbOrganizationName_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void tbOrganizationINN_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void tbOrganizationGenDirector_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void tbOrganizationOGRN_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void tbLastName_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void tbFirstName_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void tbPatronymic_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        //SelectionChanged Текстбоксов организацции и редактирования
    }
}
