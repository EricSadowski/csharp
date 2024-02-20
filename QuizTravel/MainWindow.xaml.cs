using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizTravel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string DataFileName = @"..\..\trips.txt";
        List<Trip> tripList = new List<Trip>();
        public MainWindow()
        {
            InitializeComponent();
            LoadData();

        }


        private void NameValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[;]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void LoadData()
        {
            const string dataFile = @"..\..\trips.txt";
            if (File.Exists(dataFile))
            {


                IEnumerable<string> allLines = File.ReadLines(dataFile);
                foreach (string line in allLines)
                {
                    string[] items = line.Split(';');
                    string destination = items[0];
                    string name = items[1];
                    string passport = items[2];
                    string departure = items[3];
                    string re_turn = items[4];

                    tripList.Add(new Trip(destination, name, passport, departure, re_turn));
                }
                lvTrips.ItemsSource = tripList;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // we want to save the information in a file
            using (StreamWriter writer = new StreamWriter(DataFileName))
            {
                foreach (Trip trip in tripList)
                {
                    writer.WriteLine(trip.ToDataString());
                }
            }
        }

        private void ResetValues()
        {
            lvTrips.Items.Refresh();
            txtName.Clear();
            txtDestination.Clear();
            txtPassport.Clear();
            dpDepart.Text = "";
            dpReturn.Text = "";



            lvTrips.SelectedIndex = -1;

            btnDeleteTrip.IsEnabled = false;
            btnUpdateTrip.IsEnabled = false;
        }

        private void lvTrips_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnUpdateTrip.IsEnabled = true;
            btnDeleteTrip.IsEnabled = true;

            var selectedTrip = lvTrips.SelectedItem;

            if (selectedTrip is Trip)
            {
                Trip trip = (Trip)selectedTrip;
                txtDestination.Text = trip.Destination;
                txtName.Text = trip.Name;
                txtPassport.Text = trip.Passport;
                dpDepart.Text = trip.Departure;
                dpReturn.Text = trip.Return;
            }
        }

        private void btnAddTrip_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string passport = txtPassport.Text;
            string destination = txtDestination.Text;

            string re_turn = dpReturn.Text;
            string departure = dpDepart.Text;

            var compare = DateTime.Compare(DateTime.Parse(departure), DateTime.Parse(re_turn));

            if(compare == 1)
            {
                
                MessageBox.Show("Departure must be before arrival");
                return;
            }




            Trip t = new Trip(destination,name,passport,departure,re_turn);
            tripList.Add(t);

            ResetValues();
        }

        private void btnUpdateTrip_Click(object sender, RoutedEventArgs e)
        {
            if (lvTrips.SelectedIndex == -1)
            {
                MessageBox.Show("you need to choose one trip");
                return;
            }

            string newName = txtName.Text;
            string passport = txtPassport.Text;
            string destination = txtDestination.Text;
            string re_turn = dpReturn.Text;
            string departure = dpDepart.Text;

            var compare = DateTime.Compare(DateTime.Parse(departure), DateTime.Parse(re_turn));

            if (compare == 1)
            {

                MessageBox.Show("Departure must be before arrival");
                return;
            }


            Trip tripToBeUpdated = (Trip)lvTrips.SelectedItem;
            tripToBeUpdated.Name = newName;
            tripToBeUpdated.Passport = passport;
            tripToBeUpdated.Destination = destination;
            tripToBeUpdated.Return = re_turn;
            tripToBeUpdated.Departure = departure;


            ResetValues();
        }

        private void btnDeleteTrip_Click(object sender, RoutedEventArgs e)
        {
            if (lvTrips.SelectedIndex == -1)
            {
                MessageBox.Show("you need to choose one trip");
                return;
            }

            Trip tripToBeDeleted = (Trip)lvTrips.SelectedItem;
            tripList.Remove(tripToBeDeleted);

            ResetValues();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            //Display a SaveFileDialog so the user can save the file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "trip files (*.trips)|*.trips|All files (*.*)|*.*";
            saveFileDialog.Title = "Save the trips in a file";
            if (saveFileDialog.ShowDialog() == true)
            {
                string allData = "";
                foreach (Trip trip in tripList)
                {
                    allData += trip.ToString() + "\n";
                }
                File.WriteAllText(saveFileDialog.FileName, allData);
            }
        }
    }
}
