using System;
using System.Collections.Generic;
using TableStorage.Enums;

#nullable disable

namespace TableStorage
{
    public partial class ColumnProperty
    {
        public Guid Id { get; set; }
        public Guid? ColumnId { get; set; }
        public Guid? PropertyId { get; set; }
        public EnumPropertyValues Value { get; set; }

        public virtual Column Column { get; set; }
        public virtual Property Property { get; set; }
    }
}
