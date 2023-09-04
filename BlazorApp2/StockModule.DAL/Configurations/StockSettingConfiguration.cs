using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Configurations
{
    public class StockSettingConfiguration : IEntityTypeConfiguration<StockSetting>
    {
        public void Configure(EntityTypeBuilder<StockSetting> builder)
        {
            builder.HasIndex(e => e.MaterialId, "IX_StockSettings_MaterialId")
                .IsUnique();
            builder.Property(e => e.CollectionType)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Warehouse')");
            builder.Property(e => e.Reproducible)
                    .IsRequired()
                    .HasDefaultValueSql("(1)");
            builder.HasOne(d => d.Material)
                    .WithOne(p => p.StockSetting)
                    .HasForeignKey<StockSetting>(d => d.MaterialId);
            builder.Property(d => d.FolderHierarchyId)
                    .IsRequired(false);
        }
    }
}
