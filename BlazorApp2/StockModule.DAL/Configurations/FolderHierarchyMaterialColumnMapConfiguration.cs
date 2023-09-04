using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Configurations
{
    public class FolderHierarchyMaterialColumnMapConfiguration : IEntityTypeConfiguration<FolderHierarchyMaterialColumnMap>
    {
        public void Configure(EntityTypeBuilder<FolderHierarchyMaterialColumnMap> builder)
        {
            builder.ToTable("FolderHierarchyMaterialColumnMap");
            
            builder.Property(d => d.IsRequired)
                .HasDefaultValue(false);
            builder.Property(d => d.IsVisible)
                .HasDefaultValue(true);
            builder.Property(d => d.FolderHierarchyId)
                .IsRequired();
            builder.Property(d => d.MaterialColumnId)
                .IsRequired();

        }
    }
}
