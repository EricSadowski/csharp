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
using System.Windows.Shapes;

namespace CarApplication
{
    /// <summary>
    /// Interaction logic for CarGarage.xaml
    /// </summary>
    public partial class CarGarage : Window
    {
        public event Action<string, double, string> AssignResult;
        public Car curCar;
      //  slEngine.ValueChanged += SlEngine_ValueChanged;
        public CarGarage(Car car)
        {
            InitializeComponent();

            if(car != null )
            {
                make.Text = car.Make;
                slEngine.Value = car.Engine;
                comboFuel.Text = car.Fuel;
                curCar = car;
                saveBtn.Content = "Update";
            }
        }



        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            string fuelType = comboFuel.Text;
            string makeType = make.Text;
            double engineSize = (double)slEngine.Value;
            if(curCar != null )
            {
                curCar.Make = makeType;
                curCar.Fuel = fuelType;
                curCar.Engine = engineSize;
            }
            else
            {
                AssignResult?.Invoke(makeType, engineSize, fuelType);
            }
            
            DialogResult = true;
        }

        

// Then, define the event handler
        private void SlEngine_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Set the content of the Label to the current value of the Slider
            valEngine.Content = slEngine.Value.ToString();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
