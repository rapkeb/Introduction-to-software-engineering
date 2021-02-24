using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.Presentation_Layer.viewModel
{
    public class BoardsWindowColumn
    {
        public string BoardName { get; set; }
        public int NumOfColumns { get; set; }

        public BoardsWindowColumn(string BoardName, int NumOfColumns)
        {
            this.BoardName = BoardName;
            this.NumOfColumns = NumOfColumns;
        }
    }
}
