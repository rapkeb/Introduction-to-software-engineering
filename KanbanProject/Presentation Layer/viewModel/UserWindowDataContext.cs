using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanProject.Interface_Layer;

namespace KanbanProject.Presentation_Layer.viewModel
{
   public class UserWindowDataContext : INotifyPropertyChanged
    {
        string email = "";
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("email"));
            }
        }
        string pwd = "";
        public string PWD
        {
            get
            {
                return pwd;
            }
            set
            {
                pwd = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("PWD"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        Service service;

        public UserWindowDataContext()
        {
            // emulating some registered users, this naturally shouldnt be here.
            service = new Service();
        }

        public bool login()
        {
            return service.login(this.email, this.pwd);
        }
        public bool register()
        {
            return service.register(this.email, this.pwd);
        }
    }
}