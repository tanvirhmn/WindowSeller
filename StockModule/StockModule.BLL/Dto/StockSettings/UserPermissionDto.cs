using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.StockSettings
{
    public class UserPermissionDto
    {
        public int Id { get; set; }
        public PermissionDto? Permission { get; set; }
        public EmployeeDto? Employee { get; set; }
    }
}
