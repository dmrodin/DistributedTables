using System;
using System.Collections.Generic;
using TableStorage.Enums;

#nullable disable

namespace TableStorage
{
    public partial class Property
    {
        public Guid Id { get; set; }
        public EnumProperties EnumProperty { get; set; }
    }
}
