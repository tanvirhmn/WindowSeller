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
    public class MaterialColumnConfiguration : IEntityTypeConfiguration<MaterialColumn>
    {
        public void Configure(EntityTypeBuilder<MaterialColumn> builder)
        {
            builder.ToTable("MaterialColumn");
            builder.Property(d => d.Name)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(d => d.Block)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(d => d.Type)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(d => d.IsLocked)
                .HasDefaultValue(false);



            builder.HasMany<MaterialColumnValue>(wnd => wnd.MaterialColumnValues)
                .WithOne(sblmnt => sblmnt.MaterialColumn)
                .HasForeignKey(sblmnt => sblmnt.MaterialColumnId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
