using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModule.BLL.DTOs
{
    public class PermissionDto
    {
        public string Name { get; set; } = String.Empty;
        public string MenuCode { get; set; } = String.Empty;
        public string AzureGroup { get; set; } = String.Empty;
    }
}
