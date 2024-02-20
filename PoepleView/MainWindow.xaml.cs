using System;
using System.Collections.Generic;
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

namespace PoepleView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string DataFileName = @"..\..\people.txt";
        List<Person> peopleList = new List<Person>();
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists(DataFileName))
            {
                string[] peopleInfo = File.ReadAllLines(DataFileName);
                
                foreach(string personLine in peopleInfo)
                {
                    string[] person = personLine.Split(';');

                    //make a person
                    string personName = person[0];
                    int age;
                    if (!int.TryParse(person[1], out age))
                    {
                        MessageBox.Show("an error happened in reading the file");
                    }
                    Person p = new Person(personName, age);
                    peopleList.Add(p);
                }
            }

            lvPeople.ItemsSource = peopleList;
        }

        private void Winow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           // we want to save the information in a file
           using (StreamWriter writer = new StreamWriter(DataFileName))
            {
                foreach(Person person in peopleList)
                {
                    writer.WriteLine(person.ToDataString());
                }
            }
        }

        private void btnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string ageText = txtAge.Text;

            int age;
            if (!int.TryParse(ageText, out age))
            {
                MessageBox.Show("enter value number for age", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Person p = new Person(name, age);
            peopleList.Add(p);

            ResetValues();
        }

        private void ResetValues()
        {
            lvPeople.Items.Refresh();
            txtName.Clear();
            txtAge.Clear();

            lvPeople.SelectedIndex = -1;

            btnDeletePerson.IsEnabled = false;
            btnUpdatePerson.IsEnabled = false;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnUpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            if(lvPeople.SelectedIndex == -1)
            {
                MessageBox.Show("you need to choose one person");
                return;
            }

            string newName = txtName.Text;
            string newAge = txtAge.Text;

            Person personToBeUpdated = (Person)lvPeople.SelectedItem;
            personToBeUpdated.Name = newName;
            personToBeUpdated.Age = int.Parse(newAge);

            ResetValues();
        }

        private void btnDeletePerson_Click(object sender, RoutedEventArgs e)
        {
            if (lvPeople.SelectedIndex == -1)
            {
                MessageBox.Show("you need to choose one person");
                return;
            }

            Person personToBeDeleted = (Person)lvPeople.SelectedItem;
            peopleList.Remove(personToBeDeleted);

            ResetValues();
        }

        private void lvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnUpdatePerson.IsEnabled = true;
            btnDeletePerson.IsEnabled = true;

            var selectedPerson = lvPeople.SelectedItem;

            if(selectedPerson is Person) // instance of
            {
                Person person = (Person)selectedPerson;
                txtName.Text = person.Name;
                txtAge.Text = person.Age.ToString();
            } 
        }
    }
}
