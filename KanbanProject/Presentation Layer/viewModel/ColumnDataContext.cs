using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanProject.Interface_Layer;
using System.ComponentModel;

namespace KanbanProject.Presentation_Layer.viewModel
{
    public class ColumnDataContext : INotifyPropertyChanged
    {
        string column = "";
        public string Column
        {
            get
            {
                return column;
            }
            set
            {
                column = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("column"));
            }
        }
        int position;
        public int Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("position"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        Service service;

        public ColumnDataContext()
        {
            // emulating some registered users, this naturally shouldnt be here.
            service = new Service();
        }

        public bool AddColumn()
        {
            return service.AddColumn(this.column, this.position);
        }
        public bool RemoveColumn()
        {
            return service.RemoveColumn(this.column);
        }
        public bool MoveColumn()
        {
            return service.MoveColumn(this.column,this.position);
        }
    }
}
