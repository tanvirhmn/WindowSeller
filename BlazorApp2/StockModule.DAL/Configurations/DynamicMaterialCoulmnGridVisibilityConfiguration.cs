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
    public class DynamicMaterialCoulmnGridVisibilityConfiguration : IEntityTypeConfiguration<DynamicMaterialCoulmnGridHiding>
    {
        public void Configure(EntityTypeBuilder<DynamicMaterialCoulmnGridHiding> builder)
        {
            builder.ToTable("DynamicMaterialCoulmnGridHiding");

            builder.Property(d => d.UserID)
                .IsRequired(required: false);
            builder.Property(d => d.MaterialColumnId)
                .IsRequired();

        }
    }
}
