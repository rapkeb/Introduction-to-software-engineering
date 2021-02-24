using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanProject.Interface_Layer;

namespace KanbanProject.Presentation_Layer.viewModel
{
    public class TaskIDWindowDataContext
    {
        string taskID = "";
        public string TaskID
        {
            get
            {
                return taskID;
            }
            set
            {
                taskID = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("taskID"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        Service service;

        public TaskIDWindowDataContext()
        {
            // emulating some registered users, this naturally shouldnt be here.
            service = new Service();
        }

        public bool PushTask()
        {
            return service.pushTask(this.taskID);
        }
        public bool RemoveTask()
        {
            return service.removeTask(this.taskID);
        }
        public Task ShowTask()
        {
            return service.ShowTask(this.taskID);
        }
        public bool ContainsTask()
        {
            return service.ContainsTask(this.taskID);
        }
    }
}
