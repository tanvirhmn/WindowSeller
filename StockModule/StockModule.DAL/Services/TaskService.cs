using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StockModule.DAL.DBContexts;

namespace StockModule.DAL.Services
{
    public class TaskService :  ITaskService
    {
        private readonly IntusPrefContext _context;
        private readonly ILogger _logger;
        public TaskService(IntusPrefContext context, ILogger<TaskService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public int DeleteTaskRemnant(int taskId, string selectCycleOrders)
        {
            _logger.LogInformation("DeleteTaskRemnant: TaskID - {taskId}, CycleOrders - {cycleOrders}", taskId, selectCycleOrders);
            return _context.Database.ExecuteSqlRaw("dbo.Tasks_Remnant_API_DEL @Task_Id, @SelectCycleOrders",
                      new SqlParameter("@Task_Id", taskId),                   
                      new SqlParameter("@SelectCycleOrders", selectCycleOrders)
                      );
        }

        public int UpdateTaskRemnant(int taskId, string remnantBarcode, string selectCycleOrders, bool? returnWarehouse)
        {
            if (returnWarehouse.HasValue)
            {
                _logger.LogInformation("UpdateTaskRemnant: TaskID - {taskId}, CycleOrders - {cycleOrders}, RemnantBarcode - {remnantBarcode}, ReturnWarehouse - {returnWarehouse}",  
                    taskId, selectCycleOrders, remnantBarcode, returnWarehouse);
                return _context.Database.ExecuteSqlRaw("dbo.Tasks_Remnant_API_UPD @Task_Id, @RemnantBarcode, @SelectCycleOrders, @ReturnWarehouse",
                    new SqlParameter("@Task_Id", taskId),
                    new SqlParameter("@RemnantBarcode", remnantBarcode),
                    new SqlParameter("@SelectCycleOrders", selectCycleOrders),
                    new SqlParameter("@ReturnWarehouse", returnWarehouse));
            } else
            {
                _logger.LogInformation("UpdateTaskRemnant: TaskID - {taskId}, CycleOrders - {cycleOrders}, RemnantBarcode - {remnantBarcode}",
                    taskId, selectCycleOrders, remnantBarcode);
                return _context.Database.ExecuteSqlRaw("dbo.Tasks_Remnant_API_UPD @Task_Id, @RemnantBarcode, @SelectCycleOrders",
                  new SqlParameter("@Task_Id", taskId),
                  new SqlParameter("@RemnantBarcode", remnantBarcode),
                  new SqlParameter("@SelectCycleOrders", selectCycleOrders)
                 );
            }

        }
    }
}
