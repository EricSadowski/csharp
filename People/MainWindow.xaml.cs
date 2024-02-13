using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace People
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
        private ObservableCollection<People> peopleList = new ObservableCollection<People>();

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void LoadData()
        {
            const string dataFile = @"..\..\available.txt";
            if (File.Exists(dataFile))
            {

                
                IEnumerable<string> allLines = File.ReadLines(dataFile);
                foreach (string line in allLines)
                {
                   string[] items = line.Split(';');
                    string name = items[0];
                    int age = int.Parse(items[1]);
                    peopleList.Add(new People(name, age));
                }
                lvPeople.ItemsSource = peopleList;
            }
        }

        class People
        {

            public string Name { get; set; }
            public int Age { get; set; }
            public People(string name, int age)
            {
                Name = name;
                Age = age;
            }
            public override string ToString()
            {
                return Name + " " + Age;
            }
        }

        private void btnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            int age = int.Parse(txtBoxAge.Text);
            string name = txtBoxName.Text;
            People guy = new People(name, age);
            peopleList.Add(guy);
           // lvPeople.Items.Add(guy);
        }
    }
}
