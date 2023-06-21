using System;
using System.Collections.Generic;

#nullable disable

namespace TableStorage
{
    public partial class Table
    {
        public Table()
        {
            Cells = new HashSet<Cell>();
            Columns = new HashSet<Column>();
            Rows = new HashSet<Row>();
            TableTypes = new HashSet<TableType>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Cell> Cells { get; set; }
        public virtual ICollection<Column> Columns { get; set; }
        public virtual ICollection<Row> Rows { get; set; }
        public virtual ICollection<TableType> TableTypes { get; set; }
    }
}
