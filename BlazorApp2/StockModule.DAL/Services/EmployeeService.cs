using StockModule.DAL.DBContexts;
using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services
{
    public class EmployeeService : EntityService<Employee>, IEmployeeService
    {
        public EmployeeService(IntusPrefContext context) : base(context)
        {
            Set = context.Set<Employee>();
        }

        public int GetIdByUserName(string userName)
        {
            userName = userName.Replace("INTUSWINDOWS", "");

            var employee = Set.FirstOrDefault(f => f.SqlUserName == userName || f.UserName == userName);

            return employee == null ? this.GetId(0) : employee.ID;
        }

        public int GetId(int EmployeeId)
        {
            return Set.Where(w => w.EmployeeID == EmployeeId).Select(s => s.ID).FirstOrDefault();
        }
    }
}
