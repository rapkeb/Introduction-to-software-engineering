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
using KanbanProject.Interface_Layer;
using KanbanProject.Presentation_Layer.viewModel;

namespace KanbanProject.Presentation_Layer
{
    /// <summary>
    /// Interaction logic for BoardsWindow.xaml
    /// </summary>

    public partial class BoardsWindow : Window
    {
        Service service = new Service();
       // public BoardsDataContext k;
        public BoardsDataContext k;
        public BoardsWindow()
        {
            InitializeComponent();

            k = new BoardsDataContext();

            this.DataContext = k;
        }

        private void AddBoardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.k.AddBoard())
            {
                MessageBox.Show("Board has added");
                k.showTheard1();
            }
            else
            {
                MessageBox.Show("Name of board is wrong");
            }
        }
        private void RemoveBoardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.k.RemoveBoard())
            {
                MessageBox.Show("Board has removed");
                k.showTheard1();
            }
            else
            {
                MessageBox.Show("Name of board is wrong");
            }
        }

        private void showBoard_click(object sender, RoutedEventArgs e)
        {
            if (this.k.ShowBoard())
            {
                KanbanBoardWindow win1 = new KanbanBoardWindow();
                win1.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Name of board is wrong");
            }
        }

        private void Logout_click(object sender, RoutedEventArgs e)
        {
            if (service.Logout())
            {
                MainWindow win1 = new MainWindow();
                win1.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("error while logging-out");
            }
        }
    }
}
