using Microsoft.Win32;
using OwnerCars.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace OwnerCars
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public byte[] ImageData { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                LoadData();
            }catch (SystemException ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadData()
        {
            lvOwners.SelectedItem = -1;
            tbName.Text = string.Empty;
            ImagePreview.Source = null;
            idBox.Content = "";

            // Retrieve owners with associated cars and count the number of cars for each owner
            List<Owner> owners = Global.context.owners.Include("Cars").ToList();
            foreach (Owner owner in owners)
            {
                owner.TotalCars = owner.Cars.Count;
            }

            lvOwners.ItemsSource = owners;
            lvOwners.Items.Refresh();
            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFieldsValid()) { return; }

            try
            {
                string name = tbName.Text;
                byte[] img = ImageData;


                Owner owner = new Owner { Name = name, Image = img };
                Global.context.owners.Add(owner);
                Global.context.SaveChanges();

                LoadData();
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message);
            }

        }


        private bool IsFieldsValid()
        {
            if (tbName.Text.Length < 2)
            {
                MessageBox.Show("Name must be between 2 and 100 characters", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }
            if (ImageData == null)
            {
                MessageBox.Show("Choose a picture", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;

            //valiadte the image
        }


        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == true)
            {
                // Read the binary data of the selected image file
                ImageData = File.ReadAllBytes(openFileDialog.FileName);

                // Display the selected image in the Image element
                ImagePreview.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }


            public static ImageSource GetImage(byte[] imageData)
            {
                if (imageData == null || imageData.Length == 0)
                    return null;

                var imageSource = new BitmapImage();
                using (var stream = new MemoryStream(imageData))
                {
                    imageSource.BeginInit();
                    imageSource.CacheOption = BitmapCacheOption.OnLoad;
                    imageSource.StreamSource = stream;
                    imageSource.EndInit();
                    imageSource.Freeze();
                }

                return imageSource;
            }
        

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Owner ownerToBeDeleted = (Owner)lvOwners.SelectedItem;
            if (ownerToBeDeleted == null) { return; }
            if (MessageBoxResult.Yes != MessageBox.Show("Do you want to delete ? \n" + ownerToBeDeleted.Name, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                return;
            }
            try
            {
                Global.context.owners.Remove(ownerToBeDeleted);
                Global.context.SaveChanges();
                LoadData();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFieldsValid()) { return; }
            Owner ownerUpdate = (Owner)lvOwners.SelectedItem;
            if (ownerUpdate == null) { return; }
            try
            {
                ownerUpdate.Name = tbName.Text;
                ownerUpdate.Image = ImageData;


                Global.context.SaveChanges();

                LoadData();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void btnManage_Click(object sender, RoutedEventArgs e)
        {
            if (lvOwners.SelectedIndex == -1)
            {
                return;
            }
            Owner owner = (Owner)lvOwners.SelectedItem;
            CarManager carGarage = new CarManager(owner);
            carGarage.Owner = this;

            carGarage.ShowDialog();

            //bool? result = carGarage.ShowDialog();
            //if (result == true)
            //{
            //    LoadData();
                
            //}
            LoadData();
        }

        private void lvOwners_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lvOwners.SelectedIndex == -1)
            {
                return;
            }

            Owner owner = (Owner)lvOwners.SelectedItem;
            tbName.Text = owner.Name;
            //slDifficulty.Value = (int)slDifficulty.Value;
            ImageData = owner.Image;
            ImagePreview.Source = GetImage(ImageData);
            idBox.Content = owner.Id;



            btnDelete.IsEnabled = true;
            btnUpdate.IsEnabled = true;

        }
    }
}
