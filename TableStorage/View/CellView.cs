using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableStorage.View
{
    public class CellView
    {
        public Guid TableId { get; set; }
        public int RowIndex { get; set; }
        public string ColumnName { get; set; }
        public string Value { get; set; }
    }
}
