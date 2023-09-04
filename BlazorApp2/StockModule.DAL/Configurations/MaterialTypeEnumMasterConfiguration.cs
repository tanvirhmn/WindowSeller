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
    public class MaterialTypeEnumMasterConfiguration
    {
        public void Configure(EntityTypeBuilder<MaterialTypeEnumMaster> builder)
        {
            builder.ToTable("MaterialTypeEnumMaster");
            builder.Property(d => d.Name)
                .HasMaxLength(255)
                .IsRequired();


            builder.HasOne<MaterialColumn>(wnd => wnd.MaterialColumn)
                .WithOne(sblmnt => sblmnt.MaterialTypeEnumMaster)
                .HasForeignKey<MaterialColumn>(sblmnt => sblmnt.MaterialTypeEnumMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<MaterialTypeEnumDetail>(wnd => wnd.MaterialTypeEnumDetails)
                .WithOne(sblmnt => sblmnt.MaterialTypeEnumMaster)
                .HasForeignKey(sblmnt => sblmnt.MaterialTypeEnumMasterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
