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
using ClosedXML.Excel;
using Microsoft.Win32;
using System.IO;

namespace PR_21._106_HranitelPRO_Practic
{
    /// <summary>
    /// Логика взаимодействия для wndGrupZajavka.xaml
    /// </summary>
    public partial class wndGrupZajavka : Window
    {
        HranitelPRO db = HranitelPRO.GetContext();
        List<Posetiteli> posetitelis = new List<Posetiteli>();
        Polzovateli curUser = new Polzovateli();

        public wndGrupZajavka(Polzovateli user)
        {
            InitializeComponent();

            curUser = user;

            string str = user.Familia + " " + user.Imya;
            tbkFamImya.Text = str;


            //Заполнение ComboBox(а) подразделений
            List<string> cmbPList = new List<string>();
            var podrazdelenia = db.Podrazdelenia.ToList();

            foreach (Podrazdelenia p in podrazdelenia)
            {
                cmbPList.Add(p.Nazvanie_goroda + " " + p.Nazvanie_ylici + " " + p.Nomer_stroenia.ToString());
            }
            cmbPodrazdel.ItemsSource = cmbPList;
            //Заполнение ComboBox(а) подразделений


            //Заполнение ComboBox(а) целей посещения
            cmbCelPos.ItemsSource = db.Celi.ToList();
            //Заполнение ComboBox(а) целей посещения


            //DataPicker - Свободные(доступные) даты
            List<CalendarDateRange> listDate = new List<CalendarDateRange>();

            dpDataStart.BlackoutDates.AddDatesInPast();

            foreach (Zajavki z in db.Zajavki)
            {
                if (z.ID_Statusa == 1)
                {
                    listDate.Add(new CalendarDateRange(z.Data_poseshenia));
                }
            }
            for (int i = 0; i < listDate.Count; i++)
            {
                dpDataStart.BlackoutDates.Add(listDate[i]);

            }
            //DataPicker - Свободные(доступные) даты
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(curUser);
            main.Show();
            this.Close();
        }

