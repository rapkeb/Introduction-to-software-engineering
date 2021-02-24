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
    /// Interaction logic for ChangeTaskWindow.xaml
    /// </summary>
    public partial class ChangeTaskWindow : Window
    {
        ChangeTaskWindowDataContext VM;
        public ChangeTaskWindow()
        {
            InitializeComponent();

            this.VM = new ChangeTaskWindowDataContext();

            this.DataContext = VM;
        }

        private void Title_Click(object sender, RoutedEventArgs e)
        {
            if (this.VM.ChangeTitle())
            {
                MessageBox.Show("Title has been changed successfully");
                KanbanBoardWindow.x.showTheard();
                this.Close();
            }
            else
            {
                MessageBox.Show("Some fields are wrong");
            }
        }

        private void Description_Click(object sender, RoutedEventArgs e)
        {
            if (this.VM.ChangeDescription())
            {
                MessageBox.Show("Description has been changed successfully");
                KanbanBoardWindow.x.showTheard();
                this.Close();
            }
            else
            {
                MessageBox.Show("Some fields are wrong");
            }
        }

        private void DueDate_Click(object sender, RoutedEventArgs e)
        {
            if (this.VM.ChangeDueDate())
            {
                MessageBox.Show("DueDate has been changed successfully");
                KanbanBoardWindow.x.showTheard();
                this.Close();
            }
            else
            {
                MessageBox.Show("Some fields are wrong");
            }
        }

        private void return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}