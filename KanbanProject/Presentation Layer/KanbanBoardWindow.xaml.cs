using KanbanSolution.Presentation_Layer.viewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for KanbanBoardWindow.xaml
    /// </summary>
    public partial class KanbanBoardWindow : Window
    {
        Service service = new Service();
        public static BoardWindowDataContext x;
        public KanbanBoardWindow()
        {
            InitializeComponent();
            x = new BoardWindowDataContext();
            this.DataContext = x;
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow win1 = new AddTaskWindow();
            win1.Show();
        }

        private void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            TaskIDWindow win1 = new TaskIDWindow();
            win1.Show();
        }

        private void ChangeTaskButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeTaskWindow win1 = new ChangeTaskWindow();
            win1.Show();
        }

        private void LimitColumnTasks_Click(object sender, RoutedEventArgs e)
        {
            LimitColumnWindow win1 = new LimitColumnWindow();
            win1.Show();
        }

        private void EditColumn_Click(object sender, RoutedEventArgs e)
        {
            ColumnWindow win1 = new ColumnWindow();
            win1.Show();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ColumnWindow win1 = new ColumnWindow();
            win1.Show();
        }

        private void Return_click(object sender, RoutedEventArgs e)
        {
                BoardsWindow win1 = new BoardsWindow();
                win1.Show();
                this.Close();
        }

        private void showTask_click(object sender, RoutedEventArgs e)
        {
            TaskViewWindow win1 = new TaskViewWindow();
            win1.Show();
        }

    }
}