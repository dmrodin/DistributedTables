using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableStorage.View;

namespace TablesAPI.BLogical.Interfaces
{
    public interface ITableService
    {
        public TableView GetTable(Guid id);
        public List<TableDesription> GetTableDesriptions();
        public TableDesription GetTableDesription(Guid tableId);
        public Guid CreateTable(TableView tableView);
        public void DeleteTable(Guid id);
        public Guid DownloadExcel(string filePath, string tableName);
        public void ChangeCell(CellView cellView);
    }
}
