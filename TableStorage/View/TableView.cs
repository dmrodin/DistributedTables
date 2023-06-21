using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableStorage.View
{
    public class TableView
    {
        public Guid TableId { get; set; }
        public string Title { get; set; }
        public Type Type { get; set; }
        public IEnumerable<ColumnWithPropertiesView> Columns { get; set; }
        public Dictionary<int, Dictionary<string, string>> Records { get; set; }

    }
}
