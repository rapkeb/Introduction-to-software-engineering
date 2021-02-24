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
    /// Interaction logic for LimitColumnWindow.xaml
    /// </summary>
    public partial class LimitColumnWindow : Window
    {
        LimitColumnWindowDataContext VM;
        public LimitColumnWindow()
        {
            InitializeComponent();

            this.VM = new LimitColumnWindowDataContext();

            this.DataContext = VM;
        }

        private void limit_Click(object sender, RoutedEventArgs e)
        {
            if (this.VM.limitColumnTasks())
            {
                MessageBox.Show("Limiting tasks successfully");
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
