using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;

namespace WindowsSellerWASM.DAL
{
    public class WindowSellerDdContext : DbContext
    {
        public WindowSellerDdContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OrderCreation(modelBuilder);
            modelBuilder.Entity<Order>()
    .HasMany<Window>(ordr => ordr.Windows)
    .WithOne(wnd => wnd.Order)
    .HasForeignKey(wnd => wnd.OrderId)
    .OnDelete(DeleteBehavior.Cascade);
            WindowCreation(modelBuilder);
            SubElementtCreation(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WindowSellerDdContext).Assembly);
        }


        public DbSet<Order> Orders { get; set; }

        public DbSet<Window> Windows { get; set; }

        public DbSet<SubElement> SubElements { get; set; }

        private void OrderCreation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Property(ordr => ordr.OrderName).IsRequired();
            modelBuilder.Entity<Order>().Property(ordr => ordr.State).IsRequired();

        }
        private void WindowCreation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Window>().Property(wnd => wnd.WindowName).IsRequired();
            modelBuilder.Entity<Window>().Property(wnd => wnd.QuantityOfWindows).IsRequired();
            modelBuilder.Entity<Window>()
                .HasMany<SubElement>(wnd => wnd.SubElements)
                .WithOne(sblmnt => sblmnt.Window)
                .HasForeignKey(sblmnt => sblmnt.WindowId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        private void SubElementtCreation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubElement>().Property(subElm => subElm.Element).IsRequired();
            modelBuilder.Entity<SubElement>().Property(subElm => subElm.Width).IsRequired();
            modelBuilder.Entity<SubElement>().Property(subElm => subElm.Height).IsRequired();
        }
    }
}
