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
using KanbanProject.Interface_Layer;

namespace KanbanProject.Presentation_Layer
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        Service service = new Service();
        viewModel.UserWindowDataContext VM;
        public LoginWindow()
        {
            InitializeComponent();

            this.VM = new UserWindowDataContext();

            this.DataContext = this.VM;
        }

        private void return_Click(object sender, RoutedEventArgs e)
        {
                MainWindow win1 = new MainWindow();
                win1.Show();
                this.Close();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (this.VM.login())
            {
                service.startup();
                MessageBox.Show("login successfully");
                BoardsWindow win1 = new BoardsWindow();
                win1.Show();
                this.Close();
            }
            else
            {
                string message = "Incorrect Email or password";
                string title = "Error";
                MessageBox.Show(message, title);
            }
        }
    }
}
