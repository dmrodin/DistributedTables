using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableStorage.Constants;
using TableStorage.Enums;
using TableStorage.Interfaces;
using TableStorage.View;

namespace TableStorage.Implementations
{
    public class Repository : IRepository
    {
        private readonly TableStorageContext _dbContext;

        public Repository(TableStorageContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Public methods

        public void ChangeCell(Guid id, string value)
        {
            var cell = _dbContext.Cells.Where(c => c.Id == id).FirstOrDefault();

            cell.Value = value;

            _dbContext.SaveChanges();
        }

        public void ChangeCell(CellView cellView)
        {
            Cell cell = _dbContext.Cells.Where(c => c.TableId == cellView.TableId && c.Row.Index == cellView.RowIndex && c.Column.Name == cellView.ColumnName).FirstOrDefault();

            cell.Value = cellView.Value;

            _dbContext.SaveChanges();
        }

        public Guid CreateTable(TableView tableView)
        {
            Table table = new Table();
            TableType type = new TableType();

            type.Type = tableView.Type;

            table.Name = tableView.Title;
            var tableEntity = _dbContext.Tables.Add(table);

            Guid tableId = tableEntity.Entity.Id;

            var createdColDict = CreateColumns(tableView.Columns
                .Select(column => column.Name)
                .ToArray(), tableId);
            tableView.Columns = tableView.Columns
                .Where(column => column.Name != ColumnNames.Id)
                .ToList();
            var createdPropertyIds = CreateProperties(tableView.Columns
                .SelectMany(column => column.Properties
                    .Select(property => property.EnumProperty))
                .Distinct()
                .ToArray());
            var createdColumnPropertyIds = CreateColumnsProperties(createdColDict, createdPropertyIds, tableView.Columns);

            foreach (var record in tableView.Records)
            {
                int rowIndex = record.Key;

                Row row = new Row();

                row.Index = rowIndex;
                row.TableId = tableId;

                var rowId = _dbContext.Rows.Add(row).Entity.Id;

                foreach (var item in record.Value)
                {
                    string colName = item.Key;

                    Guid colId = createdColDict[colName];

                    Cell cell = new Cell();

                    cell.TableId = tableId;
                    cell.RowId = rowId;
                    cell.ColumnId = colId;
                    cell.Value = item.Value;

                    _dbContext.Cells.Add(cell);
                }
            }

            _dbContext.SaveChanges();

            return tableId;
        }

        public TableView GetTable(Guid id)
        {
            TableView tableView = new TableView();

            Table table = _dbContext.Tables.Where(t => t.Id == id).FirstOrDefault();

            tableView.TableId = id;
            tableView.Title = table.Name;
            //tableView.Type = table.TableTypes.FirstOrDefault().Type;
            tableView.Records = GetRecords(GetCells(id));
            tableView.Columns = GetColumnArray(GetColumns(id));

            return tableView;
        }

        public void DeleteTable(Guid id)
        {
            Table table = _dbContext.Tables.Where(t => t.Id == id).FirstOrDefault();
            Row[] rows = _dbContext.Rows.Where(r => r.TableId == id).ToArray();
            Column[] columns = _dbContext.Columns.Where(c => c.TableId == id).ToArray();
            Cell[] cells = _dbContext.Cells.Where(c => c.TableId == id).ToArray();

            _dbContext.Cells.RemoveRange(cells);
            _dbContext.Columns.RemoveRange(columns);
            _dbContext.Rows.RemoveRange(rows);
            _dbContext.Tables.Remove(table);            
        }

        public List<TableDesription> GetTableDesriptionsList()
        {
            var tables = _dbContext.Tables.ToList();

            return GetTableDescriptions(tables);
        }

        public List<TableDesription> GetTableDesriptionsList(Type type)
        {
            var t = (from tables in _dbContext.Tables
                         join types in _dbContext.TableTypes on tables.Id equals types.TableId
                         where types.Type == type
                         select tables).ToList();

            return GetTableDescriptions(t);
        }

        public TableDesription GetTableDesription(Guid tableId)
        {
            TableDesription tableDesription = new TableDesription();

            var table = _dbContext.Tables.Where(t => t.Id == tableId).First();

            tableDesription.Id = table.Id;
            tableDesription.Title = table.Name;
            //tableDesription.Type = table.TableTypes.First().Type;
            tableDesription.ColumnViews = GetColumnViews(table.Id);

            return tableDesription;
        }

        #endregion

        #region Private methods

        private Dictionary<int, Dictionary<string, string>> GetRecords(Cell[] cells)
        {
            Dictionary<int, Dictionary<string, string>> records = new Dictionary<int, Dictionary<string, string>>();

            foreach (var cell in cells)
            {
                Dictionary<string, string> record = new Dictionary<string, string>();
                Dictionary<string, string> tempRecord = new Dictionary<string, string>();

                int rowIndex = cell.Row.Index;

                if (records.TryGetValue(rowIndex, out tempRecord))
                {
                    tempRecord.Add(cell.Column.Name, cell.Value);

                    records[rowIndex] = tempRecord;
                }
                else
                {
                    record.Add(cell.Column.Name, cell.Value);

                    records.Add(rowIndex, record);
                }
            }

            return records;
        }
        private IEnumerable<ColumnWithPropertiesView> GetColumnArray(Column[] columns)
        {
            var columnViews = new List<ColumnWithPropertiesView>();

            foreach (var column in columns)
            {
                var columnView = new ColumnWithPropertiesView
                {
                    Name = column.Name
                };
                var properties = new List<ColumnPropertyView>();
                foreach(var property in column.ColumnProperties)
                {
                    var propertyView = new ColumnPropertyView();
                    propertyView.EnumProperty = property.Property.EnumProperty;
                    propertyView.Value = property.Value;
                    properties.Add(propertyView);   
                }
                columnView.Properties = properties;
                columnViews.Add(columnView);
            }

            return columnViews;
        }
        private Dictionary<string, Guid> CreateColumns(string[] colArray, Guid tableId)
        {
            Dictionary<string, Guid> createdColumnsDict = new Dictionary<string, Guid>();

            foreach (string colName in colArray)
            {
                Column column = new Column();

                column.Name = colName;
                column.TableId = tableId;

                var colEntity = _dbContext.Columns.Add(column);

                createdColumnsDict.Add(colName, colEntity.Entity.Id);                
            }

            return createdColumnsDict;
        }
        private IEnumerable<Guid> CreateColumnsProperties(IDictionary<string, Guid> columnIds, IDictionary<EnumProperties, Guid> propertyIds, IEnumerable<ColumnWithPropertiesView> columns)
        {
            var columnPropertyIds = new List<Guid>();

            foreach (var column in columns)
            {
                foreach (var property in column.Properties)
                {
                    var columnProperty = new ColumnProperty
                    {
                        ColumnId = columnIds[column.Name],
                        PropertyId = propertyIds[property.EnumProperty],
                        Value = property.Value
                    };

                    var createdColumnProperty = _dbContext.ColumnProperties.Add(columnProperty);

                    columnPropertyIds.Add(createdColumnProperty.Entity.Id);
                }
            }

            return columnPropertyIds;
        }
        private IDictionary<EnumProperties, Guid> CreateProperties(IEnumerable<EnumProperties> properties)
        {
            var propertyIds = new Dictionary<EnumProperties, Guid>();

            foreach (var property in properties)
            {
                var propertyEntity = new Property
                {
                    EnumProperty = property,
                };

                var createdProperty = _dbContext.Properties.Add(propertyEntity);

                propertyIds.Add(property, createdProperty.Entity.Id);
            }

            return propertyIds;
        }
        private List<TableDesription> GetTableDescriptions(List<Table> tables)
        {
            List<TableDesription> tableDesriptions = new List<TableDesription>();

            foreach (var table in tables)
            {
                var tableDesription = GetTableDesription(table.Id);

                tableDesriptions.Add(tableDesription);
            }

            return tableDesriptions;
        }

        private List<ColumnView> GetColumnViews(Guid tableId)
        {
            List<ColumnView> columnViews = new List<ColumnView>();

            var columnList = _dbContext.Columns.Where(c => c.TableId == tableId).ToList();

            foreach (var col in columnList)
            {
                ColumnView columnView = new ColumnView();

                columnView.ColumnId = col.Id;
                columnView.Name = col.Name;
                columnView.ColumnProperties = GetColumnProperties(col.Id);

                columnViews.Add(columnView);
            }

            return columnViews;
        }

        private List<ColumnProperty> GetColumnProperties(Guid id)
        {
            List<ColumnProperty> columnProperties = new List<ColumnProperty>();

            return _dbContext.ColumnProperties.Where(p => p.ColumnId == id).ToList();
        }
        private Cell[] GetCells(Guid tableId)
        {
            var cells = _dbContext.Cells.Where(c => c.TableId == tableId).ToArray();

            return cells;
        }
        private Column[] GetColumns(Guid tableId)
        {
            var columns = _dbContext.Columns
                .Where(c => c.TableId == tableId)
                .Include(x => x.ColumnProperties)
                .ThenInclude(x => x.Property).ToArray();

            return columns;
        }

        #endregion
    }
}
