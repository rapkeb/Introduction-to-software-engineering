using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanProject.Interface_Layer;

namespace KanbanSolution.Presentation_Layer.viewModel
{
    public class BoardWindowRow
    {
        public string NameOfColumn { get; set; }
        public string TaskID { get; set; }
        public string CreationDate { get; set; }
        public string DueDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public BoardWindowRow(string NameOfColumn, string TaskID, string CreationDate, string DueDate, string Title, string Description)
        {
            this.NameOfColumn = NameOfColumn;
            this.TaskID = TaskID;
            this.CreationDate = CreationDate;
            this.DueDate = DueDate;
            this.Title = Title;
            this.Description = Description;
        }
    }
}