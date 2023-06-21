using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TablesAPI.BLogical.Interfaces;
using TableStorage.View;

namespace TablesAPI.Hubs
{
    public class Broadcast : Hub
    {
        private readonly ITableService _tableService;

        public Broadcast(ITableService tableService)
        {
            _tableService = tableService;
        }

        public Task ChangeCell(string jsonCell)
        {
            CellView cellView = new CellView();

            cellView = JsonConvert.DeserializeObject<CellView>(jsonCell);

            _tableService.ChangeCell(cellView);

            return Clients.All.SendAsync("BroadcastCell", cellView);
        }
    }
}
