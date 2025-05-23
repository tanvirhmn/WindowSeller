﻿using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services
{
    public interface IEmployeeService : IEntityService<Employee>
    {
        int GetIdByUserName(string userName);
        int GetId(int EmployeeId);
    }
}
