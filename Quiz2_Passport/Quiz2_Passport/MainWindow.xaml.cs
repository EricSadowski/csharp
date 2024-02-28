using Quiz2_Passport.Domain;
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

namespace Quiz2_Passport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {

            List<Passport> passports = Global.context.passports.ToList<Passport>();
            lvPassports.ItemsSource = passports;
            lvPassports.Items.Refresh();
        }

        private void lvPassports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvPassports.SelectedIndex == -1)
            {
                return;
            }
            btnInsert.Content = "Update";
            Passport passport = (Passport)lvPassports.SelectedItem;

            idZone.Text = passport.Id.ToString();
            tbFirst.Text = passport.First;
            tbLast.Text = passport.Last;
            tbPass.Text = passport.PassNum;
            dpExpire.Text = passport.Expiration;
            cbValid.IsChecked = passport.IsValid;

            btnDelete.IsEnabled = true;
            
        }

        private bool IsFieldsValid()
        {
            if (tbFirst.Text.Length < 2 || tbLast.Text.Length < 2)
            {
                MessageBox.Show("Length Of Name too Short", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            // validate the data

            if (!IsFieldsValid()) { return; };

            try
            {

                string first = tbFirst.Text;
                string last = tbLast.Text;
                string pass = tbPass.Text;
                string exp = dpExpire.Text;
                bool isValid = (bool)cbValid.IsChecked;
                Passport passport = new Passport { First = first, Last = last, PassNum = pass, Expiration = exp, IsValid = isValid };

                if (lvPassports.SelectedIndex == -1)
                {

                    Global.context.passports.Add(passport);
                }
                else
                {
                    Passport passportUpdate = (Passport)lvPassports.SelectedItem;
                    passportUpdate.First = first;
                    passportUpdate.Last = last;
                    passportUpdate.Expiration = exp;
                    passportUpdate.IsValid = isValid;
                    passportUpdate.PassNum = pass;
                }


                Global.context.SaveChanges();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            Clear();
            LoadData();
            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Passport passToDelete = (Passport)lvPassports.SelectedItem;

                Global.context.passports.Remove(passToDelete);
                Global.context.SaveChanges();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            LoadData();
            Clear();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            idZone.Text = "...";
            tbFirst.Text = string.Empty;
            tbLast.Text = string.Empty;
            tbPass.Text = string.Empty;
            dpExpire.Text = string.Empty;
            cbValid.IsChecked = false;
            lvPassports.SelectedIndex = -1;
            btnDelete.IsEnabled = false;
            btnInsert.Content = "Add";
        }
    }
}
