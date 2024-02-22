using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using CsvHelper;
using System.Globalization;
using System.Linq.Expressions;

namespace CarApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        const string DataFileName = @"..\..\cars.txt";
        List<Car> carList = new List<Car>();
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }


        private void LoadData()
        {
            const string dataFile = @"..\..\cars.txt";
            if (File.Exists(dataFile))
            {


                IEnumerable<string> allLines = File.ReadLines(dataFile);
                foreach (string line in allLines)
                {
                    string[] items = line.Split(';');
                    string make = items[0];
                    double engine = double.Parse(items[1]);
                    string fuel = items[2];

                    carList.Add(new Car(make, engine, fuel));
                }
                lvCars.ItemsSource = carList;
                tbStatus.Text = String.Format("You curently have {0} car(s)", lvCars.Items.Count);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // we want to save the information in a file
            using (StreamWriter writer = new StreamWriter(@"..\..\cars.txt"))
            {
                foreach (Car car in carList)
                {
                    writer.WriteLine(car.ToDataString());
                }
            }
        }

        private void MenuItemAdd_Click(object sender, RoutedEventArgs e)
        {
            CarGarage carGarage = new CarGarage(null);
            carGarage.Owner = this;
            

            carGarage.AssignResult += (make, eng, fuel) =>
            {
                Car newCar = new Car(make, eng, fuel);
                carList.Add(newCar);
                lvCars.Items.Refresh();
                tbStatus.Text = String.Format("You curently have {0} car(s)", lvCars.Items.Count);
            };
            carGarage.ShowDialog();
            
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if(lvCars.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select one item", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Car carToBeDeleted = (Car) lvCars.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                carList.Remove(carToBeDeleted);
                lvCars.Items.Refresh();
                tbStatus.Text = String.Format("You curently have {0} car(s)", lvCars.Items.Count);
            }

        }

        private void MenuItemEdit_Click(object sender, RoutedEventArgs e)
        {
            if(lvCars.SelectedIndex == -1)
            {
                return;
            }
            Car car = (Car) lvCars.SelectedItem;
            CarGarage carGarage = new CarGarage(car);
            carGarage.Owner = this;

           bool? result = carGarage.ShowDialog();
            if(result == true)
            {
                lvCars.Items.Refresh();
                tbStatus.Text = String.Format("You curently have {0} car(s)", lvCars.Items.Count);
            }
        }

        private void MenuItemExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV file(*.csv)|*.csv";
            saveFileDialog.Title = "Export to file";

            if(saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using(var writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                        {

                            csv.WriteRecords(carList);
                        }
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Error writing file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
