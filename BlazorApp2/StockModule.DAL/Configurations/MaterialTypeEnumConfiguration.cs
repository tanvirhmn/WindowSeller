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
    public class MaterialTypeEnumConfiguration : IEntityTypeConfiguration<MaterialTypeEnumDetail>
    {
        public void Configure(EntityTypeBuilder<MaterialTypeEnumDetail> builder)
        {
            builder.ToTable("MaterialTypeEnumDetail");
            builder.Property(d => d.Key)
                .IsRequired();
            builder.Property(d => d.Value)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(d => d.MaterialTypeEnumMasterId)
                .IsRequired();

        }
    }
}
