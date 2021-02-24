using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject
{
    public class Task
    {
        private string TaskID;
        private DateTime creationTime;
        private DateTime dueDate;
        private string title;
        private string description;
        private string column;

        /**
         * a defult constuctor
         */
        public Task(string TaskID, DateTime dueDate, string title, string description, string column)
        {
            this.creationTime = DateTime.Now;
            this.dueDate = dueDate;
            this.title = title;
            this.description = description;
            this.column = column;
            this.TaskID = TaskID;
        }
        /**
         * a constructor to recreate a task
         */
        public Task(string TaskID, DateTime dueDate, DateTime creationTime, string title, string description, string column)
        {
            this.creationTime = creationTime;
            this.dueDate = dueDate;
            this.title = title;
            this.description = description;
            this.column = column;
            this.TaskID = TaskID;
        }
        /*
         * this class only has getters and setters 
         */
        public DateTime GetCreationTime()
        {
            return this.creationTime;
        }
        public DateTime GetDueDate()
        {
            return this.dueDate;
        }
        public void SetDueDate(DateTime dueDate)
        {
            this.dueDate = dueDate;
        }
        public string GetTitle()
        {
            return this.title;
        }
        public void SetTitle(string title)
        {
            this.title = title;
        }
        public string GetDescription()
        {
            return this.description;
        }
        public string GetTaskID()
        {
            return this.TaskID;
        }
        public void SetDescription(string Description)
        {
            this.description = Description;
        }
        public string GetColumn()
        {
            return this.column;
        }
        public void SetColumn(string column)
        {
            this.column = column;
        }
    }
}