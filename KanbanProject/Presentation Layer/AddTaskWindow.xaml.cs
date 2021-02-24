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
using KanbanProject.Presentation_Layer.viewModel;

namespace KanbanProject.Presentation_Layer
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        viewModel.TaskWindowDataContext VM;
        public AddTaskWindow()
        {
            InitializeComponent();

            this.VM = new TaskWindowDataContext();

            this.DataContext = this.VM;
        }

        private void return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (this.VM.addTask())
            {
                MessageBox.Show("Task successfully added");
                KanbanBoardWindow.x.showTheard();
                this.Close();
            }
            else
            {
                MessageBox.Show("Some fields are incorrect");
            }
        }
    }
}
