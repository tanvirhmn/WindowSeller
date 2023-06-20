using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services
{
    public interface ITaskService 
    {
        int UpdateTaskRemnant(int taskId, string remnantBarcode, string selectCycleOrders,  bool? returnWarehouse);

        int DeleteTaskRemnant(int taskId, string selectCycleOrders);

   
    }
}
