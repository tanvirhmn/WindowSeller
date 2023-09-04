using StockModule.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StockModule.DAL.DBContexts;

namespace StockModule.DAL.Services
{
    public class StockMovementService : EntityService<StockMovement>, IStockMovementService
    {
        private readonly IntusPrefContext _context = null!;
        private readonly ILogger Logger;
        public StockMovementService(IntusPrefContext context, ILogger<StockMovementService> logger) : base(context)
        {
            _context = context;
            Logger = logger;
            Set = context.Set<StockMovement>();
        }


        public StockMovement? GetById(int id)
        {           
            return Set.Include(_=>_.StockMovementReason).FirstOrDefault(_=>_.Id == id);
        }

        public List<int> GetStockMovementIdByFromDateForAccounting(DateTime datetime)
        {
            return Set.Include(_ => _.StockMovementReason).Where(_=>_.StockMovementReason.IsGenerateAccountingEvent && _.DocumentDate >= datetime).OrderBy(_=>_.DocumentDate).ThenBy(_=>_.InsertDate).Select(_=>_.Id).ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public override void Create(StockMovement entity)
        {
            try
            {
                base.Create(entity);

                if (entity.ToStockId != null)
                {
                    entity.ToStock = _context.Stock.First(f => f.Id == entity.ToStockId);

                    //If new movement date is older than last movement, we need to recalculate all newer movements totals
                    if (entity.DocumentDate < entity.ToStock.LastDocumentDate)
                    {
                        List<StockMovement> stockMovements = Set.Where(w => w.DocumentDate > entity.DocumentDate && (w.ToStockId == entity.ToStockId || w.FromStockId == entity.ToStockId)).ToList();

                        StockMovement stockMovementBefore = stockMovements.OrderBy(o => o.DocumentDate).ThenBy(t => t.InsertDate).First();
                        entity.ToTotalQuantity = (stockMovementBefore.ToStockId == entity.ToStockId ? stockMovementBefore.ToTotalQuantity - stockMovementBefore.Quantity : stockMovementBefore.FromTotalQuantity + stockMovementBefore.Quantity) + entity.Quantity;

                        foreach (StockMovement stockMovement in stockMovements)
                        {
                            if (stockMovement.ToStockId == entity.ToStockId) stockMovement.ToTotalQuantity += entity.Quantity;
                            if (stockMovement.FromStockId == entity.ToStockId) stockMovement.FromTotalQuantity += entity.Quantity;
                        }

                        StockMovement lastMovement = Set.Where(w => w.ToStockId == entity.ToStockId || w.FromStockId == entity.ToStockId).OrderByDescending(o => o.DocumentDate).ThenByDescending(t => t.InsertDate).First();
                        entity.ToStock.Quantity = lastMovement.ToStockId == entity.ToStockId ? lastMovement.ToTotalQuantity : lastMovement.FromTotalQuantity;
                        entity.ToStock.LastDocumentDate = lastMovement.DocumentDate;
                    }
                    else
                    {
                        entity.ToTotalQuantity = entity.ToStock.Quantity + entity.Quantity;
                        entity.ToStock.Quantity = entity.ToTotalQuantity;
                        entity.ToStock.LastDocumentDate = entity.DocumentDate;
                    }



                    //update prefsuite DB stock table remnants table if remnants are returned from reserved
                    entity.ToStock = _context.Stock.Include(i => i.Warehouse).Include(i => i.Material).Where(w => w.Id == entity.ToStockId).First();
                                        
                    if (entity.ToStock.Warehouse.Name == "FreeRemnants")
                    {
                        var prefWarehouse = 3;
                        var count = _context.Database.ExecuteSqlRaw("Update Prefsuite.dbo.Stock Set Quantity = {0} WHERE warehouse = {1} and Reference = {2} and Length = {3} and Height = {4}",
                                                        entity.ToStock.Quantity, prefWarehouse, entity.ToStock.Material.Code, entity.ToStock.Length, entity.ToStock.Height);
                    }
                }

                if (entity.FromStockId != null)
                {
                    entity.FromStock = _context.Stock.First(f => f.Id == entity.FromStockId);


                    if (entity.DocumentDate < entity.FromStock.LastDocumentDate)
                    {
                        List<StockMovement> stockMovements = Set.Where(w => w.DocumentDate > entity.DocumentDate && (w.ToStockId == entity.FromStockId || w.FromStockId == entity.FromStockId)).ToList();

                        StockMovement stockMovementBefore = stockMovements.OrderBy(o => o.DocumentDate).ThenBy(t => t.InsertDate).First();
                        entity.FromTotalQuantity = (stockMovementBefore.FromStockId == entity.FromStockId ? stockMovementBefore.FromTotalQuantity + stockMovementBefore.Quantity : stockMovementBefore.ToTotalQuantity - stockMovementBefore.Quantity) - entity.Quantity;

                        foreach (StockMovement stockMovement in stockMovements)
                        {
                            if (stockMovement.ToStockId == entity.FromStockId) stockMovement.ToTotalQuantity -= entity.Quantity;
                            if (stockMovement.FromStockId == entity.FromStockId) stockMovement.FromTotalQuantity -= entity.Quantity;
                        }

                        StockMovement lastMovement = Set.Where(w => w.ToStockId == entity.FromStockId || w.FromStockId == entity.FromStockId).OrderByDescending(o => o.DocumentDate).ThenByDescending(t => t.InsertDate).First();
                        entity.FromStock.Quantity = lastMovement.FromStockId == entity.FromStockId ? lastMovement.FromTotalQuantity : lastMovement.ToTotalQuantity;
                        entity.FromStock.LastDocumentDate = lastMovement.DocumentDate;
                    }
                    else
                    {
                        entity.FromTotalQuantity = entity.FromStock.Quantity - entity.Quantity;
                        entity.FromStock.Quantity = entity.FromTotalQuantity;
                        entity.FromStock.LastDocumentDate = entity.DocumentDate;
                    }

                    //update prefsuite DB stock table remnants table with remnant quantity
                    entity.FromStock = _context.Stock.Include(i => i.Warehouse).Include(i => i.Material).Where(w => w.Id == entity.FromStockId).First();

                    if (entity.FromStock.Warehouse.Name == "FreeRemnants")
                    {
                        var prefWarehouse = 3;
                        var count = _context.Database.ExecuteSqlRaw("Update Prefsuite.dbo.Stock Set Quantity = {0} WHERE warehouse = {1} and Reference = {2} and Length = {3} and Height = {4}",
                                                        entity.FromStock.Quantity, prefWarehouse, entity.FromStock.Material.Code, entity.FromStock.Length, entity.FromStock.Height);
                    }
                }
                

                _context.SaveChanges();
                Logger.LogInformation("Finished create movement {entity}", JsonConvert.SerializeObject(entity).ToString());
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed create movement {entity}", JsonConvert.SerializeObject(entity).ToString());
            }
            finally
            {

            }
        }

    }
}
