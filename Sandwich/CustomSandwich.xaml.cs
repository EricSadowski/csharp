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

namespace Sandwich
{
    /// <summary>
    /// Interaction logic for CustomSandwich.xaml
    /// </summary>
    public partial class CustomSandwich : Window
    {
        public event Action<string, string, string> AssignResult;
        public CustomSandwich()
        {
            InitializeComponent();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            string breadType = comboBread.Text;
            List<string> selectedVeggies = new List<string>();

            if(cbVegCucumbers.IsChecked == true)
            {
                selectedVeggies.Add(cbVegCucumbers.Content.ToString());

            }
            if (cbVegLettuce.IsChecked == true)
            {
                selectedVeggies.Add(cbVegLettuce.Content.ToString());

            }
            if (cbVegTomatos.IsChecked == true)
            {
                selectedVeggies.Add(cbVegTomatos.Content.ToString());
            }
            string meat = "";
            if(rbMeatChicken.IsChecked == true)
            {
                meat = rbMeatChicken.Content.ToString();
            }
            if (rbMeatTofu.IsChecked == true)
            {
                meat = rbMeatTofu.Content.ToString();
            }
            if (rbMeatTurki.IsChecked == true)
            {
                meat = rbMeatTurki.Content.ToString();
            }

            AssignResult?.Invoke(breadType, string.Join(",", selectedVeggies), meat);
            DialogResult = true;
        }
    }
}
