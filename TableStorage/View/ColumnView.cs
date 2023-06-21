using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableStorage.View
{
    public class ColumnView
    {
        public Guid ColumnId { get; set; }
        public string Name { get; set; } 
        public List<ColumnProperty> ColumnProperties { get; set; }
    }
}
