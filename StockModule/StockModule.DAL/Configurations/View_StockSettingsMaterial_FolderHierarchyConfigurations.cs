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
    public class View_StockSettingsMaterial_FolderHierarchyConfigurations : IEntityTypeConfiguration<View_StockSettingsMaterial_FolderHierarchy>
    {
        public void Configure(EntityTypeBuilder<View_StockSettingsMaterial_FolderHierarchy> builder)
        {
            builder.ToView("View_StockSettingsMaterial_FolderHierarchy");
        }
    }
}
