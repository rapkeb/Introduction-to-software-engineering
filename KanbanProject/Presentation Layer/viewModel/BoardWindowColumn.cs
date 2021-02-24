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
    public class BoardWindowColumn
    {
        public string ColumnName {get; set;}
        public int NumOfTasks { get; set; }
        public int LimitTasks { get; set; }

        public BoardWindowColumn(string ColumnName, int NumOfTasks, int LimitTasks)
        {
            this.ColumnName = ColumnName;
            this.NumOfTasks = NumOfTasks;
            this.LimitTasks = LimitTasks;
        }
    }
}