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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IceCream
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List <Flavor> flavors = new List<Flavor>();
            //foreach (Flavor flavor in Flavors) { }
            flavors.Add(new Flavor("Mint Chocolate"));
            flavors.Add(new Flavor("Chocolate"));
            flavors.Add(new Flavor("Vanilla"));
            flavors.Add(new Flavor("Rocky Road"));
            lvFlavors.ItemsSource = flavors;
        }
    }

   class Flavor
    {
        public string Name { get; set; } 
        
        public Flavor(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
