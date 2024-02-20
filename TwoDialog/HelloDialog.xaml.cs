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

namespace TwoDialog
{
    /// <summary>
    /// Interaction logic for HelloDialog.xaml
    /// </summary>
    public partial class HelloDialog : Window
    {
        public event Action<int, string> AssignResult;
        public HelloDialog(string name)
        {
            InitializeComponent();
            lblMessage.Content = $"Hello {name}";
            
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            int age = int.Parse(txtBoxAge.Text);
            AssignResult ?.Invoke (age, txtBoxCity.Text);
            DialogResult = true;

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
