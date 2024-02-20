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

namespace IcecreamSelector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HashSet<string> selectedFlavors = new HashSet<string>();
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            const string dataFile = @"..\..\available.txt";
            if (File.Exists(dataFile))
            {
                List<string> flavorList = new List<string>();
                IEnumerable<string> allLines = File.ReadLines(dataFile);
                foreach (string line in allLines)
                {
                    flavorList.Add(line);
                }
                lvIceCreamFalvors.ItemsSource = flavorList;
            }
        }

        private void BtnAddSelectedFlavor_Click(object sender, RoutedEventArgs e)
        {
            if(lvIceCreamFalvors.SelectedIndex == -1)
            {
                MessageBox.Show("you need to choose a falvor", "Error" ,MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }

            var selectedItemList = lvIceCreamFalvors.SelectedItems;
            
            foreach (string item in selectedItemList)
            {
                selectedFlavors.Add(item);

            }

            //we need to do a workaround !

            lvSelectedFalvors.ItemsSource = null;
            lvSelectedFalvors.ItemsSource = selectedFlavors;

            lvIceCreamFalvors.SelectedIndex = -1;
        }

        private void BtnDeleteScoop_Click(object sender, RoutedEventArgs e)
        {
            //the list should not be empty
            if(lvSelectedFalvors.Items.Count == 0)
            {
                return;
            }
            //if user doesn't choose any item
            if(lvSelectedFalvors.SelectedIndex == -1)
            {
                MessageBox.Show("please choose a scoop");
                return;
            }

            var selectedForDelete = lvSelectedFalvors.SelectedItems;
            
            foreach(string item in selectedForDelete)
            {
                selectedFlavors.Remove(item);
            }

            lvSelectedFalvors.ItemsSource = null;
            lvSelectedFalvors.ItemsSource = selectedFlavors;

        }

        private void BtnClearScoops_Click(object sender, RoutedEventArgs e)
        {
            if (lvSelectedFalvors.Items.Count == 0)
            {
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure to delete the list?", "Clear All", MessageBoxButton.YesNo, MessageBoxImage.Warning); ;
            if(result == MessageBoxResult.Yes)
            {
                lvSelectedFalvors.ItemsSource = null;
                selectedFlavors.Clear();
            }
        }

        private void BtnAddSelectedFlavor_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
