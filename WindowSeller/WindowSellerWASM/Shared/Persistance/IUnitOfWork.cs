using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowSellerWASM.Shared.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository OrderRepository { get; }
        IWindowRepository WindowRepository { get; }
        ISubElementRepository SubElementRepository { get; }

    }
}
