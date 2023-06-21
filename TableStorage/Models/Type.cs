using System;
using System.Collections.Generic;

#nullable disable

namespace TableStorage
{
    public partial class Type
    {
        public Type()
        {
            TableTypes = new HashSet<TableType>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TableType> TableTypes { get; set; }
    }
}
