using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanProject.Interface_Layer;


namespace KanbanProject.Presentation_Layer.viewModel
{
   public class TaskWindowDataContext : INotifyPropertyChanged
    {
        string title = "";
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("title"));
            }
        }
        string descrition = "";
        public string Description
        {
            get
            {
                return descrition;
            }
            set
            {
                descrition = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("description"));
            }
        }
        string date = "";
        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("date"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        Service service;

        public TaskWindowDataContext()
        {
            // emulating some registered users, this naturally shouldnt be here.
            service = new Service();
        }

        public bool addTask()
        {
            return service.addTask(this.title, this.descrition, this.date);
        }
    }
}

