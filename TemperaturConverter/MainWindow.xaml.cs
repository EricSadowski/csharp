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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TemperaturConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtBoxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            // I need a method to be called to convert the temperature
            TempConvert();
        }

        private void TempConvert()
        {
            double temp;
            if(!double.TryParse(txtBoxInput.Text, out temp))
            {
                return;
            }

            if(rbtnInputCelcius.IsChecked == true)
            {
                if(rbtnOutputCelcius.IsChecked == true)
                {
                    lblResult.Content = string.Format("{0:0.0}° C", temp);
                }
                if (rbtnOutputFarenheit.IsChecked == true)
                {
                    lblResult.Content = string.Format("{0:0.0}° C", (temp * 9 /5) + 32);
                }
                if (rbtnOutputKelvin.IsChecked == true)
                {
                    lblResult.Content = string.Format("{0:0.0}° C", (temp + 273.15));

                }
            }
            if (rbtnInputFarenheit.IsChecked == true)
            {
                if (rbtnOutputCelcius.IsChecked == true)
                {

                }
                if (rbtnOutputFarenheit.IsChecked == true)
                {

                }
                if (rbtnOutputKelvin.IsChecked == true)
                {

                }
            }
            if (rbtnInputKelvin.IsChecked == true)
            {
                if (rbtnOutputCelcius.IsChecked == true)
                {

                }
                if (rbtnOutputFarenheit.IsChecked == true)
                {

                }
                if (rbtnOutputKelvin.IsChecked == true)
                {

                }
            }
        }
    }
}
