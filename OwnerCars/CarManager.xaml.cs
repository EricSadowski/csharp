using OwnerCars.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
using System.Xml.Linq;

namespace OwnerCars
{
    /// <summary>
    /// Interaction logic for CarManager.xaml
    /// </summary>
    public partial class CarManager : Window
    {

        public event Action<Car> AssignResult;
        public Owner curOwner;
        public CarManager(Owner owner)
        {
            InitializeComponent();

            if (owner != null)
            {
                ownerText.Content = owner.Name;
                curOwner = owner;

            }
            LoadData();
        }

        private bool IsDialogFieldsValid()
        {
            if (tbMake.Text.Length < 1)
            {
                MessageBox.Show("Please, fill in Make & Model", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void LoadData()
        {
            tbMake.Text = string.Empty;


            int ownerId = curOwner.Id;
            List<Car> cars = Global.context.cars.Where(c => c.OwnerId == ownerId).ToList();
            lvCars.ItemsSource = cars;
            lvCars.Items.Refresh();

            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!IsDialogFieldsValid()) { return; };
            try
            {
                Car carToAdd = new Car();
                carToAdd.OwnerId = curOwner.Id;
                carToAdd.Name = tbMake.Text;

                Global.context.cars.Add(carToAdd);
                Global.context.SaveChanges();

                int newCarId = carToAdd.Id;
                LoadData();

                AssignResult?.Invoke(carToAdd);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            Car carToBeDeleted = (Car)lvCars.SelectedItem;
            if (carToBeDeleted == null) { return; }
            if (MessageBoxResult.No == MessageBox.Show("Do you want to delete the record?\n" + carToBeDeleted.Name, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            { return; }
            try
            {

                Global.context.cars.Remove(carToBeDeleted);
                Global.context.SaveChanges();
                LoadData();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!IsDialogFieldsValid()) { return; };

            Car carUpdate = (Car)lvCars.SelectedItem;
            if (carUpdate == null) { return; }
            try
            {
                carUpdate.Name = tbMake.Text;

                Global.context.SaveChanges();

                LoadData();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lvCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lvCars.SelectedIndex == -1)
            {
                return;
            }
            Car car = (Car)lvCars.SelectedItem;
            tbMake.Text = car.Name;
            carId.Content = car.Id;
            btnDelete.IsEnabled = true;
            btnUpdate.IsEnabled = true;
        }
    }
}
