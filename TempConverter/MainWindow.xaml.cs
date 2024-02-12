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

namespace TempConverter
{

    public partial class MainWindow : Window
    {
        char inputSelect;
        char outputSelect;
        double inputNumber;
        public MainWindow()
        {
            InitializeComponent();
            inputSelect = 'f'; 
            outputSelect = 'f';
            inputNumber = 0;
        }

        private void fahrenheitInput_Checked(object sender, RoutedEventArgs e)
        {
            
            inputSelect = 'f';
            //conversionCalc();

        }

        private void celsiusInput_Checked(object sender, RoutedEventArgs e)
        {
            inputSelect = 'c';
            conversionCalc();
        }

        private void kelvinInput_Checked(object sender, RoutedEventArgs e)
        {
            inputSelect = 'k';
            conversionCalc();
        }

        private void fahrenheitOutput_Checked(object sender, RoutedEventArgs e)
        {
            outputSelect = 'f';
          //  conversionCalc();
        }

        private void celsiusOutput_Checked(object sender, RoutedEventArgs e)
        {
            outputSelect = 'c';
            conversionCalc();
        }

        private void kelvinOutput_Checked(object sender, RoutedEventArgs e)
        {
            outputSelect = 'k';
            conversionCalc();
        }

        void conversionCalc()
        {
            double output = 0;
            switch (outputSelect)
            {
                case 'f':
                    output = convertToF();
                    break;
                case 'c':
                    output = convertToC();
                    break;
                case 'k':
                    output = convertToK();
                    break;
            }
           
                outputTextBlock.Text = output.ToString();
            
            
        }

        double convertToC()
        {
            double output = 0;
            switch (inputSelect)
            {
                case 'f':
                    output = (inputNumber - 32) * 5 / 9;
                    break;
                case 'c':
                    output = inputNumber;
                    break;
                case 'k':
                    output = inputNumber - 273.15;
                    break;
            }

            return output;
        }

        double convertToF()
        {
            double output = 0;
            switch (inputSelect)
            {
                case 'f':
                    output = inputNumber;
                    break;
                case 'c':
                    output = (inputNumber * 9 / 5) + 32;
                    break;
                case 'k':
                    output = (inputNumber - 273.15) * 9 / 5 + 32;
                    break;
            }

            return output;
        }

        double convertToK()
        {
            double output = 0;
            switch (inputSelect)
            {
                case 'f':
                    output = (inputNumber + 459.67) * 5 / 9;
                    break;
                case 'c':
                    output = inputNumber + 273.15;
                    break;
                case 'k':
                    output = inputNumber;
                    break;
            }

            return output;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            double num;
            
            if(Double.TryParse(inputBox.Text, out num))
            {
                inputNumber = num;
                conversionCalc();
            }
            else
            {
                outputTextBlock.Text = "Output: Invalid input";
            }
        }
    }
}
