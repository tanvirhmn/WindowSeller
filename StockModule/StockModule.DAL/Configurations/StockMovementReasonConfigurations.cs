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
    public class StockMovementReasonConfigurations : IEntityTypeConfiguration<StockMovementReason>
    {
        public void Configure(EntityTypeBuilder<StockMovementReason> builder)
        {
            builder.Property(e => e.RivileCenter)
                .HasDefaultValue("");
        }
    }
}
