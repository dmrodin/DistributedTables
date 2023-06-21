﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TableStorage;

namespace TableStorage.Migrations
{
    [DbContext(typeof(TableStorageContext))]
    [Migration("20230517092357_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TableStorage.Cell", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ColumnId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RowId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TableId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ColumnId");

                    b.HasIndex("RowId");

                    b.HasIndex("TableId");

                    b.ToTable("Cells");
                });

            modelBuilder.Entity("TableStorage.Column", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TableId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.ToTable("Columns");
                });

            modelBuilder.Entity("TableStorage.ColumnProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ColumnId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("PropertyId1")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ColumnId");

                    b.HasIndex("PropertyId1");

                    b.ToTable("ColumnProperties");
                });

            modelBuilder.Entity("TableStorage.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EnumProperty")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("TableStorage.Row", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<Guid?>("TableId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.ToTable("Rows");
                });

            modelBuilder.Entity("TableStorage.Table", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("TableStorage.TableType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TableId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.HasIndex("TypeId");

                    b.ToTable("TableTypes");
                });

            modelBuilder.Entity("TableStorage.Type", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("TableStorage.Cell", b =>
                {
                    b.HasOne("TableStorage.Column", "Column")
                        .WithMany("Cells")
                        .HasForeignKey("ColumnId");

                    b.HasOne("TableStorage.Row", "Row")
                        .WithMany("Cells")
                        .HasForeignKey("RowId");

                    b.HasOne("TableStorage.Table", "Table")
                        .WithMany("Cells")
                        .HasForeignKey("TableId");

                    b.Navigation("Column");

                    b.Navigation("Row");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("TableStorage.Column", b =>
                {
                    b.HasOne("TableStorage.Table", "Table")
                        .WithMany("Columns")
                        .HasForeignKey("TableId");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("TableStorage.ColumnProperty", b =>
                {
                    b.HasOne("TableStorage.Column", "Column")
                        .WithMany("ColumnProperties")
                        .HasForeignKey("ColumnId");

                    b.HasOne("TableStorage.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId1");

                    b.Navigation("Column");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("TableStorage.Row", b =>
                {
                    b.HasOne("TableStorage.Table", "Table")
                        .WithMany("Rows")
                        .HasForeignKey("TableId");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("TableStorage.TableType", b =>
                {
                    b.HasOne("TableStorage.Table", "Table")
                        .WithMany("TableTypes")
                        .HasForeignKey("TableId");

                    b.HasOne("TableStorage.Type", "Type")
                        .WithMany("TableTypes")
                        .HasForeignKey("TypeId");

                    b.Navigation("Table");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("TableStorage.Column", b =>
                {
                    b.Navigation("Cells");

                    b.Navigation("ColumnProperties");
                });

            modelBuilder.Entity("TableStorage.Row", b =>
                {
                    b.Navigation("Cells");
                });

            modelBuilder.Entity("TableStorage.Table", b =>
                {
                    b.Navigation("Cells");

                    b.Navigation("Columns");

                    b.Navigation("Rows");

                    b.Navigation("TableTypes");
                });

            modelBuilder.Entity("TableStorage.Type", b =>
                {
                    b.Navigation("TableTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
