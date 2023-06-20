using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Models
{
    [JsonObject(IsReference = true)]
    public class Employee
    {        
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string WorkPosition { get; set; } = String.Empty;
        public string? Team { get; set; }
        public string? UserName { get; set; }
        public string? SqlUserName { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserPermission>? UserPermissions { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<StockMovement>? StockMovemenets { get; set; }
    }
}
