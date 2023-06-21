using System;
using System.Collections.Generic;

#nullable disable

namespace TableStorage
{
    public partial class Row
    {
        public Row()
        {
            Cells = new HashSet<Cell>();
        }

        public Guid Id { get; set; }
        public int Index { get; set; }
        public Guid? TableId { get; set; }

        public virtual Table Table { get; set; }
        public virtual ICollection<Cell> Cells { get; set; }
    }
}
