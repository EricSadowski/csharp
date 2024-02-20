using Microsoft.Win32;
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
using System.Xml.Linq;

namespace ToDoStuff
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        const string DataFileName = @"..\..\tasks.txt";
        List<Task> taskList = new List<Task>();
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            const string dataFile = @"..\..\tasks.txt";
            if (File.Exists(dataFile))
            {


                IEnumerable<string> allLines = File.ReadLines(dataFile);
                foreach (string line in allLines)
                {
                    string[] items = line.Split(';');
                    string name = items[0];
                    int difficult = int.Parse(items[1]);
                    string date = items[2];
                    string status = items[3];

                    taskList.Add(new Task(name, difficult, date, status));
                }
                lvTasks.ItemsSource = taskList;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // we want to save the information in a file
            using (StreamWriter writer = new StreamWriter(DataFileName))
            {
                foreach (Task task in taskList)
                {
                    writer.WriteLine(task.ToDataString());
                }
            }
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxName.Text;
            int difficulty = (int)slDifficulty.Value;
            string date = dpDueDate.Text;
            string status = comStatus.Text;

            Task t = new Task(name, difficulty, date, status);
            taskList.Add(t);

            ResetValues();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lvTasks.SelectedIndex == -1)
            {
                MessageBox.Show("you need to choose one task");
                return;
            }

            string newName = textBoxName.Text;
            int difficulty = (int)slDifficulty.Value;
            string date = dpDueDate.Text;
            string status = comStatus.Text;

            Task taskToBeUpdated = (Task)lvTasks.SelectedItem;
            taskToBeUpdated.Name = newName;
            taskToBeUpdated.Difficulty = difficulty;
            taskToBeUpdated.Date = date;
            taskToBeUpdated.Status = status;

            ResetValues();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvTasks.SelectedIndex == -1)
            {
                MessageBox.Show("you need to choose one task");
                return;
            }

            Task taskToBeDeleted = (Task)lvTasks.SelectedItem;
            taskList.Remove(taskToBeDeleted);

            ResetValues();
        }

        private void btnExportToFile_Click(object sender, RoutedEventArgs e)
        {
            //Display a SaveFileDialog so the user can save the file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.Title = "Save the todos in a file";
            if (saveFileDialog.ShowDialog() == true)
            {
                string allData = "";
                foreach (Task todo in taskList)
                {
                    allData += todo.ToString() + "\n";
                }
                File.WriteAllText(saveFileDialog.FileName, allData);
            }
        }

        private void lvTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnUpdate.IsEnabled = true;
            btnDelete.IsEnabled = true;

            var selectedTask = lvTasks.SelectedItem;

            if (selectedTask is Task) // instance of
            {
                Task task = (Task)selectedTask;
                textBoxName.Text = task.Name;
                slDifficulty.Value = task.Difficulty;
                dpDueDate.Text = task.Date;
                comStatus.Text = task.Status;
            }
        }

        private void ResetValues()
        {
            lvTasks.Items.Refresh();
            textBoxName.Clear();
           // comStatus.Clear();
            

            lvTasks.SelectedIndex = -1;

            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;
        }
    }
}
