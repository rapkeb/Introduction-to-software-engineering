using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanProject.Interface_Layer;

namespace KanbanProject.Presentation_Layer.viewModel
{
   public class ChangeTaskWindowDataContext
    {
        string taskData = "";
        public string TaskData
        {
            get
            {
                return taskData;
            }
            set
            {
                taskData = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("taskData"));
            }
        }
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

        public ChangeTaskWindowDataContext()
        {
            // emulating some registered users, this naturally shouldnt be here.
            service = new Service();
        }

        public bool ChangeTitle()
        {
            return service.changeTitle(this.taskData,this.taskID);
        }
        public bool ChangeDescription()
        {
            return service.changeDescription(this.taskData, this.taskID);
        }
        public bool ChangeDueDate()
        {
            return service.changeDueDate(this.taskData, this.taskID);
        }
    }
}