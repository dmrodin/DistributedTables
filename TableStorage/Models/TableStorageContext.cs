using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TableStorage.Models.Users;

#nullable disable

namespace TableStorage
{
    public partial class TableStorageContext : DbContext
    {
        public TableStorageContext()
        {
        }

        public TableStorageContext(DbContextOptions<TableStorageContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }

        public virtual DbSet<Cell> Cells { get; set; }
        public virtual DbSet<Column> Columns { get; set; }
        public virtual DbSet<ColumnProperty> ColumnProperties { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Row> Rows { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<TableType> TableTypes { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Right> Rights { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
    }
}
