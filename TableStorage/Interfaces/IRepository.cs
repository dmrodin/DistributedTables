using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableStorage.View;

namespace TableStorage.Interfaces
{
    public interface IRepository
    { 
        public Guid CreateTable(TableView tableView);
        public void DeleteTable(Guid id);
        public TableView GetTable(Guid id);
        public void ChangeCell(Guid id, string value);
        public List<TableDesription> GetTableDesriptionsList();
        public List<TableDesription> GetTableDesriptionsList(Type type);
        public TableDesription GetTableDesription(Guid tableId);
        public void ChangeCell(CellView cellView);
    }
}
