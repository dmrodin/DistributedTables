using System;
using System.Collections.Generic;

#nullable disable

namespace TableStorage
{
    public partial class Column
    {
        public Column()
        {
            Cells = new HashSet<Cell>();
            ColumnProperties = new HashSet<ColumnProperty>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? TableId { get; set; }

        public virtual Table Table { get; set; }
        public virtual ICollection<Cell> Cells { get; set; }
        public virtual ICollection<ColumnProperty> ColumnProperties { get; set; }
    }
}
