using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject
{

    public class User
    {
        private Board currentBoard = null;
        private string email;
        private string password;
        private Dictionary<String, Board>  boards;
        private Boolean loggedIn = false;

        /**
         * defult constructor
         */
        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
            boards = new Dictionary<string, Board>();
        }
        /**
         * a constructor to recreate a user
         */
        public User(string email, string password, Dictionary<String, Board> boards)
        {
            this.email = email;
            this.password = password;
            this.boards = boards;
        }
        /*
         * getters and setter (login/logout are basically SetLoggedIn)
         */
        public void Login(string email, string password)
        {
            this.loggedIn = true;
        }
        public void Logout()
        {
            loggedIn = false;
        }
        public string GetEmail()
        {
            return this.email;
        }
        public void SetEmail(string email)
        {
            this.email = email;
        }
        public string GetPassword()
        {
            return this.password;
        }
        public void SetPassword(string password)
        {
            this.password = password;
        }
        public Board GetCurrentBoard()
        {
            return this.currentBoard;
        }
        public void SetCurrentBoard(Board board)
        {
            this.currentBoard = board;
        }
        public void SetCurrentBoardToNull()
        {
            this.currentBoard = null;
        }
        public Boolean GetLoggedIn()
        {
            return this.loggedIn;
        }
        public Dictionary<String,Board> GetBoards()
        {
            return this.boards;
        }
        public void AddBoard(String nameOfBoard)
        {
            this.boards.Add(nameOfBoard,new Board(this.email,nameOfBoard));
        }
        public void AddBoard2(String nameOfBoard)
        {
            this.boards.Add(nameOfBoard, new Board(this.email, nameOfBoard,0));
        }
        public void RemoveBoard(String nameOfBoard)
        {
            this.boards.Remove(nameOfBoard);
        }
        public void ChooseBoard(String nameOfBoard)
        {
            this.currentBoard = this.boards[nameOfBoard];
        }
    }
}
