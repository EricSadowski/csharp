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
        public CarGarage()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            string fuelType = comboFuel.Text;
            string makeType = make.Text;
            double engineSize = (double)slEngine.Value;

            AssignResult?.Invoke(makeType, engineSize, fuelType);
            DialogResult = true;
        }


    }
}
