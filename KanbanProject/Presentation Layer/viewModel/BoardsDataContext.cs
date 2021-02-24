using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using KanbanProject.Interface_Layer;

namespace KanbanProject.Presentation_Layer.viewModel
{
    public class BoardsDataContext : INotifyPropertyChanged
    {
        string board = "";
        public string Board
        {
            get
            {
                return board;
            }
            set
            {
                board = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Board"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        Service service;

        public BoardsDataContext()
        {
            // emulating some registered users, this naturally shouldnt be here.
            service = new Service();
            showTheard1();
        }

        public bool AddBoard()
        {
            return service.addBoard(this.board);
        }
        public bool RemoveBoard()
        {
            return service.removeBoard(this.board);
        }
        public bool ShowBoard()
        {
            return service.ShowBoard(this.board);
        }

        private BoardsWindowColumn selectBoard;
        public BoardsWindowColumn SelectBoard
        {
            get { return selectBoard; }
            set
            {
                selectBoard = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectBoard"));
                }
            }
        }
        private ICollectionView gridBoard;
        public ICollectionView GridBoard
        {
            get { return gridBoard; }
            set
            {
                gridBoard = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("GridBoard"));
                }
            }
        }
        private ObservableCollection<BoardsWindowColumn> boards;
        public ObservableCollection<BoardsWindowColumn> Boards
        {
            get { return boards; }
            set
            {
                boards = value;
                UpdateFilterBoards();
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Boards"));
                }
            }
        }
        private void UpdateFilterBoards()
        {
            CollectionViewSource cvs = new CollectionViewSource() { Source = boards };
            ICollectionView cv = cvs.View;
            //        cv.Filter = o =>
            //         {
            //           BoardWindowRow p = o as BoardWindowRow;
            //         return (p.Title.ToUpper().Contains(searchTerm.ToUpper()) | p.Description.ToUpper().Contains(searchTerm.ToUpper()));
            //   };
            GridBoard = cv;
        }
        public void showTheard1()
        {
            ObservableCollection<BoardsWindowColumn> bors = new ObservableCollection<BoardsWindowColumn>();
            foreach (KeyValuePair<string,Board> c in service.GetBoards())
            {
                bors.Add(new BoardsWindowColumn(c.Key,c.Value.GetColumns().Count));
            }
            boards = bors;
            UpdateFilterBoards();
        }
    }
}
