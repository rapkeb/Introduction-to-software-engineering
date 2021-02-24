using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanProject.Interface_Layer;

namespace KanbanProject.Presentation_Layer.viewModel
{
    public class LimitColumnWindowDataContext : INotifyPropertyChanged
    {
        string nameOfColumn = "";
        public string NameOfColumn
        {
            get
            {
                return nameOfColumn;
            }
            set
            {
                nameOfColumn = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("nameOfColumn"));
            }
        }
        int tasks;
        public int Tasks
        {
            get
            {
                return tasks;
            }
            set
            {
                tasks = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("tasks"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        Service service;

        public LimitColumnWindowDataContext()
        {
            // emulating some registered users, this naturally shouldnt be here.
            service = new Service();
        }

        public bool limitColumnTasks()
        {
            return service.LimitColumnTasks(this.nameOfColumn, this.tasks);
        }

    }
}
