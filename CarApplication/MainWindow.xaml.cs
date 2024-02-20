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

        private void lvCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            CarGarage carGarage = new CarGarage();
            carGarage.Owner = this;
            

            carGarage.AssignResult += (make, eng, fuel) =>
            {
                Car newCar = new Car(make, eng, fuel);
                carList.Add(newCar);
                lvCars.Items.Refresh();
            };
            carGarage.ShowDialog();
        }

    }
}
