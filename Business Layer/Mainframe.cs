
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
// load TOTALNUMOFTASKS
namespace KanbanProject
{
    // <TextBox Name="search_box" HorizontalAlignment="Left" Height="33" Margin="565,55,0,0" TextWrapping="Wrap" Text="Search" VerticalAlignment="Top" Width="160" TextChanged="TextBox_TextChanged"/>

    public static class Mainframe
    {
        public static User currentUser;
        static Dictionary<string, User> USERS = new Dictionary<string, User>();
        public static long TOTALNUMOFTASKS = 10000000001;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /**
         * a private function the checks if an email is valid 
         */
        private static Boolean ValidEmail(string email)
        {
            if (email == "null" | email == null)
                return false;
            foreach (KeyValuePair<string, User> user in USERS)
            {
                if (user.Value.GetEmail() == email | email == null)
                {
                    return false;
                }
            }
            return true;
        }
        /**
         * a private function that checks if the given email is in the system
         */
        private static Boolean ExistsEmail(string email)
        {
            if (email == "null" | email == null)
                return false;
            foreach (KeyValuePair<string, User> entry in USERS)
            {
                if (entry.Value.GetEmail() == email)
                    return true;
            }
            return false;
        }
        /**
        * a private function the checks if a password is valid 
        */
        private static Boolean ValidPassword(string password)
        {
            if (password == "null" | password == null)
                return false;
            Boolean length = false;
            Boolean capitalCharacter = false;
            Boolean smallCharacter = false;
            Boolean number = false;
            if (password.Length <= 20 && password.Length >= 4)
                length = true;
            foreach (char c in password)
            {
                if ("0123456789".Contains(c))
                    number = true;
                else if ("abcdefghijklmnopqrstuvwxyz".Contains(c))
                    smallCharacter = true;
                else if ("abcdefghijklmnopqrstuvwxyz".ToUpper().Contains(c))
                    capitalCharacter = true;
            }
            return (length && capitalCharacter && smallCharacter && number);
        }
        /**
        * a private function the checks if a task title is valid 
        */
        private static Boolean ValidTitle(string title)
        {
            if (title == "null" | title == null)
                return false;
            return (title.Length > 0 && title.Length <= 50);
        }
        /**
        * a private function the checks if a task description is valid 
        */
        private static Boolean ValidDescription(string description)
        {
            if (description == "null" | description == null)
                return false;
            return (description.Length <= 300);
        }
        /**
        * a private function the checks if a given date is from the format(DD/MM/YYYY)  and contains numbers 
        */
        private static Boolean IsNumbers(string date)
        {
            int count = 0;
            foreach (char c in date)
            {
                if (!"0123456789".Contains(c))
                    count++;
            }
            return (count == 2);
        }
        /**
        * a private function the checks if a task duedate is valid 
        */
        private static Boolean ValidDueDate(string date)
        {
            if (date == "null" | date == null)
                return false;
            Boolean isValid = true;
            if (!(date.Length == 10 && date[2] == '/' && date[5] == '/'))
                isValid = false;
            else if (IsNumbers(date))
                try
                {
                    DateTime dueDate = (Convert.ToDateTime(date));
                    int tmp = dueDate.CompareTo(DateTime.Now);
                    if (!(tmp > 0))
                        isValid = false;
                }
                catch
                {
                    isValid = false;
                }
            return isValid;
        }
        /**
         * a function that registers a new user to the system
         */
        public static bool Register(string email, string password)
        {
            if (!(ValidEmail(email)))
            {
                log.Warn("Registration attempt failed, email already exists in the system");
                return false;
            }
            else if (!(ValidPassword(password)))
            {
                log.Warn("Registration attempt failed, invalid password");
                return false;
            }
            else
            {
                User user = new User(email, password);
                USERS.Add(email, user);
                string[] data = new string[] { user.GetEmail(), user.GetPassword() };
                Serialization.SaveUser(user.GetEmail(),user.GetPassword());
                log.Info("New user has registered - " + email);
                if (!USERS.ContainsKey(email))
                    log.Error("A user registered but wasn't added to the users list");
                return true;
            }
        }
        /**
        * a function that logs a user to the system
        */
        public static bool Login(string email, string password)
        {
            if (!(ValidPassword(password)))
            {
                log.Warn("Login attempt failed, invalid password");
                return false;
            }
            else if (!(ExistsEmail(email)))
            {
                log.Warn("Login attempt failed, Invalid email was submitted");
                return false;
            }
            else if (USERS[email].GetPassword() != password)
            {
                log.Warn("Login attempt failed, Wrong email or password");
                return false;
            }
            else
            {
                currentUser = USERS[email];
                USERS[email].Login(email, password);
                log.Info("Login successfull - " + email);
                return true;
            }
        }
        /**
        * a function that logs a user out of the system
        */
        public static bool Logout()
        {
            currentUser.Logout();
            log.Info("Logout successfull - " + currentUser.GetEmail());
            currentUser.SetCurrentBoardToNull();
            currentUser = null;
            return true;
        }
        private static void Logout(string email)
        {
            USERS[email].Logout();
            log.Info("Logout successfull - " + currentUser.GetEmail());
        }
        public static bool AddBoard(string nameOfBoard)
        {
            if (nameOfBoard == "null" | nameOfBoard == null)
                return false;
            if (currentUser.GetBoards().ContainsKey(nameOfBoard))
                return false;
            else
            {
                currentUser.AddBoard(nameOfBoard);
                Serialization.SaveBoard(nameOfBoard, currentUser.GetEmail());
                foreach (KeyValuePair<string, Column> column in currentUser.GetBoards()[nameOfBoard].GetColumns())
                {
                    Serialization.SaveColumn(currentUser.GetEmail(), column.Key, nameOfBoard, column.Value.GetMountOfTasks(), Array.IndexOf(currentUser.GetBoards()[nameOfBoard].ColumnsOrder, column.Key) + 1);
                }
                log.Info("New Board - " + nameOfBoard);
                if (!(currentUser.GetBoards().ContainsKey(nameOfBoard)))
                    log.Error("A new Board added but isn't on the user information");
                return true;
            }
        }
        public static bool RemoveBoard(string nameOfBoard)
        {
            if (nameOfBoard == "null" | nameOfBoard == null)
                return false;
            if (!(currentUser.GetBoards().ContainsKey(nameOfBoard)))
                return false;
            else
            {
                currentUser.RemoveBoard(nameOfBoard);
                Serialization.RemoveBoard(nameOfBoard, currentUser.GetEmail());
                log.Info("Board removed - " + nameOfBoard);
                if ((currentUser.GetBoards().ContainsKey(nameOfBoard)))
                    log.Error("A Board removed but is on the user information");
                return true;
            }
        }
        public static bool ChooseBoard(string nameOfBoard)
        {
            if (nameOfBoard == "null" | nameOfBoard == null)
                return false;
            if (!(currentUser.GetBoards().ContainsKey(nameOfBoard)))
                return false;
            else
            {
                currentUser.SetCurrentBoard(currentUser.GetBoards()[nameOfBoard]);
                return true;
            }
        }
        /**
        * a function that adds a new task to the board
        */
        public static bool AddColumn(string nameOfColumn, int position)
        {
            if (nameOfColumn == "null" | nameOfColumn == null)
                return false;
            if (currentUser.GetCurrentBoard().ColumnsOrder.Contains(nameOfColumn))
                return false;
            else if (position < 1 | position > currentUser.GetCurrentBoard().ColumnsOrder.Length)
                return false;
            else
            {
                Column column = new Column(nameOfColumn);
                currentUser.GetCurrentBoard().AddColumn(nameOfColumn, column, position);
                Serialization.SaveColumn(currentUser.GetEmail(), nameOfColumn, currentUser.GetCurrentBoard().GetName(), 0, position);
                log.Info("New column - " + nameOfColumn);
                if (!(currentUser.GetCurrentBoard().ColumnsOrder.Contains(nameOfColumn)))
                    log.Error("A new column added but isn't on the board");
                return true;
            }
        }
        public static bool MoveColumn(string nameOfColumn, int position)
        {
            if (nameOfColumn == "null" | nameOfColumn == null)
                return false;
            if (!(currentUser.GetCurrentBoard().ColumnsOrder.Contains(nameOfColumn)))
                return false;
            else if (position < 1 | position > currentUser.GetCurrentBoard().ColumnsOrder.Length)
                return false;
            else
            {
                Column tmp = currentUser.GetCurrentBoard().GetColumns()[nameOfColumn];
                currentUser.GetCurrentBoard().RemoveColumn(nameOfColumn);
                currentUser.GetCurrentBoard().AddColumn(nameOfColumn, tmp, position);
                foreach (KeyValuePair<string, Column> column in currentUser.GetCurrentBoard().GetColumns())
                {
                    Serialization.EditColumn(currentUser.GetEmail(), column.Key, currentUser.GetCurrentBoard().GetName());
                    Serialization.SaveColumn(currentUser.GetEmail(), column.Key, currentUser.GetCurrentBoard().GetName(), currentUser.GetCurrentBoard().GetColumns()[column.Key].GetMountOfTasks(), Array.IndexOf(currentUser.GetCurrentBoard().ColumnsOrder,column.Key)+1);
                }
                log.Info("Column has been moved - " + nameOfColumn);
                if (!(currentUser.GetCurrentBoard().ColumnsOrder.Contains(nameOfColumn)))
                    log.Error("A column moved but isn't on the board");
                log.Error("A column moved but isn't on the board");
                return true;
            }
        }
        public static bool RemoveColumn(string nameOfColumn)
        {
            if (nameOfColumn == "null" | nameOfColumn == null)
                return false;
            if (!(currentUser.GetCurrentBoard().ColumnsOrder.Contains(nameOfColumn)))
                return false;
            else
            {
                currentUser.GetCurrentBoard().RemoveColumn(nameOfColumn);
                Serialization.RemoveColumn(nameOfColumn, currentUser.GetCurrentBoard().GetName(), currentUser.GetEmail());
                foreach (KeyValuePair<string, Column> column in currentUser.GetCurrentBoard().GetColumns())
                {
                    Serialization.EditColumn(currentUser.GetEmail(), column.Key, currentUser.GetCurrentBoard().GetName());
                    Serialization.SaveColumn(currentUser.GetEmail(), column.Key, currentUser.GetCurrentBoard().GetName(), currentUser.GetCurrentBoard().GetColumns()[column.Key].GetMountOfTasks(), Array.IndexOf(currentUser.GetCurrentBoard().ColumnsOrder, column.Key) + 1);
                }
                log.Info("Column has been deleted - " + nameOfColumn);
                if ((currentUser.GetCurrentBoard().ColumnsOrder.Contains(nameOfColumn)))
                    log.Error("A column deleted but is still on the board");
                return true;
            }
        }
        public static bool AddTask(string title, string description, string date)
        {
            if (!(ValidTitle(title)))
            {
                log.Warn("Task creation attempt failed, Invalid title was submitted");
                return false;
            }
            else if (!(ValidDescription(description)))
            {
                log.Warn("Task creation attempt failed, Invalid description was submitted");
                return false;
            }
            else if (!(ValidDueDate(date)))
            {
                log.Warn("Task creation attempt failed, Invalid duedate was submitted");
                return false;
            }
            else if (currentUser.GetCurrentBoard().GetColumns()[currentUser.GetCurrentBoard().ColumnsOrder[0]].GetMountOfTasks() == currentUser.GetCurrentBoard().GetColumns()[currentUser.GetCurrentBoard().ColumnsOrder[0]].GetTasks().Count())
            {
                log.Warn("Task creation attempt failed, column is full");
                return false;
            }
            else
            {
                DateTime dueDate = new DateTime(Convert.ToInt32(date.Substring(6, 4)), Convert.ToInt32(date.Substring(3, 2)), Convert.ToInt32(date.Substring(0, 2)));
                Task task = new Task(TOTALNUMOFTASKS.ToString().Substring(1, 10), dueDate, title, description, currentUser.GetCurrentBoard().ColumnsOrder[0]);
                currentUser.GetCurrentBoard().AddTask(TOTALNUMOFTASKS.ToString().Substring(1, 10), task);
                log.Info("Task successfully added - " + TOTALNUMOFTASKS.ToString().Substring(1, 10));
                TOTALNUMOFTASKS++;
                Serialization.SaveTask(task.GetTaskID(), task.GetCreationTime().ToString("dd/MM/yyyy"), task.GetDueDate().ToString("dd/MM/yyyy"), task.GetTitle(), task.GetDescription(), task.GetColumn(), currentUser.GetCurrentBoard().GetName(), currentUser.GetEmail());
                if (!currentUser.GetCurrentBoard().GetColumns()[currentUser.GetCurrentBoard().ColumnsOrder[0]].ContainsTask((TOTALNUMOFTASKS - 1).ToString()))
                    log.Error("Task was created but wasn't added to Backlog column");
                return true;
            }
        }
        /**
        * a function that removes a task from the board
        */
        public static bool RemoveTask(string taskID)
        {
            if (!(currentUser.GetCurrentBoard().ContainsTask(taskID)))
            {
                log.Warn("Task removing attempt failed, task does not exist in board");
                return false;
            }
            else
            {
                foreach (var c in currentUser.GetCurrentBoard().GetColumns())
                {
                    foreach (var t in c.Value.GetTasks())
                    {
                        if (t.Key.Equals(taskID))
                        {
                            c.Value.RemoveTask(taskID);
                            break;
                        }
                    }
                }
                log.Warn("Task removed successfully");
                Serialization.RemoveTask(taskID);
                if (currentUser.GetCurrentBoard().ContainsTask(taskID))
                    log.Error("task removed but is still on board");
                return true;
            }
        }
        /**
        * a function that pushes a task to the next column
        */
        public static bool PushTask(string taskID)
        {

            if (!(currentUser.GetCurrentBoard().ContainsTask(taskID)))
            {
                log.Warn("Task pushing attempt failed, task was not be found on a board");
                return false;
            }
            else if ((currentUser.GetCurrentBoard().GetColumns()[currentUser.GetCurrentBoard().ColumnsOrder[currentUser.GetCurrentBoard().ColumnsOrder.Length - 1]].ContainsTask(taskID)))
            {
                log.Warn("Task pushing attempt failed, task could not be found in a pushable column");
                return false;
            }
            else if ((currentUser.GetCurrentBoard().GetColumns()[currentUser.GetCurrentBoard().ColumnsOrder[currentUser.GetCurrentBoard().IndexOfColumn(currentUser.GetCurrentBoard().FindTask(taskID).GetColumn()) + 1]].GetTasks().Count)
                == (currentUser.GetCurrentBoard().GetColumns()[currentUser.GetCurrentBoard().ColumnsOrder[currentUser.GetCurrentBoard().IndexOfColumn(currentUser.GetCurrentBoard().FindTask(taskID).GetColumn()) + 1]].GetMountOfTasks()))
            {
                log.Warn("Task pushing attempt failed, column is full");
                return false;
            }

            else if (currentUser.GetCurrentBoard().FindTask(taskID).GetColumn() == currentUser.GetCurrentBoard().ColumnsOrder[currentUser.GetCurrentBoard().ColumnsOrder.Length - 1])
            {
                log.Warn("Task pushing attempt failed, cannot push a task from the last column");
                return false;
            }
            else
            {
                string c1 = currentUser.GetCurrentBoard().FindTask(taskID).GetColumn();
                int index = Array.IndexOf(currentUser.GetCurrentBoard().ColumnsOrder, c1);
                currentUser.GetCurrentBoard().pushTask(taskID);
                currentUser.GetCurrentBoard().FindTask(taskID).SetColumn(currentUser.GetCurrentBoard().ColumnsOrder[currentUser.GetCurrentBoard().IndexOfColumn(currentUser.GetCurrentBoard().FindTask(taskID).GetColumn()) + 1]);
                log.Info("Task pushed successfully");
                Task task = currentUser.GetCurrentBoard().FindTask(taskID);
                Serialization.SaveTask(taskID, task.GetCreationTime().ToString("dd/MM/yyyy"), task.GetDueDate().ToString("dd/MM/yyyy"), task.GetTitle(), task.GetDescription(), task.GetColumn(), currentUser.GetCurrentBoard().GetName(), currentUser.GetEmail());
                if ((currentUser.GetCurrentBoard().GetColumns()[currentUser.GetCurrentBoard().ColumnsOrder[index]].ContainsTask(taskID)))
                    log.Error("task pushed but is still on the same column (potential duplicate)");
                if ((currentUser.GetCurrentBoard().GetColumns()[currentUser.GetCurrentBoard().ColumnsOrder[index + 1]].ContainsTask(taskID)))
                    log.Error("task pushed but isn't on the next column (potential remove)");
                return true;
            }
        }
        /**
        * a function that limits the amount of tasks allowed on a board
        */
        public static bool LimitColumnTasks(string nameOfColumn, int limit1)
        {
            if (nameOfColumn == "null" | nameOfColumn == null)
                return false;
            int limit;
            limit = Convert.ToInt32(limit1);
            if (!(currentUser.GetCurrentBoard().ColumnsOrder.Contains(nameOfColumn)))
            {
                log.Warn("Limiting tasks attempt failed, column doesn't exist in the board");
                return false;
            }
            else if ((currentUser.GetCurrentBoard().GetColumns()[nameOfColumn].GetTasks().Count() > limit))
            {
                log.Warn("Limiting tasks attempt failed, column has more tasks than the limit");
                return false;
            }
            else
            {
                currentUser.GetCurrentBoard().GetColumns()[nameOfColumn].SetMountOfTasks(limit);
                log.Info("Limiting tasks successfully changed");
                Serialization.ChangeColumn(currentUser.GetEmail(), nameOfColumn, currentUser.GetCurrentBoard().GetName(), limit, Array.IndexOf(currentUser.GetCurrentBoard().ColumnsOrder, nameOfColumn)+1);
                if (currentUser.GetCurrentBoard().GetColumns()[nameOfColumn].GetMountOfTasks() != limit)
                    log.Error("limit was set but wasn't changed");
                return true;
            }
        }
        /**
        * a function that changes the title of a task
        */
        public static bool ChangeTitle(string title, string taskID)
        {
            if (!(ValidTitle(title)))
            {
                log.Warn("Changing title attempt failed, Invalid title was submitted");
                return false;
            }
            else if (!(currentUser.GetCurrentBoard().ContainsTask(taskID)))
            {
                log.Warn("Changing title attempt failed, task does not exist in board");
                return false;
            }
            else if (currentUser.GetCurrentBoard().FindTask(taskID).GetColumn() == currentUser.GetCurrentBoard().ColumnsOrder[currentUser.GetCurrentBoard().ColumnsOrder.Length - 1])
            {
                log.Warn("Changing title attempt failed, task can not be edite in 'Done' column");
                return false;
            }
            else
            {
                Task task = currentUser.GetCurrentBoard().FindTask(taskID);
                task.SetTitle(title);
                log.Info("Changing title attempt successfull");
                Serialization.SaveTask(taskID, task.GetCreationTime().ToString("dd/MM/yyyy"), task.GetDueDate().ToString("dd/MM/yyyy"), title, task.GetDescription(), task.GetColumn(), currentUser.GetCurrentBoard().GetName(), currentUser.GetEmail());
                if (task.GetTitle() != title)
                    log.Error("title has been set but hasn't changed");
                return true;
            }
        }
        /**
        * a function that changes the description of a task
        */
        public static bool ChangeDescription(string description, string taskID)
        {
            if (!(ValidDescription(description)))
            {
                log.Warn("Changing description attempt failed, Invalid description was submitted");
                return false;
            }
            else if (!(currentUser.GetCurrentBoard().ContainsTask(taskID)))
            {
                log.Warn("Changing description attempt failed, task does not exist in board");
                return false;
            }
            else if (currentUser.GetCurrentBoard().FindTask(taskID).GetColumn() == currentUser.GetCurrentBoard().ColumnsOrder[currentUser.GetCurrentBoard().ColumnsOrder.Length - 1])
            {
                log.Warn("Changing description attempt failed, task can not be edite in 'Done' column");
                return false;
            }
            else
            {
                Task task = currentUser.GetCurrentBoard().FindTask(taskID);
                task.SetDescription(description);
                log.Info("Changing description attempt successfull");
                Serialization.SaveTask(taskID, task.GetCreationTime().ToString("dd/MM/yyyy"), task.GetDueDate().ToString("dd/MM/yyyy"), task.GetTitle(), description, task.GetColumn(), currentUser.GetCurrentBoard().GetName(), currentUser.GetEmail());
                if (task.GetTitle() != description)
                    log.Error("description has been set but hasn't changed");
                return true;
            }
        }
        /**
        * a function that changes the duedate of a task
        */
        public static bool ChangeDueDate(string date, string taskID)
        {
            if (!(currentUser.GetCurrentBoard().ContainsTask(taskID)))
            {
                log.Warn("Changing duedate attempt failed, task does not exist in board");
                return false;
            }
            else if (!(ValidDueDate(date)))
            {
                log.Warn("Changing duedate attempt failed, Invalid duedate was submitted");
                return false;
            }
            else if (currentUser.GetCurrentBoard().FindTask(taskID).GetColumn() == currentUser.GetCurrentBoard().ColumnsOrder[currentUser.GetCurrentBoard().ColumnsOrder.Length - 1])
            {
                log.Warn("Changing duedate attempt failed, task can not be edite in 'Done' column");
                return false;
            }
            else
            {
                DateTime dueDate = new DateTime(Convert.ToInt32(date.Substring(6, 4)), Convert.ToInt32(date.Substring(3, 2)), Convert.ToInt32(date.Substring(0, 2)));
                Task task = currentUser.GetCurrentBoard().FindTask(taskID);
                task.SetDueDate(dueDate);
                if (task.GetDueDate() != dueDate)
                {
                    log.Error("Date has been set but hasn't changed");
                }
                else
                {
                    log.Info("Changing duedate attempt successfull");
                }
                Serialization.SaveTask(taskID, task.GetCreationTime().ToString("dd/MM/yyyy"), date, task.GetTitle(), task.GetDescription(), task.GetColumn(), currentUser.GetCurrentBoard().GetName(), currentUser.GetEmail());

                return true;
            }
        }
        /**
         * a fuction that loads the user data from the harddrive
         */
        public static bool startup()
        {
            log4net.Config.XmlConfigurator.Configure();
            USERS = Serialization.LoadData();
            
            return true;
        }

    }
}
