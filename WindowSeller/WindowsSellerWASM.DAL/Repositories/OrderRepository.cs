using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;
using WindowSellerWASM.Shared.Persistance;

namespace WindowsSellerWASM.DAL.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly WindowSellerDdContext _dbContext;

        public OrderRepository(WindowSellerDdContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
