using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject
{
    public class Column
    {

        private string name;
        private Dictionary<string, Task> Tasks;
        private int mountOfTasks = -1;
        private LinkedList<Task> TasksOrder = new LinkedList<Task>();

        /**
         * constructor
         */
        public Column(string name)
        {
            this.name = name;
            this.Tasks = new Dictionary<string, Task>();
        }
        /**
        * a function that returns the name of the column
        */
        public LinkedList<Task> GetTasksOrder()
        {
            return this.TasksOrder;
        }
        public string GetName()
        {
            return this.name;
        }
        /**
        * a function that returns the tasks on this column
        */
        public Dictionary<string, Task> GetTasks()
        {
            return this.Tasks;
        }
        /**
         * a function that sets the amount of tasks the column can have (-1 = infinity)
         */
        public void SetMountOfTasks(int mountOfTasks)
        {
            this.mountOfTasks = mountOfTasks;
        }
        /**
         * a function that returns the amount of tasks the column can have (-1 = infinity)
         */
        public int GetMountOfTasks()
        {
            return this.mountOfTasks;
        }
        /**
         * a function that add a task to this column
         */
        public void AddTask(string TaskID, Task task)
        {
            this.Tasks.Add(TaskID, task);
            this.TasksOrder.AddLast(task);
        }
        /**
         * a function that removes a task from this column
         */
        public void RemoveTask(string TaskID)
        {
            this.Tasks.Remove(TaskID);
            this.TasksOrder.Remove(Mainframe.currentUser.GetCurrentBoard().FindTask(TaskID));
        }
        /**
         * a function that checks if a task is on this column
         */
        public Boolean ContainsTask(string TaskID)
        {
            return Tasks.ContainsKey(TaskID);
        }
        public void SortByDueDate()
        {
            LinkedListNode<Task> tmp;
            LinkedList<Task> sorted = new LinkedList<Task>();
            foreach (Task task in TasksOrder)
            {
                tmp = sorted.First;
                if (sorted.Count == 0)
                    sorted.AddFirst(task);
                else
                {
                    while (tmp.Value.GetDueDate().CompareTo(task.GetDueDate()) < 0 & tmp.Next != null)
                    {
                        tmp = tmp.Next;
                    }
                    if (tmp.Next != null)
                        sorted.AddBefore(tmp, task);
                    else
                        sorted.AddLast(task);
                }
            }
            TasksOrder = sorted;
        }
        public void SortByCreationDate()
        {
            LinkedListNode<Task> tmp;
            LinkedList<Task> sorted = new LinkedList<Task>();
            foreach (Task task in TasksOrder)
            {
                tmp = sorted.First;
                if (sorted.Count == 0)
                    sorted.AddFirst(task);
                else
                {
                    while (tmp.Value.GetCreationTime().CompareTo(task) < 0)
                    {
                        tmp = tmp.Next;
                    }
                    if (tmp != null)
                        sorted.AddBefore(tmp, task);
                    else
                        sorted.AddLast(task);
                }
            }
            TasksOrder = sorted;

        }
    }
}