using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSellerWASM.Shared.Persistance;

namespace WindowsSellerWASM.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WindowSellerDdContext _dbContext;

        IOrderRepository _orderRepository;
        IWindowRepository _windowRepository;
        ISubElementRepository _subElementRepository;

        public UnitOfWork(WindowSellerDdContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IOrderRepository OrderRepository =>
            _orderRepository ??= new OrderRepository(_dbContext);

        public IWindowRepository WindowRepository =>
            _windowRepository ??= new WindowRepository(_dbContext);

        public ISubElementRepository SubElementRepository =>
            _subElementRepository ??= new SubElementRepository(_dbContext);

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
