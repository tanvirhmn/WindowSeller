using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace StockModule.DAL.Configurations
{
    public class FilterViewDetailConfiguration : IEntityTypeConfiguration<FilterViewDetail>
    {
        public void Configure(EntityTypeBuilder<FilterViewDetail> builder)
        {
            builder.ToTable("FilterViewDetail");
            builder.Property(d => d.Property)
                .IsRequired();
            builder.Property(d => d.FilterValue)
                .IsRequired();
            builder.Property(d => d.FilterOperator)
                .IsRequired();
            builder.Property(d => d.LogicalFilterOperator)
                .IsRequired();
            builder.Property(d => d.FilterViewMasterID)
                .IsRequired();

            builder.HasOne<FilterViewDetail>(wnd => wnd.ParentFilterViewDetail)
                .WithMany(sblmnt => sblmnt.ChildFilterViewDetails)
                .HasForeignKey(sblmnt => sblmnt.ParentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
