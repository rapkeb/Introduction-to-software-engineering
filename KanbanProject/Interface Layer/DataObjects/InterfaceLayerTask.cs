using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanSolution.Interface_Layer.DataObjects
{
    class InterfaceLayerTask
    {
        private string nameOfColumn;
        private string taskID;
        private DateTime creationTime;
        private DateTime dueDate;
        private string title;
        private string description;


        public string NameOfColumn { get => nameOfColumn; set => nameOfColumn = value; }
        public string TaskID { get => taskID; set => taskID = value; }
        public DateTime CreationTime { get => creationTime; }
        public DateTime DueDate { get => dueDate; set => dueDate = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }

        public InterfaceLayerTask(string nameOfColumn, string taskID, DateTime dueDate, DateTime creationTime, string title, string description)
        {
            this.nameOfColumn = nameOfColumn;
            this.taskID = taskID;
            this.creationTime = creationTime;
            this.dueDate = dueDate;
            this.title = title;
            this.description = description;
        }
    }
}