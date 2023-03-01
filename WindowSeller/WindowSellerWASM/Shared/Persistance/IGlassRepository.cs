using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;

namespace WindowSellerWASM.Shared.Persistance
{
    public interface IGlassRepository : IGenericRepository<Glass>
    {
        Task<IReadOnlyList<Glass>> GetAllSortedAsync();
    }
}
