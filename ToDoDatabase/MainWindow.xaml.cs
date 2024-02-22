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
using ToDoDatabase.Domain;

namespace ToDoDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //filling the combobox
            comboStatus.ItemsSource = Enum.GetValues(typeof(Domain.ToDo.StatusEnum));
            LoadData();
        }

        private void LoadData()
        {
            tbTask.Text = string.Empty;
            slDifficulty.Value = 0;
            dpDueDate.Text = string.Empty;
            comboStatus.SelectedIndex = -1;
            lvTodos.SelectedIndex = -1;

            List<ToDo> todos = Global.context.todos.ToList<ToDo>();
            lvTodos.ItemsSource = todos;
            lvTodos.Items.Refresh();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // validate the data

            string task = tbTask.Text;
            int diff = (int)slDifficulty.Value;
            DateTime dueDate = (DateTime)dpDueDate.SelectedDate;
            ToDo.StatusEnum status = (ToDo.StatusEnum) comboStatus.SelectedItem;

            ToDo todo = new ToDo { Task = task, Difficulty = diff, DueDate = dueDate, Status = status };
            Global.context.todos.Add(todo);
            Global.context.SaveChanges();

            LoadData();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ToDo todoToBeDeleted = (ToDo)lvTodos.SelectedItem;

            Global.context.todos.Remove(todoToBeDeleted);
            Global.context.SaveChanges();
            LoadData();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            ToDo todoUpdate = (ToDo)lvTodos.SelectedItem;
            todoUpdate.Task = tbTask.Text;
            todoUpdate.Difficulty = (int)slDifficulty.Value;
            todoUpdate.Status = (ToDo.StatusEnum)comboStatus.SelectedItem;
            todoUpdate.DueDate = (DateTime)dpDueDate.SelectedDate;

            Global.context.SaveChanges();

            LoadData();
        }

        private void lvTodos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lvTodos.SelectedIndex == -1)
            {
                return;
            }

            ToDo todo = (ToDo)lvTodos.SelectedItem;
            tbTask.Text = todo.Task;
            slDifficulty.Value = (int)slDifficulty.Value;
            dpDueDate.SelectedDate = todo.DueDate;
            comboStatus.SelectedItem = todo.Status;

            btnDelete.IsEnabled = true;
            btnUpdate.IsEnabled = true;
        }
    }
}
