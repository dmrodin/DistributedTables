using System;
using System.Collections.Generic;

#nullable disable

namespace TableStorage
{
    public partial class Cell
    {
        public Guid Id { get; set; }
        public Guid? TableId { get; set; }
        public Guid? ColumnId { get; set; }
        public Guid? RowId { get; set; }
        public string Value { get; set; }

        public virtual Column Column { get; set; }
        public virtual Row Row { get; set; }
        public virtual Table Table { get; set; }
    }
}
