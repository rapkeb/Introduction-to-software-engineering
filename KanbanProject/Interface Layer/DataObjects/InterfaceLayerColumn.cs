using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanSolution.Interface_Layer.DataObjects
{
    class InterfaceLayerColumn
    {
        private string name;
        private IReadOnlyCollection<InterfaceLayerTask> tasks;

        public string Name { get => name; set => name = value; }
        public IReadOnlyCollection<InterfaceLayerTask> Tasks { get => tasks; set => tasks = value; }

        public InterfaceLayerColumn(string name, IReadOnlyCollection<InterfaceLayerTask> tasks)
        {
            this.name = name;
            this.tasks = tasks;
        }
    }
}
