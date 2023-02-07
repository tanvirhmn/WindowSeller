using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;

namespace WindowSellerWASM.Shared.Persistance
{
    public interface ISubElementRepository : IGenericRepository<SubElement>
    {
        Task<List<SubElement>> GetByWindowIdAsync(int windowId);
    }
}
