using StockModule.BLL.Logic;
using StockModule.DAL.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockModule.BLL.StockSettings;
using StockModule.BLL.Dto;

namespace StockModule.BLL
{
    public interface IMaterialLogic
    {
       
        public bool MoveStocks(RequestMoveStock requestMoveStocks, out string errMessage);
        public bool MoveStockFromCollectionApi(List<CollectionMovement> movement);
        public StockMovement GetMovementWithTypeKeyRelatedIds(CollectionMovement movement);
        public bool MoveReproduction(ReproductionMovement reproductionMovement);
        bool ExternalMovementBusy { get; set; }
        public bool MoveExternalStock();
        MaterialStockInfoDto? GetMaterialStockInfo(string code, Warehouses warehouse);
        public Task<MaterialDescriptionDTO> GetMaterialsDescriptionsAsync(MaterialDescriptionRequestDTO materialDescriptionDTO);
        public Task<IEnumerable<MaterialDto>> GetMaterialsByCodesAsync(MaterialDescriptionRequestDTO materialDescriptionDTO);
    }
}
