using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableStorage.View
{
    public class TableDesription
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Type Type { get; set; }
        public List<ColumnView> ColumnViews { get; set; }
    }
}
