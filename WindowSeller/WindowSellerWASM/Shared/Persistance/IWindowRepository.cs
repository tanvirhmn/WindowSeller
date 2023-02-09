using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;

namespace WindowSellerWASM.Shared.Persistance
{
    public interface IWindowRepository: IGenericRepository<Window>
    {
        Task<List<Window>> GetByOrderIdAsync(long orderId);
        Task UpdateTotalSubELementsAsync(int count, long windowI);
    }
}
