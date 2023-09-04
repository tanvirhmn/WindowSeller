using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services.Interfaces
{
    public interface IDynamicMaterialColumnPivotedTableService
    {
        DataTable GetAll();

        DataTable GetAllColumnControlled();
    }
}
