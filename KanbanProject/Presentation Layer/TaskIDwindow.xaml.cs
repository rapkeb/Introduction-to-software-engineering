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
    /// Interaction logic for TaskIDWindow.xaml
    /// </summary>
    public partial class TaskIDWindow : Window
    {
        TaskIDWindowDataContext VM;
        public TaskIDWindow()
        {
            InitializeComponent();

            this.VM = new TaskIDWindowDataContext();

            this.DataContext = VM;
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (this.VM.RemoveTask())
            {
                MessageBox.Show("Task successfully removed from board");
                KanbanBoardWindow.x.showTheard();
                this.Close();
            }
            else
            {
                MessageBox.Show("Task ID is wrong");
            }
        }

        private void Push_Click(object sender, RoutedEventArgs e)
        {
            if (this.VM.PushTask())
            {
                MessageBox.Show("Task successfully pushed");
                KanbanBoardWindow.x.showTheard();
                this.Close();
            }
            else
            {
                MessageBox.Show("Task ID is wrong");
            }
        }

        private void return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}