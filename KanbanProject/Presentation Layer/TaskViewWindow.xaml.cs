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
    /// Interaction logic for TaskViewWindow.xaml
    /// </summary>
    public partial class TaskViewWindow : Window
    {
        TaskIDWindowDataContext VM;
        public TaskViewWindow()
        {
            InitializeComponent();
            this.VM = new TaskIDWindowDataContext();
            this.DataContext = VM;
        }

        private void Show_click(object sender, RoutedEventArgs e)
        {
            if (this.VM.ContainsTask())
            {
                creationTime.Text = this.VM.ShowTask().GetCreationTime().ToString("dd/MM/yyyy");
                title.Text = this.VM.ShowTask().GetTitle();
                description.Text = this.VM.ShowTask().GetDescription();
                dueDate.Text = this.VM.ShowTask().GetDueDate().ToString("dd/MM/yyyy");
            }
            else
            {
                MessageBox.Show("wrong Task ID");
            }
        }

        private void Return_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}