
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace KanbanProject
{

    public static class Serialization
    {

        public static void SaveTask(string taskID, string creationTime, string dueDate, string title, string description, string column, string board, string user)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(GetConnenctionString()))
            {
                try
                {
                    cnn.Open();
                    SQLiteCommand command = new SQLiteCommand();
                    command.Connection = cnn;
                    command.CommandTimeout = 0;
                    command.CommandText = "INSERT OR REPLACE INTO [Tasks] (TaskID, CreationTime, DueDate, Title, Description, NameOfColumn, NameOfBoard, UserEmail) VALUES" +
                        "(@TaskID, @CreationTime, @DueDate, @Title, @Description, @NameOfColumn, @NameOfBoard, @UserEmail)";
                    command.Parameters.Add(new SQLiteParameter("@TaskID", taskID));
                    command.Parameters.Add(new SQLiteParameter("@CreationTime", creationTime));
                    command.Parameters.Add(new SQLiteParameter("@DueDate", dueDate));
                    command.Parameters.Add(new SQLiteParameter("@Title", title));
                    command.Parameters.Add(new SQLiteParameter("@Description", description));
                    command.Parameters.Add(new SQLiteParameter("@NameOfColumn", column));
                    command.Parameters.Add(new SQLiteParameter("@NameOfBoard", board));
                    command.Parameters.Add(new SQLiteParameter("@UserEmail", user));
                    command.ExecuteNonQuery();
                    cnn.Close();
                }
                catch
                {

                }
                //                cnn.Execute("insert or replace into Tasks (TaskID, CreationTime, DueDate, Title, Description, Column, Board, UserEmail)", new string[] {taskID, creationTime, dueDate, title, description, column, board, user });
            }
        }

        public static void SaveUser(string email, string password)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(GetConnenctionString()))
            {
                try
                {
                    cnn.Open();
                    SQLiteCommand command = new SQLiteCommand();
                    command.Connection = cnn;
                    command.CommandTimeout = 0;
                    command.CommandText = "INSERT OR REPLACE INTO [Users] (Email, Password) VALUES (@Email, @Password)";
                    command.Parameters.Add(new SQLiteParameter("@Email", email));
                    command.Parameters.Add(new SQLiteParameter("@Password", password));
                    command.ExecuteNonQuery();
                    cnn.Close();
                }
                catch
                {

                }
            }
        }

        public static void SaveBoard(string nameOfBoard, string userEmail)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(GetConnenctionString()))
            {
                try
                {
                    cnn.Open();
                    SQLiteCommand command = new SQLiteCommand();
                    command.Connection = cnn;
                    command.CommandTimeout = 0;
                    command.CommandText = "INSERT OR REPLACE INTO [Boards] (NameOfBoard, UserEmail) VALUES (@NameOfBoard, @UserEmail)";
                    command.Parameters.Add(new SQLiteParameter("@UserEmail", userEmail));
                    command.Parameters.Add(new SQLiteParameter("@NameOfBoard", nameOfBoard));
                    command.ExecuteNonQuery();
                    cnn.Close();
                }
                catch
                {

                }
            }
        }
        public static void SaveColumn(string userEmail, string nameOfColumn, string nameOfBoard, int numOfTasks, int position)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(GetConnenctionString()))
            {
                try
                {
                    cnn.Open();
                    SQLiteCommand command = new SQLiteCommand();
                    command.Connection = cnn;
                    command.CommandTimeout = 0;
                    command.CommandText = "INSERT OR REPLACE INTO [Columns] (NameOfColumn, UserEmail, NameOfBoard,NumOfTasks, Position) VALUES (@NameOfColumn, @UserEmail, @NameOfBoard, @NumOfTasks, @Position)";
                    command.Parameters.Add(new SQLiteParameter("@UserEmail", userEmail));
                    command.Parameters.Add(new SQLiteParameter("@NameOfColumn", nameOfColumn));
                    command.Parameters.Add(new SQLiteParameter("@NameOfBoard", nameOfBoard));
                    command.Parameters.Add(new SQLiteParameter("@Position", position));
                    command.Parameters.Add(new SQLiteParameter("@NumOfTasks", numOfTasks));
                    command.ExecuteNonQuery();
                    cnn.Close();
                }
                catch
                {

                }
            }
        }
        public static void ChangeColumn(string userEmail, string nameOfColumn, string nameOfBoard, int numOfTasks, int position)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(GetConnenctionString()))
            {
                cnn.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = cnn;
                command.CommandTimeout = 0;
                command.CommandText = "Update [Columns] SET NumOfTasks = '" + numOfTasks + "' WHERE (NameOfColumn = '" + nameOfColumn + "') AND (NameOfBoard = '" + nameOfBoard + "') AND (UserEmail = '" + userEmail + "')"; 
                command.ExecuteNonQuery();
                cnn.Close();
            }
        }
        public static void EditColumn(string userEmail, string nameOfColumn, string nameOfBoard)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(GetConnenctionString()))
            {
                try
                {
                    cnn.Open();
                    string sql = "Delete FROM [Columns] WHERE (NameOfColumn = '" + nameOfColumn + "') AND (NameOfBoard = '" + nameOfBoard + "') AND (UserEmail = '" + userEmail + "')";
                    SQLiteCommand cm = new SQLiteCommand(sql, cnn);
                    cm.ExecuteNonQuery();
                    cnn.Close();
                }
                catch
                {

                }
            }
        }
        private static Dictionary<string, User> LoadUsers()
        {
            Dictionary<string, User> current = new Dictionary<string, User>();
            using (SQLiteConnection cnn = new SQLiteConnection(GetConnenctionString()))
            {
                try
                {
                    cnn.Open();
                    string sql = "SELECT * FROM [Users]";
                    SQLiteCommand command = new SQLiteCommand(sql, cnn);
                    SQLiteDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        User user = new User(dr["Email"].ToString(), dr["Password"].ToString());
                        current.Add(dr["Email"].ToString(), user);
                    }
                    command.ExecuteNonQuery();
                    cnn.Close();
                }
                catch
                {
                    current = current;
                }
            }
            return current;
        }
        private static LinkedList<Board> LoadBoards()
        {
            LinkedList<Board> current = new LinkedList<Board>();
            using (SQLiteConnection cnn = new SQLiteConnection(GetConnenctionString()))
            {
                try
                {
                    cnn.Open();
                    string sql = "SELECT * FROM [Boards]";
                    SQLiteCommand command = new SQLiteCommand(sql, cnn);
                    SQLiteDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        Board board = new Board(dr["UserEmail"].ToString(), dr["NameOfBoard"].ToString(),0);
                        current.AddLast(board);
                    }
                    command.ExecuteNonQuery();
                    cnn.Close();
                }
                catch
                {
                    current = current;
                }
            }
            return current;
        }
        private static Dictionary<string, User> mergeUserBoards()
        {
            LinkedList<Board> current = LoadBoards();
            Dictionary<string, User> tmp = LoadUsers();
            foreach (Board board in current)
            {
                tmp[board.GetUserMail()].AddBoard2(board.GetName());
            }
            return tmp;
        }
        private static Dictionary<string, User> LoadColumns()
        {
            Dictionary<string, User> tmp = mergeUserBoards();
            using (SQLiteConnection cnn = new SQLiteConnection(GetConnenctionString()))
            {
                try
                {
                    cnn.Open();
                    string sql = "SELECT * FROM [Columns]";
                    SQLiteCommand command = new SQLiteCommand(sql, cnn);
                    SQLiteDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        Column current = new Column(dr["NameOfColumn"].ToString());
                        current.SetMountOfTasks(Convert.ToInt32(dr["NumOfTasks"].ToString()));
                        tmp[dr["UserEmail"].ToString()].GetBoards()[dr["NameOfBoard"].ToString()].AddColumn(dr["NameOfColumn"].ToString(), current, int.Parse(dr["Position"].ToString()));
                    }
                    command.ExecuteNonQuery();
                    cnn.Close();
                }
                catch
                {
                    tmp = tmp;
                }
            }
            return tmp;
        }
        public static Dictionary<string, User> LoadData()
        {
                long numOfTasks = 0;
                Dictionary<string, User> tmp = LoadColumns();
                using (SQLiteConnection cnn = new SQLiteConnection(GetConnenctionString()))
                {
                try
                {
                    cnn.Open();
                    string sql = "SELECT * FROM [Tasks]";
                    SQLiteCommand command = new SQLiteCommand(sql, cnn);
                    SQLiteDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        Task task = new Task(dr["TaskID"].ToString(), DateTime.Parse(dr["DueDate"].ToString()), DateTime.Parse(dr["CreationTime"].ToString()), dr["Title"].ToString(), dr["Description"].ToString(), dr["NameOfColumn"].ToString());
                        tmp[dr["UserEmail"].ToString()].GetBoards()[dr["NameOfBoard"].ToString()].GetColumns()[dr["NameOfColumn"].ToString()].AddTask(dr["TaskID"].ToString(), task);
                        if (Convert.ToInt32(numOfTasks) < Convert.ToInt32(task.GetTaskID()))
                        {
                            numOfTasks = Convert.ToInt64(task.GetTaskID());
                        }
                    }
                    command.ExecuteNonQuery();
                    cnn.Close();
                }
                catch
                {

                }
                }
                Mainframe.TOTALNUMOFTASKS = numOfTasks + 10000000001;
                return tmp;
            }
        public static void RemoveTask(string taskID)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(GetConnenctionString()))
            {
                cnn.Open();
                string sql = "Delete FROM [Tasks] WHERE TaskID = '" + taskID + "'";
                SQLiteCommand cm = new SQLiteCommand(sql, cnn);
                cm.ExecuteNonQuery();
                cnn.Close();
            }
        }
        public static void RemoveColumn(string nameOfColumn,string board,string user)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(GetConnenctionString()))
            {
                cnn.Open();
                string sql = "Delete FROM [Columns] WHERE (NameOfColumn = '" + nameOfColumn + "') AND (NameOfBoard = '" + board + "') AND (UserEmail = '" + user + "')" ;
                SQLiteCommand cm = new SQLiteCommand(sql, cnn);
                cm.ExecuteNonQuery();
                cm.Prepare();
                sql = "Delete FROM [Tasks] WHERE (NameOfColumn = '" + nameOfColumn + "') AND (NameOfBoard = '" + board + "') AND (UserEmail = '" + user + "')";
                cm = new SQLiteCommand(sql, cnn);
                cm.ExecuteNonQuery();
                cnn.Close();
            }
        }
        public static void RemoveBoard(string board, string user)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(GetConnenctionString()))
            {
                cnn.Open();
                string sql = "Delete FROM [Boards] WHERE (NameOfBoard = '" + board + "') AND (UserEmail = '" + user + "')";
                SQLiteCommand cm = new SQLiteCommand(sql, cnn);
                cm.ExecuteNonQuery();
                cm.Prepare();
                sql = "Delete FROM [Columns] WHERE (NameOfBoard = '" + board + "') AND (UserEmail = '" + user + "')";
                cm = new SQLiteCommand(sql, cnn);
                cm.ExecuteNonQuery();
                cm.Prepare();
                sql = "Delete FROM [Tasks] WHERE (NameOfBoard = '" + board + "') AND (UserEmail = '" + user + "')";
                cm = new SQLiteCommand(sql, cnn);
                cm.ExecuteNonQuery();
                cnn.Close();
            }
        }
        private static string GetConnenctionString(string id = "DB")
            {
                return ConfigurationManager.ConnectionStrings[id].ConnectionString;
            }

        }
    }
