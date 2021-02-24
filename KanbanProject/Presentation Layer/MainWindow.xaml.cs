using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using KanbanProject.Presentation_Layer;
using KanbanProject.Interface_Layer;

namespace KanbanProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Service service = new Service();
        public MainWindow()
        {
            InitializeComponent();
            service.startup();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow win1 = new LoginWindow();
            win1.Show();
            this.Close();
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow win1 = new RegisterWindow();
            win1.Show();
            this.Close();
        }
        
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}