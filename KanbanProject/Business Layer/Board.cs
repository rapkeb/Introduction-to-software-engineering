using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject
{
    public class Board
    {
        private String name;
        private Dictionary<string, Column> columns;
        private string userEmail;
        public string[] ColumnsOrder { get; set; }

        /**
         * a defult constructor
         */
        public Board(string email,string name)
        {
            this.name = name;
            this.userEmail = email;
            this.ColumnsOrder = new string[] { "Backlog", "InProgress", "Done" };
            columns = new Dictionary<string, Column>();
            Column c1 = new Column(ColumnsOrder[0]);
            Column c2 = new Column(ColumnsOrder[1]);
            Column c3 = new Column(ColumnsOrder[2]);
            columns.Add(ColumnsOrder[0], c1);
            columns.Add(ColumnsOrder[1], c2);
            columns.Add(ColumnsOrder[2], c3);
        }
        public Board(string email, string name,int size)
        {
            this.name = name;
            this.userEmail = email;
            this.ColumnsOrder = new string[size];
            columns = new Dictionary<string, Column>();
        }
        /**
        * a constructor to recreate a board
        */
        public Board(string userEmail, Dictionary<string, Column> columns, string[] ColumnsOrder)
        {
            this.userEmail = userEmail;
            this.columns = columns;
            this.ColumnsOrder = ColumnsOrder;
        }
        /**
        * a function the returns the columns of a board
        */
        public string GetName()
        {
            return this.name;
        }
        public string GetUserMail()
        {
            return this.userEmail;
        }
        public int IndexOfColumn(string nameOfColumn)
        {
            for (int i = 0; i < ColumnsOrder.Length; i++)
                if (ColumnsOrder[i].Equals(nameOfColumn))
                    return i;
            return -1;
        }
        public Dictionary<string, Column> GetColumns()
        {
            return this.columns;
        }
        /**
        * a fuction that adds a column to a board
        */
        public void AddColumn(string nameOfColumn, Column column, int position)
        {
            string[] newColumns = new string[ColumnsOrder.Length + position];
            foreach (string col in ColumnsOrder)
            {
                if (Array.IndexOf(ColumnsOrder, col) >= position - 1)
                {
                    newColumns[Array.IndexOf(ColumnsOrder, col) + 1] = col;
                }
                else
                    newColumns[Array.IndexOf(ColumnsOrder, col)] = col;
            }
            newColumns[position - 1] = nameOfColumn;
            ColumnsOrder = newColumns;
            this.columns.Add(nameOfColumn, column);
        }
        public void RemoveColumn(string nameOfColumn)
        {
            string[] newColumns = new string[ColumnsOrder.Length - 1];
            int colIndex = Array.IndexOf(ColumnsOrder, nameOfColumn);
            foreach (string col in ColumnsOrder)
            {
                if (Array.IndexOf(ColumnsOrder, col) < colIndex)
                {
                    newColumns[Array.IndexOf(ColumnsOrder, col)] = col;
                }
                else if (Array.IndexOf(ColumnsOrder, col) > colIndex)
                {
                    newColumns[Array.IndexOf(ColumnsOrder, col) - 1] = col;
                }
            }
            ColumnsOrder = newColumns;
            this.columns.Remove(nameOfColumn);
        }
        public void MoveColumn(string nameOfColumn, int position)
        {
            Column tmp = this.columns[nameOfColumn];
            this.RemoveColumn(nameOfColumn);
            this.AddColumn(nameOfColumn, tmp, position);
        }
        /**
        * a function that adds a new task to a board (left most column)
        */
        public void AddTask(string TaskID, Task task)
        {
             Column b1 = (Column)this.columns[ColumnsOrder[0]];
            b1.AddTask(TaskID, task);           
            this.columns[ColumnsOrder[0]] = b1;
        }
        /**
        * a function that checks if a task is on the board
        */

        public Boolean ContainsTask(string TaskID)
        {
            if (TaskID == "null" | TaskID == null)
                return false;
            Boolean b1 = false;
            foreach (KeyValuePair<string, Column> column in columns)
                if (column.Value.ContainsTask(TaskID))
                    b1 = true;
            return b1;
        }
        /**
        * a function that pushes a task to the next column
        */
        public void pushTask(string TaskID)
        {
            Task a1 = FindTask(TaskID);
            columns[a1.GetColumn()].RemoveTask(TaskID);
            columns[ColumnsOrder[Array.IndexOf(ColumnsOrder, a1.GetColumn()) + 1]].AddTask(a1.GetTaskID(), a1);
        }
        /**
        * a function that returns a task by the task ID
        */
        public Task FindTask(string TaskID)
        {
            Column c1;
            foreach (KeyValuePair<string, Column> column in columns)
            {
                c1 = column.Value;
                foreach(KeyValuePair<string,Task> task1 in c1.GetTasks())
                {
                    if (task1.Key.Equals(TaskID))
                    {
                        return task1.Value;
                    }
                }
            }
            return null;
        }
    }
}