        private void btnAddtoSpisok_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на заполнения формы для добавления гостей
            if (tbFamilia.Text == "" || tbYmya.Text == "" || tbOtch.Text == "" || tbPhoneNumber.Text == ""
                || tbEmail.Text == "" || dpDataRozhdenia == null || dpDataRozhdenia.Text == ""
                || tbSeriaPas.Text == "" || tbNomerPas.Text == "") 
            { MessageBox.Show("Заполните обязательные поля формы(Помечены <*>)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            //Проверка на заполнения формы для добавления гостей

            //Проверка на ЧС
            foreach(CherniySpisok bL in db.CherniySpisok)
            {
                if (tbSeriaPas.Text == bL.Seria_pas && tbNomerPas.Text == bL.Nomer_pas)
                {
                    MessageBox.Show("Человек, которого вы пытаетесь добавить находится в черном списке предприятия\n" +
                    "<" + bL.Familia + " " + bL.Imya + " " + bL.Otchestvo + ">", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            //Проверка на ЧС

            //Проверка даты рождения
            var userDate = (DateTime)dpDataRozhdenia.SelectedDate;

            if (DateTime.Now.Date.Year - userDate.Date.Year < 14)
            { MessageBox.Show("Возраст гостей - от 14 лет!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); return; }

            else if (DateTime.Now.Date.Year - userDate.Date.Year == 14)
            {
                if (DateTime.Now.Date.Month > userDate.Date.Month)
                { MessageBox.Show("Возраст гостей - от 14 лет!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); return; }

                else if (DateTime.Now.Date.Month == userDate.Date.Month)
                {
                    if (DateTime.Now.Date.Day > userDate.Date.Day)
                    { MessageBox.Show("Возраст гостей - от 14 лет!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); return; }
                }
            }
            //Проверка даты рождения

            //Проверка пасспортных данных
            if (tbSeriaPas.Text.Length < 4 || tbNomerPas.Text.Length < 6)
            { MessageBox.Show("Некорректно заполнены пасспортные данные", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning); return; }    
            //Проверка пасспортных данных

            Posetiteli posetitel = new Posetiteli();

            posetitel.Familia = tbFamilia.Text;
            posetitel.Imya = tbYmya.Text.ToString();
            posetitel.Otchestvo = tbOtch.Text.ToString();
            posetitel.Nomer_telefona = tbPhoneNumber.Text.ToString();
            posetitel.Email = tbEmail.Text.ToString();
            posetitel.Nazvanie_organizacii = tbOrganizacia.Text.ToString();
            posetitel.Data_rozhdenia = (DateTime)dpDataRozhdenia.SelectedDate;
            posetitel.Seria_pas = tbSeriaPas.Text.ToString();
            posetitel.Nomer_pas = tbNomerPas.Text.ToString();

            posetitelis.Add(posetitel);

            lvPosetitelList.Items.Add(posetitel);
            
        }

        private void btnClearForm_Click(object sender, RoutedEventArgs e)
        {
            dpDataStart.Text = "";
            dpDataRozhdenia.Text = "";

            cmbCelPos.Text = "";
            cmbPodrazdel.Text = "";

            tbFIOKompanii.Clear();
            tbFamilia.Clear();
            tbYmya.Clear();
            tbOtch.Clear();
            tbEmail.Clear();
            tbPhoneNumber.Clear();
            tbOrganizacia.Clear();
            tbSeriaPas.Clear();
            tbNomerPas.Clear();

            lvPosetitelList.Items.Clear();

            posetitelis.Clear();
        }

        private void btnOform_Click(object sender, RoutedEventArgs e)
        {
            //Проверка заполнения цели посещения(cmbCelPos)
            if (cmbCelPos == null || cmbCelPos.Text == "")
            { MessageBox.Show("Выберите цель посещения", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            //Проверка заполнения цели посещения(cmbCelPos)

            //Проверка заполнения подразделения(cmbPodrazdel)
            if (cmbPodrazdel == null || cmbPodrazdel.Text == "")
            { MessageBox.Show("Выберите подразделение", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            //Проверка заполнения подразделения(cmbPodrazdel)

            //Проверка на количество посетителей
            if (posetitelis.Count < 2)
            { MessageBox.Show("В групповой заявке должно быть от 2 гостей", "Ошибка"); return; }
            else if(posetitelis.Count == 0)
            { MessageBox.Show("Вы не добаввили ни одного песетителя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            else if (posetitelis.Count >= 15)
            { MessageBox.Show("Максимальное количество гостей на одно песещение - 15", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            //Проверка на количество посетителей

            //Проверка на заполнение даты посещения
            if(dpDataStart == null || dpDataStart.Text == "")
            { MessageBox.Show("Укажите дату, в которую хотите посетить объект", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            //Проверка на заполнение даты посещения

            //Проверка даты рождения
            for (int i = 0; i < posetitelis.Count; i++)
            {
                if (DateTime.Now.Date.Year - posetitelis[i].Data_rozhdenia.Date.Year < 14)
                { MessageBox.Show("Возраст гостей - от 14 лет!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); return; }

                else if (DateTime.Now.Date.Year - posetitelis[i].Data_rozhdenia.Date.Year == 14)
                {
                    if (DateTime.Now.Date.Month > posetitelis[i].Data_rozhdenia.Date.Month)
                    { MessageBox.Show("Возраст гостей - от 14 лет!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); return; }

                    else if (DateTime.Now.Date.Month == posetitelis[i].Data_rozhdenia.Date.Month)
                    {
                        if (DateTime.Now.Date.Day > posetitelis[i].Data_rozhdenia.Date.Day)
                        { MessageBox.Show("Возраст гостей - от 14 лет!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); return; }
                    }
                }
            }
            //Проверка даты рождения

            //Проверка на ЧС
            foreach (CherniySpisok bL in db.CherniySpisok)
            {
                for (int i = 0; i < posetitelis.Count; i++)
                {
                    if (posetitelis[i].Seria_pas == bL.Seria_pas && posetitelis[i].Nomer_pas == bL.Nomer_pas)
                    {
                        MessageBox.Show("Человек, которого вы пытаетесь добавить находится в черном списке предприятия\n" +
                        "<" + bL.Familia + " " + bL.Imya + " " + bL.Otchestvo + ">", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            //Проверка на ЧС

            string numberToApplication = AutoGenerateNumber();
            foreach (Zajavki z in db.Zajavki.ToList())
            {
                while (z.Nomer_zajavki == numberToApplication)
                {
                    numberToApplication = AutoGenerateNumber();
                    break;
                }
            }

            Zajavki zajavka = new Zajavki();

            zajavka.ID_Polzovatelia = curUser.ID_Polzovatelia;
            zajavka.ID_Celi = (int)cmbCelPos.SelectedValue;
            zajavka.Nomer_zajavki = numberToApplication;
            zajavka.ID_Statusa = 3;


            string[] columns = cmbPodrazdel.Text.Split(' ');
            foreach (Podrazdelenia p in db.Podrazdelenia)
            {
                if (p.Nazvanie_goroda == columns[0] && p.Nazvanie_ylici == columns[1] && p.Nomer_stroenia == int.Parse(columns[2]))
                {
                    zajavka.ID_Podrazdelenia = p.ID_Podrazdelenia;
                }
            }

            zajavka.Data_poseshenia = (DateTime)dpDataStart.SelectedDate;
            zajavka.Data_oformlenia = DateTime.Now;

            db.Zajavki.Add(zajavka);
            db.Posetiteli.AddRange(posetitelis);
            db.SaveChanges();

            
            for (int i = 0; i < posetitelis.Count; i++)
            {
                zajavka.GrupZajavki.Add(new GrupZajavki { ID_Posetitelia = posetitelis[i].ID_Psetitelia });
            }
            
            db.SaveChanges();


            MessageBox.Show("Заявка успешно создана!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void tbPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void tbPhoneNumber_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);
        }

        private static bool IsTextNumeric(string str)
        {
            Regex reg = new Regex("[^0-9]");
            return reg.IsMatch(str);

        }

        private static bool IsTextWord(string str)
        {
            Regex reg = new Regex("[^а-я]", RegexOptions.IgnoreCase);
            return reg.IsMatch(str);
        }

        private void tbSeriaPas_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);
        }

        private void tbNomerPas_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);
        }

        private string AutoGenerateNumber()
        {
            string[] alphavit = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", 
                "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};

            string number = "GR-";
            Random rnd = new Random();

            while (number.Length < 12)
            {
                if (rnd.Next(4) == 0)
                { number += alphavit[rnd.Next(alphavit.Length)]; }
                else
                { number += rnd.Next(10); }
            }

            return number;
        }

        private void tbFamilia_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextWord(e.Text);
        }

        private void btnShowCalendar_Click(object sender, RoutedEventArgs e)
        {
            Calendar calendar = new Calendar();
            calendar.Show();
        }

        private void tbYmya_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextWord(e.Text);
        }

        private void tbOtch_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextWord(e.Text);
        }

        private void ExcelParsing(string fileName)
        {
            try
            {
                var wBook = new XLWorkbook(fileName);
                var excelDoc = wBook.Worksheet(1);

            
                for (int i = 2; i <= 16; i++)
                {
                    List<string> fio = excelDoc.Cell("B" + i.ToString()).GetValue<string>().Split(' ').ToList();
                    List<string> passport = excelDoc.Cell("D" + i.ToString()).GetValue<string>().Split(' ').ToList();

                    if (fio.Count < 2 || passport.Count < 2)
                        return;

                    Posetiteli visitor = new Posetiteli();
                    visitor.Familia = fio[0];
                    visitor.Imya = fio[1];

                    if (fio.Count == 3)
                    { visitor.Otchestvo = fio[2]; }

                    visitor.Seria_pas = passport[0];
                    visitor.Nomer_pas = passport[1];
                    visitor.Nomer_telefona = excelDoc.Cell("C" + i.ToString()).GetValue<string>();
                    visitor.Data_rozhdenia = excelDoc.Cell("E" + i.ToString()).GetValue<DateTime>();

                    if (visitor != null)
                    {
                        if (visitor.Familia != "" && visitor.Imya != ""
                            && visitor.Seria_pas != "" && visitor.Nomer_pas != ""
                            && visitor.Nomer_telefona != "" && visitor.Data_rozhdenia != null)
                        {
                            posetitelis.Add(visitor);
                        }
                        else
                        { MessageBox.Show("Некорректно заполнен документ Excel\nP.s: Смотрите пример", "Ошибка!", 
                            MessageBoxButton.OK, MessageBoxImage.Error); return; }
                    }
                    else
                    { MessageBox.Show("Некорректно заполнен документ Excel\nP.s: Смотрите пример", "Ошибка!", 
                        MessageBoxButton.OK, MessageBoxImage.Error); return; }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Ошибка!", 
                MessageBoxButton.OK, MessageBoxImage.Error); return; }
        }

        private void btnExcelParsing_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() != true)
                return;

            ExcelParsing(openFileDialog.FileName);

            for (int i = 0; i < posetitelis.Count; i++)
            {
                lvPosetitelList.Items.Add(posetitelis[i]);
            }
        }

        private void btnExcelExample_Click(object sender, RoutedEventArgs e)
        {
            var exampleExcel = new XLWorkbook();
            var eE = exampleExcel.Worksheets.Add("Лист 1");


        }

        private bool CheckVisitor(Posetiteli visitor)
        {
            foreach(Posetiteli p in db.Posetiteli)
            {
                if (p.Seria_pas == visitor.Seria_pas && p.Nomer_pas == visitor.Nomer_pas) 
                { return false; }
            }
            return true;
        }
    }
}
