using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Configurations
{
    public class FolderHierarchyConfiguration : IEntityTypeConfiguration<FolderHierarchy>
    {
        public void Configure(EntityTypeBuilder<FolderHierarchy> builder)
        {
            builder.ToTable("FolderHierarchy");
            builder.Property(d => d.Name)
                .IsRequired();
            builder.Property(d => d.Icon)
                .IsRequired();
        }
    }
}
