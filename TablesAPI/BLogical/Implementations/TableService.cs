using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TablesAPI.BLogical.Interfaces;
using TableStorage.Constants;
using TableStorage.Enums;
using TableStorage.Interfaces;
using TableStorage.View;

namespace TablesAPI.BLogical.Implementations
{
    public class TableService : ITableService
    {
        private readonly IRepository _repository;
        public TableService(IRepository repository)
        {
            _repository = repository;
        }

        public void ChangeCell(CellView cellView)
        {
            _repository.ChangeCell(cellView);
        }

        public Guid CreateTable(TableView tableView)
        {
            return _repository.CreateTable(tableView);
        }

        public void DeleteTable(Guid id)
        {
            _repository.DeleteTable(id);
        }

        public Guid DownloadExcel(string filePath, string tableName)
        {
            Workbook wb = new Workbook(filePath);

            Worksheet worksheet = wb.Worksheets[0];

            int rowsCount = worksheet.Cells.MaxDataRow + 1 > 30 ? 30 : worksheet.Cells.MaxDataRow + 1; // TODO: заглушка 
            int columnsCount = worksheet.Cells.MaxDataColumn + 1;

            string[] arrColumnsNames = new string[columnsCount + 1];

            arrColumnsNames[0] = ColumnNames.Id;
            var idColumn = new ColumnWithPropertiesView();
            idColumn.Name = ColumnNames.Id;
            var columns = new List<ColumnWithPropertiesView>
            {
                idColumn
            };

            for (int i = 0; i < columnsCount; i++)
            {
                var column = new ColumnWithPropertiesView();
                var columnName = worksheet.Cells[0, i].Value.ToString();
                arrColumnsNames[i + 1] = columnName;
                column.Name = columnName;
                column.Properties = CreateColumnProperties(worksheet.Cells[1, i], worksheet.Cells.Columns[i]);
                columns.Add(column);
            }

            Dictionary<int, Dictionary<string, string>> records = new Dictionary<int, Dictionary<string, string>>();

            for (int i = 1; i < rowsCount; i++)
            {
                Dictionary<string, string> columnsDict = new Dictionary<string, string>();

                for (int j = 0; j <= columnsCount; j++)
                {
                    string value;
                    if (j == 0)
                    {
                        value = i.ToString();
                    }
                    else
                    {
                        var cellValue = worksheet.Cells[i, j - 1].Value;
                        value = cellValue == null ? "" : cellValue.ToString();
                    }

                    columnsDict.Add(arrColumnsNames[j], value);
                }

                records.Add(i, columnsDict);
            }

            TableView tableView = new TableView();

            tableView.Title = tableName;
            tableView.Records = records;
            tableView.Columns = columns;

            Guid tableId = _repository.CreateTable(tableView);

            return tableId;
        }

        public TableView GetTable(Guid id)
        {
            return _repository.GetTable(id);
        }

        public TableDesription GetTableDesription(Guid tableId)
        {
            return _repository.GetTableDesription(tableId);
        }

        public List<TableDesription> GetTableDesriptions()
        {
            return _repository.GetTableDesriptionsList();
        }

        private IEnumerable<ColumnPropertyView> CreateColumnProperties(Cell cell, Column column)
        {
            var properties = new List<ColumnPropertyView>();
            var value = EnumPropertyValues.Null;
            switch (cell.Type)
            {
                case CellValueType.IsNumeric: value = cell.IntValue == default ? EnumPropertyValues.Float : EnumPropertyValues.Int; break;
                case CellValueType.IsBool: value = EnumPropertyValues.Bool; break;
                case CellValueType.IsDateTime: value = EnumPropertyValues.Date; break;
                case CellValueType.IsString: value = EnumPropertyValues.String; break;
            }
            var typeProperty = new ColumnPropertyView
            {
                EnumProperty = EnumProperties.Type,
                Value = value
            };
            var enableProperty = new ColumnPropertyView
            {
                EnumProperty = EnumProperties.Enable,
                Value = cell.GetStyle().IsLocked ? EnumPropertyValues.False : EnumPropertyValues.True
            };
            var visibleProperty = new ColumnPropertyView
            {
                EnumProperty = EnumProperties.Visible,
                Value = column.IsHidden ? EnumPropertyValues.False : EnumPropertyValues.True
            };
            properties.Add(typeProperty);
            properties.Add(enableProperty);
            properties.Add(visibleProperty);
            return properties;
        }
    }
}
