using KanbanProject.Presentation_Layer.viewModel;
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

namespace KanbanProject.Presentation_Layer
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        viewModel.UserWindowDataContext VM;
        public RegisterWindow()
        {
            InitializeComponent();

            this.VM = new UserWindowDataContext();

            this.DataContext = this.VM;
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            if (this.VM.register())
            {
                MessageBox.Show("Registration successfully");
                MainWindow win1 = new MainWindow();
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

        private void return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win1 = new MainWindow();
            win1.Show();
            this.Close();
        }
    }
}
