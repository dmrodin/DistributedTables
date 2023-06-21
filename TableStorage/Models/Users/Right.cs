﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableStorage.Enums;

namespace TableStorage.Models.Users
{
    public class Right
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public EnumRights RightValue { get; set; }
    }
}
