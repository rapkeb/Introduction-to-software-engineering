using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using KanbanProject.Interface_Layer;

namespace KanbanSolution.Presentation_Layer.viewModel
{
    public class BoardWindowDataContext : INotifyPropertyChanged
    {
        string searchTerm = "";
        public string SearchTerm
        {
            get { return searchTerm; }
            set
            {
                searchTerm = value;
                UpdateFilterTasks();
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SearchTerm"));
                }
            }
        }

        private BoardWindowColumn selectColumn;
        public BoardWindowColumn SelectColumn
        {
            get { return selectColumn; }
            set
            {
                selectColumn = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectColumn"));
                }
            }
        }

        private BoardWindowRow selectRow;
        public BoardWindowRow SelectRow
        {
            get { return selectRow; }
            set
            {
                selectRow = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectRow"));
                }
            }
        }

        private ICollectionView gridView;
        public ICollectionView GridView
        {
            get { return gridView; }
            set
            {
                gridView = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("GridView"));
                }
            }
        }
        private ICollectionView gridcolumn;
        public ICollectionView GridColumn
        {
            get { return gridcolumn; }
            set
            {
                gridcolumn = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("GridColumn"));
                }
            }
        }

        private ObservableCollection<BoardWindowColumn> columns;
        public ObservableCollection<BoardWindowColumn> Columns
        {
            get { return columns; }
            set
            {
                columns = value;
                UpdateFilterColumns();
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Columns"));
                }
            }
        }

        private ObservableCollection<BoardWindowRow> tasks;
        public ObservableCollection<BoardWindowRow> Tasks
        {
            get { return tasks; }
            set
            {
                tasks = value;
                UpdateFilterTasks();
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Tasks"));
                }
            }
        }

        private void UpdateFilterTasks()
        {
            CollectionViewSource cvs = new CollectionViewSource() { Source = tasks };
            ICollectionView cv = cvs.View;
   //        cv.Filter = o =>
 //         {
   //           BoardWindowRow p = o as BoardWindowRow;
     //         return (p.Title.ToUpper().Contains(searchTerm.ToUpper()) | p.Description.ToUpper().Contains(searchTerm.ToUpper()));
       //   };
            GridView = cv;
        }
        private void UpdateFilterColumns()
        {
            CollectionViewSource cvs = new CollectionViewSource() { Source = columns };
            ICollectionView cv = cvs.View;
            //        cv.Filter = o =>
            //        {
            //            BoardWindowRow p = o as BoardWindowRow;
            //            return (p.Title.ToUpper().Contains(searchTerm.ToUpper()) | p.Description.ToUpper().Contains(searchTerm.ToUpper()));
            //        };
                GridColumn = cv;
        }

        Service service;

        public BoardWindowDataContext()
        {
            service = new Service();
            showTheard();
        }

        public void showTheard()
        {
            ObservableCollection<BoardWindowRow> rows = new ObservableCollection<BoardWindowRow>();
            ObservableCollection<BoardWindowColumn> cols = new ObservableCollection<BoardWindowColumn>();
            foreach (var c in service.GetBoard().ColumnsOrder)
            {
                if (c != null)
                {
                    foreach (var t in service.GetBoard().GetColumns()[c].GetTasks())
                        rows.Add(new BoardWindowRow(c, t.Value.GetTaskID(), t.Value.GetCreationTime().ToString("dd/MM/yyyy"), t.Value.GetDueDate().ToString("dd/MM/yyyy"), t.Value.GetTitle(), t.Value.GetDescription())); //rows));
                    cols.Add(new BoardWindowColumn(c, service.GetBoard().GetColumns()[c].GetTasks().Count, service.GetBoard().GetColumns()[c].GetMountOfTasks()));
                }
            }
            tasks = rows;
            columns = cols;
            UpdateFilterTasks();
            UpdateFilterColumns();
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}