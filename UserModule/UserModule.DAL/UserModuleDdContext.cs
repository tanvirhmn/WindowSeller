using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.DAL.Models;

namespace UserModule.DAL
{
    public class UserModuleDdContext : DbContext
    {
        public UserModuleDdContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserModuleDdContext).Assembly);
        }
        public DbSet<Permission> Permissions { get; set; }

    }
}
