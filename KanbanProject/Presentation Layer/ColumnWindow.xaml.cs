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
    /// Interaction logic for ColumnWindow.xaml
    /// </summary>
    public partial class ColumnWindow : Window
    {
        public ColumnDataContext VM;
        public ColumnWindow()
        {
            InitializeComponent();

            this.VM = new ColumnDataContext();

            this.DataContext = VM;
        }

        private void Add_click(object sender, RoutedEventArgs e)
        {
            if (this.VM.AddColumn())
            {
                MessageBox.Show("Column has added to board");
                KanbanBoardWindow.x.showTheard();
                this.Close();
            }
            else
            {
                MessageBox.Show("Some fields are wrong");
            }
        }

        private void Remove_click(object sender, RoutedEventArgs e)
        {
            if (this.VM.RemoveColumn())
            {
                MessageBox.Show("Column has deleted from board");
                KanbanBoardWindow.x.showTheard();
                this.Close();
            }
            else
            {
                MessageBox.Show("Some fields are wrong");
            }
        }

        private void Move_column(object sender, RoutedEventArgs e)
        {
            if (this.VM.MoveColumn())
            {
                MessageBox.Show("Column has been moved");
                KanbanBoardWindow.x.showTheard();
                this.Close();
            }
            else
            {
                MessageBox.Show("Some fields are wrong");
            }
        }

        private void Return_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
