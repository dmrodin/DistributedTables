using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TablesAPI.BLogical.Interfaces;
using TableStorage.View;

namespace TablesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : Controller
    {
        private ITableService _tableService;

        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        [Route("GetTable")]
        public TableView GetTable(Guid id)
        {
            return _tableService.GetTable(id);
        }

        [HttpGet]
        [Route("GetTableDescriptoins")]
        public List<TableDesription> GetTableDescriptoins()
        {
            return _tableService.GetTableDesriptions();
        }

        [HttpPost]
        [Route("Download")]
        public async Task<string> Download(IFormFile formFile)
        {
            var filePath = Path.GetTempFileName();

            if (formFile.Length > 0)
            {
                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }
            }

            string tablename = formFile.FileName;

            try
            {
                Guid tableId = _tableService.DownloadExcel(filePath, tablename);

                return tableId.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        [Route("CreateTable")]
        public async Task<Guid> CreateTable(TableView tableView)
        {
            return _tableService.CreateTable(tableView);
        }

    }
}
