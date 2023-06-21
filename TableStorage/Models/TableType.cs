using System;
using System.Collections.Generic;

#nullable disable

namespace TableStorage
{
    public partial class TableType
    {
        public Guid Id { get; set; }
        public Guid? TableId { get; set; }
        public Guid? TypeId { get; set; }

        public virtual Table Table { get; set; }
        public virtual Type Type { get; set; }
    }
}
