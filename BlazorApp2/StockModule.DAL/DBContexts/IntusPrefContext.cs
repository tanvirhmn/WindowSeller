using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StockModule.DAL.Models;

namespace StockModule.DAL.DBContexts
{
    public class IntusPrefContext : DbContext
    {
        //public IntusPrefContext()
        //{
        //}

        public IntusPrefContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IntusPrefContext).Assembly);
        }

        public DbSet<Material> Materials { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Permission> Permissions { get; set; } = null!;
        public DbSet<UserPermission> UserPermissions { get; set; } = null!;
        public DbSet<Stock> Stock { get; set; } = null!;
        public DbSet<Warehouse> Warehouses { get; set; } = null!;
        public DbSet<StockDocument> StockDocuments { get; set; } = null!;
        public DbSet<StockMovement> StockMovement { get; set; } = null!;
        public DbSet<StockMovementReason> StockMovementReasons { get; set; } = null!;
        public DbSet<StockSetting> StockSettings { get; set; } = null!;
        public DbSet<ExternalMovement> ExternalMovements { get; set; } = null!;
        public DbSet<ExternalMovementArchive> ExternalMovementsArchive { get; set; } = null!;
        public DbSet<StockAccountingAction> StockAccountingActions { get; set; } = null!;
        public DbSet<StockAccountingMovement> StockAccountingMovements { get; set; } = null!;
        public DbSet<FolderHierarchy> FolderHierarchies { get; set; } = null!;
        public DbSet<View_StockSettingsMaterial_FolderHierarchy> View_StockSettinsMaterialView_FolderHierarchies { get; set; } = null!;
        public DbSet<FilterViewMaster> FilterViewMaster { get; set; } = null!;
        public DbSet<FilterViewDetail> FilterViewDetail { get; set; } = null!;

        public DbSet<MaterialColumn> MaterialColumn { get; set; } = null!;
        public DbSet<MaterialColumnValue> MaterialColumnValue { get; set; } = null!; 
        public DbSet<MaterialTypeEnumMaster> MaterialTypeEnumMaster { get; set; } = null!;
        public DbSet<MaterialTypeEnumDetail> MaterialTypeEnumDetail { get; set; } = null!;
        public DbSet<DynamicMaterialCoulmnGridHiding> DynamicMaterialCoulmnGridHiding { get; set; } = null!;
    }
}
