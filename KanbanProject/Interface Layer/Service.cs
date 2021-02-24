using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.Interface_Layer
{
    class Service
    {
        public Board GetBoard()
        {
            return Mainframe.currentUser.GetCurrentBoard();
        }
        public Dictionary<string, Board> GetBoards()
        {
            return Mainframe.currentUser.GetBoards();
        }
        public bool register(string email, string Password)
        {
            return Mainframe.Register(email, Password);
        }
        public bool login(string email, string Password)
        {
            return Mainframe.Login(email, Password);
        }
        public bool addBoard(string nameOfBoard)
        {
            return Mainframe.AddBoard(nameOfBoard);
        }
        public bool removeBoard(string nameOfBoard)
        {
            return Mainframe.RemoveBoard(nameOfBoard);
        }
        public bool ShowBoard(string nameOfBoard)
        {
            return Mainframe.ChooseBoard(nameOfBoard);
        }
        public bool addTask(string title, string description, string date)
        {
            return Mainframe.AddTask(title, description, date);
        }
        public bool removeTask(string taskID)
        {
            return Mainframe.RemoveTask(taskID);
        }
        public bool pushTask(string taskID)
        {
            return Mainframe.PushTask(taskID);
        }
        public bool LimitColumnTasks(string nameOfColumn, int limit1)
        {
            return Mainframe.LimitColumnTasks(nameOfColumn, limit1);
        }
        public bool changeTitle(string title, string taskID)
        {
            return Mainframe.ChangeTitle(title, taskID);
        }
        public bool changeDescription(string description, string taskID)
        {
            return Mainframe.ChangeDescription(description, taskID);
        }
        public bool changeDueDate(string date, string taskID)
        {
            return Mainframe.ChangeDueDate(date, taskID);
        }
        public bool AddColumn(string nameOfColumn,int position)
        {
            return Mainframe.AddColumn(nameOfColumn, position);
        }
        public bool MoveColumn(string nameOfColumn, int position)
        {
            return Mainframe.MoveColumn(nameOfColumn, position);
        }
        public bool RemoveColumn(string nameOfColumn)
        {
            return Mainframe.RemoveColumn(nameOfColumn);
        }
        public bool startup()
        {
            return Mainframe.startup();
        }
        public bool Logout()
        {
           return Mainframe.Logout();
        }
        public Task ShowTask(string taskID)
        {
            return Mainframe.currentUser.GetCurrentBoard().FindTask(taskID);
        }
        public bool ContainsTask(string taskID)
        {
            return Mainframe.currentUser.GetCurrentBoard().ContainsTask(taskID);
        }

    }
}