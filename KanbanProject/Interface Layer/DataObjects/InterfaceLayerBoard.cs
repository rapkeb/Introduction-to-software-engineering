using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanSolution.Interface_Layer.DataObjects
{
    class InterfaceLayerBoard
    {
        private string name;
        private string userEmail;
        private IReadOnlyCollection<InterfaceLayerColumn> columns;

        public string Name { get => name; set => name = value; }
        public string UserEmail { get => userEmail; set => userEmail = value; }
        public IReadOnlyCollection<InterfaceLayerColumn> Columns { get => columns; set => columns = value; }

        public InterfaceLayerBoard(string userEmail, IReadOnlyCollection<InterfaceLayerColumn> columns, string name)
        {
            this.name = name;
            this.userEmail = userEmail;
            this.columns = columns;
        }
    }
}
