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
    public class MaterialColumnValueConfiguration : IEntityTypeConfiguration<MaterialColumnValue>
    {
        public void Configure(EntityTypeBuilder<MaterialColumnValue> builder)
        {
            builder.ToTable("MaterialColumnValue");
            builder.Property(d => d.RowNo)
                .IsRequired();
            builder.Property(d => d.Value)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(d => d.MaterialColumnId)
                .IsRequired();

        }
    }
}
