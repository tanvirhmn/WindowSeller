using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Server.Models;

public partial class IntusPrefContext : DbContext
{
    public IntusPrefContext()
    {
    }

    public IntusPrefContext(DbContextOptions<IntusPrefContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DynamicMaterialCoulmnGridHiding> DynamicMaterialCoulmnGridHidings { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<ExternalMovement> ExternalMovements { get; set; }

    public virtual DbSet<ExternalMovementsArchive> ExternalMovementsArchives { get; set; }

    public virtual DbSet<FilterViewDetail> FilterViewDetails { get; set; }

    public virtual DbSet<FilterViewMaster> FilterViewMasters { get; set; }

    public virtual DbSet<Fitting> Fittings { get; set; }

    public virtual DbSet<FittingHardwareSet> FittingHardwareSets { get; set; }

    public virtual DbSet<FittingHardwareSetDescription> FittingHardwareSetDescriptions { get; set; }

    public virtual DbSet<FittingMaterial> FittingMaterials { get; set; }

    public virtual DbSet<FittingMushroom> FittingMushrooms { get; set; }

    public virtual DbSet<FittingScrew> FittingScrews { get; set; }

    public virtual DbSet<FolderHierarchy> FolderHierarchies { get; set; }

    public virtual DbSet<FolderHierarchyMaterialColumnMap> FolderHierarchyMaterialColumnMaps { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialColumn> MaterialColumns { get; set; }

    public virtual DbSet<MaterialColumnValue> MaterialColumnValues { get; set; }

    public virtual DbSet<MaterialTypeEnumDetail> MaterialTypeEnumDetails { get; set; }

    public virtual DbSet<MaterialTypeEnumMaster> MaterialTypeEnumMasters { get; set; }

    public virtual DbSet<MaterialsSetting> MaterialsSettings { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<ProductionGlassBuffer> ProductionGlassBuffers { get; set; }

    public virtual DbSet<ProductionInformationOpeningProgress> ProductionInformationOpeningProgresses { get; set; }

    public virtual DbSet<ProductionInformationTvMessage> ProductionInformationTvMessages { get; set; }

    public virtual DbSet<ProductionTrolley> ProductionTrolleys { get; set; }

    public virtual DbSet<ProductionTrolleyElement> ProductionTrolleyElements { get; set; }

    public virtual DbSet<PurchaseInvoiceImport> PurchaseInvoiceImports { get; set; }

    public virtual DbSet<SalesElement> SalesElements { get; set; }

    public virtual DbSet<SalesElementGlass> SalesElementGlasses { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<StockAccountingAction> StockAccountingActions { get; set; }

    public virtual DbSet<StockAccountingMovement> StockAccountingMovements { get; set; }

    public virtual DbSet<StockDocument> StockDocuments { get; set; }

    public virtual DbSet<StockMovement> StockMovements { get; set; }

    public virtual DbSet<StockMovementReason> StockMovementReasons { get; set; }

    public virtual DbSet<StockSetting> StockSettings { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    public virtual DbSet<ViewDmcTabular> ViewDmcTabulars { get; set; }

    public virtual DbSet<ViewStockSettingsMaterialFolderHierarchy> ViewStockSettingsMaterialFolderHierarchies { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=IntusPref;User Id=sa;Password=123456;Integrated Security=false;;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DynamicMaterialCoulmnGridHiding>(entity =>
        {
            entity.ToTable("DynamicMaterialCoulmnGridHiding");

            entity.HasIndex(e => e.MaterialColumnId, "IX_DynamicMaterialCoulmnGridHiding_MaterialColumnId");

            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.MaterialColumn).WithMany(p => p.DynamicMaterialCoulmnGridHidings).HasForeignKey(d => d.MaterialColumnId);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
        });

        modelBuilder.Entity<ExternalMovement>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("TI_ExternalMovementToArchive"));

            entity.Property(e => e.DocumentDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            entity.Property(e => e.UserName).HasDefaultValueSql("(N'')");
        });

        modelBuilder.Entity<ExternalMovementsArchive>(entity =>
        {
            entity.ToTable("ExternalMovementsArchive");

            entity.Property(e => e.DocumentDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            entity.Property(e => e.UserName).HasDefaultValueSql("(N'')");
        });

        modelBuilder.Entity<FilterViewDetail>(entity =>
        {
            entity.ToTable("FilterViewDetail");

            entity.HasIndex(e => e.FilterViewMasterId, "IX_FilterViewDetail_FilterViewMasterID");

            entity.HasIndex(e => e.ParentId, "IX_FilterViewDetail_ParentId");

            entity.Property(e => e.FilterViewMasterId).HasColumnName("FilterViewMasterID");

            entity.HasOne(d => d.FilterViewMaster).WithMany(p => p.FilterViewDetails).HasForeignKey(d => d.FilterViewMasterId);

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasForeignKey(d => d.ParentId);
        });

        modelBuilder.Entity<FilterViewMaster>(entity =>
        {
            entity.ToTable("FilterViewMaster");

            entity.Property(e => e.AzureUserId).HasColumnName("azureUserID");
        });

        modelBuilder.Entity<Fitting>(entity =>
        {
            entity.Property(e => e.Alias).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Description).HasDefaultValueSql("(N'')");
            entity.Property(e => e.EndCuttable)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.PrefFittingsId)
                .HasDefaultValueSql("(N'')")
                .HasColumnName("PrefFittingsID");
            entity.Property(e => e.PrefMaterialBaseCode).HasDefaultValueSql("(N'')");
            entity.Property(e => e.StartCuttable)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
        });

        modelBuilder.Entity<FittingHardwareSet>(entity =>
        {
            entity.Property(e => e.PrefCode).HasDefaultValueSql("(N'')");
            entity.Property(e => e.PrefSetId).HasColumnName("PrefSetID");
        });

        modelBuilder.Entity<FittingHardwareSetDescription>(entity =>
        {
            entity.HasIndex(e => e.FittingHardwareSetId, "IX_FittingHardwareSetDescriptions_FittingHardwareSetId");

            entity.HasIndex(e => e.FittingId, "IX_FittingHardwareSetDescriptions_FittingId");

            entity.HasOne(d => d.FittingHardwareSet).WithMany(p => p.FittingHardwareSetDescriptions).HasForeignKey(d => d.FittingHardwareSetId);

            entity.HasOne(d => d.Fitting).WithMany(p => p.FittingHardwareSetDescriptions).HasForeignKey(d => d.FittingId);
        });

        modelBuilder.Entity<FittingMaterial>(entity =>
        {
            entity.HasNoKey();

            entity.HasIndex(e => e.FittingId, "IX_FittingMaterials_FittingId");

            entity.HasOne(d => d.Fitting).WithMany().HasForeignKey(d => d.FittingId);
        });

        modelBuilder.Entity<FittingMushroom>(entity =>
        {
            entity.HasIndex(e => e.FittingId, "IX_FittingMushrooms_FittingId");

            entity.HasOne(d => d.Fitting).WithMany(p => p.FittingMushrooms).HasForeignKey(d => d.FittingId);
        });

        modelBuilder.Entity<FittingScrew>(entity =>
        {
            entity.HasIndex(e => e.FittingId, "IX_FittingScrews_FittingId");

            entity.HasOne(d => d.Fitting).WithMany(p => p.FittingScrews).HasForeignKey(d => d.FittingId);
        });

        modelBuilder.Entity<FolderHierarchy>(entity =>
        {
            entity.ToTable("FolderHierarchy");

            entity.HasIndex(e => e.ParentId, "IX_FolderHierarchy_ParentId");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasForeignKey(d => d.ParentId);
        });

        modelBuilder.Entity<FolderHierarchyMaterialColumnMap>(entity =>
        {
            entity.ToTable("FolderHierarchyMaterialColumnMap");

            entity.HasIndex(e => e.FolderHierarchyId, "IX_FolderHierarchyMaterialColumnMap_FolderHierarchyId");

            entity.HasIndex(e => e.MaterialColumnId, "IX_FolderHierarchyMaterialColumnMap_MaterialColumnId");

            entity.Property(e => e.IsRequired)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.IsVisible)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(1)))");

            entity.HasOne(d => d.FolderHierarchy).WithMany(p => p.FolderHierarchyMaterialColumnMaps).HasForeignKey(d => d.FolderHierarchyId);

            entity.HasOne(d => d.MaterialColumn).WithMany(p => p.FolderHierarchyMaterialColumnMaps).HasForeignKey(d => d.MaterialColumnId);
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.Property(e => e.Color).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Type).HasDefaultValueSql("(N'')");
        });

        modelBuilder.Entity<MaterialColumn>(entity =>
        {
            entity.ToTable("MaterialColumn");

            entity.HasIndex(e => e.MaterialTypeEnumMasterId, "IX_MaterialColumn_MaterialTypeEnumMasterId")
                .IsUnique()
                .HasFilter("([MaterialTypeEnumMasterId] IS NOT NULL)");

            entity.Property(e => e.Block).HasMaxLength(255);
            entity.Property(e => e.IsLocked)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);

            entity.HasOne(d => d.MaterialTypeEnumMaster).WithOne(p => p.MaterialColumn).HasForeignKey<MaterialColumn>(d => d.MaterialTypeEnumMasterId);
        });

        modelBuilder.Entity<MaterialColumnValue>(entity =>
        {
            entity.ToTable("MaterialColumnValue");

            entity.HasIndex(e => e.MaterialColumnId, "IX_MaterialColumnValue_MaterialColumnId");

            entity.Property(e => e.Value).HasMaxLength(255);

            entity.HasOne(d => d.MaterialColumn).WithMany(p => p.MaterialColumnValues).HasForeignKey(d => d.MaterialColumnId);
        });

        modelBuilder.Entity<MaterialTypeEnumDetail>(entity =>
        {
            entity.ToTable("MaterialTypeEnumDetail");

            entity.HasIndex(e => e.MaterialTypeEnumMasterId, "IX_MaterialTypeEnumDetail_MaterialTypeEnumMasterId");

            entity.Property(e => e.Value).HasMaxLength(255);

            entity.HasOne(d => d.MaterialTypeEnumMaster).WithMany(p => p.MaterialTypeEnumDetails).HasForeignKey(d => d.MaterialTypeEnumMasterId);
        });

        modelBuilder.Entity<MaterialTypeEnumMaster>(entity =>
        {
            entity.ToTable("MaterialTypeEnumMaster");
        });

        modelBuilder.Entity<MaterialsSetting>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MaterialsSettings");

            entity.Property(e => e.CollectionType)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ProductionGlassBuffer>(entity =>
        {
            entity.ToTable("ProductionGlassBuffer");
        });

        modelBuilder.Entity<ProductionInformationOpeningProgress>(entity =>
        {
            entity.ToTable("ProductionInformationOpeningProgress");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<ProductionInformationTvMessage>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Disabled)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.DisabledBy).HasDefaultValueSql("(N'')");
        });

        modelBuilder.Entity<ProductionTrolleyElement>(entity =>
        {
            entity.HasIndex(e => e.TrolleyId, "IX_ProductionTrolleyElements_TrolleyId");

            entity.HasOne(d => d.Trolley).WithMany(p => p.ProductionTrolleyElements).HasForeignKey(d => d.TrolleyId);
        });

        modelBuilder.Entity<PurchaseInvoiceImport>(entity =>
        {
            entity.ToTable("PurchaseInvoiceImport");
        });

        modelBuilder.Entity<SalesElement>(entity =>
        {
            entity.Property(e => e.HasGrids)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.PalletName).HasDefaultValueSql("(N'')");
            entity.Property(e => e.RotationInPallet).HasDefaultValueSql("(N'')");
            entity.Property(e => e.RowInPallet).HasDefaultValueSql("(N'')");
        });

        modelBuilder.Entity<SalesElementGlass>(entity =>
        {
            entity.HasIndex(e => e.SalesElementId, "IX_SalesElementGlasses_SalesElementId");

            entity.Property(e => e.Barcode).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Glazed)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.InBuffer)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.IsAwning)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.Ordered)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.Priority).HasDefaultValueSql("(N'')");
            entity.Property(e => e.RackName).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Received)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");

            entity.HasOne(d => d.SalesElement).WithMany(p => p.SalesElementGlasses).HasForeignKey(d => d.SalesElementId);
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.ToTable("Stock");

            entity.HasIndex(e => e.MaterialId, "IX_Stock_MaterialId");

            entity.HasIndex(e => e.WarehouseId, "IX_Stock_WarehouseID");

            entity.Property(e => e.LastDocumentDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

            entity.HasOne(d => d.Material).WithMany(p => p.Stocks).HasForeignKey(d => d.MaterialId);

            entity.HasOne(d => d.Warehouse).WithMany(p => p.Stocks).HasForeignKey(d => d.WarehouseId);
        });

        modelBuilder.Entity<StockAccountingAction>(entity =>
        {
            entity.HasIndex(e => e.StockAccountingMovementId, "IX_StockAccountingActions_StockAccountingMovementId");

            entity.Property(e => e.IsSuccess)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.Method).HasDefaultValueSql("(N'')");

            entity.HasOne(d => d.StockAccountingMovement).WithMany(p => p.StockAccountingActions).HasForeignKey(d => d.StockAccountingMovementId);
        });

        modelBuilder.Entity<StockAccountingMovement>(entity =>
        {
            entity.HasIndex(e => e.ChangedByStockMovementId, "IX_StockAccountingMovements_ChangedByStockMovementId");

            entity.HasIndex(e => e.StockMovementId, "IX_StockAccountingMovements_StockMovementId");

            entity.HasOne(d => d.ChangedByStockMovement).WithMany(p => p.StockAccountingMovementChangedByStockMovements).HasForeignKey(d => d.ChangedByStockMovementId);

            entity.HasOne(d => d.StockMovement).WithMany(p => p.StockAccountingMovementStockMovements).HasForeignKey(d => d.StockMovementId);
        });

        modelBuilder.Entity<StockMovement>(entity =>
        {
            entity.ToTable("StockMovement");

            entity.HasIndex(e => e.DocumentId, "IX_StockMovement_DocumentId");

            entity.HasIndex(e => e.DocumentNumber, "IX_StockMovement_DocumentNumber");

            entity.HasIndex(e => e.EmployeeId, "IX_StockMovement_EmployeeId");

            entity.HasIndex(e => e.FromStockId, "IX_StockMovement_FromStockId");

            entity.HasIndex(e => e.ReasonId, "IX_StockMovement_ReasonId");

            entity.HasIndex(e => e.ToStockId, "IX_StockMovement_ToStockId");

            entity.HasOne(d => d.Document).WithMany(p => p.StockMovements).HasForeignKey(d => d.DocumentId);

            entity.HasOne(d => d.Employee).WithMany(p => p.StockMovements).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.FromStock).WithMany(p => p.StockMovementFromStocks).HasForeignKey(d => d.FromStockId);

            entity.HasOne(d => d.Reason).WithMany(p => p.StockMovements).HasForeignKey(d => d.ReasonId);

            entity.HasOne(d => d.ToStock).WithMany(p => p.StockMovementToStocks).HasForeignKey(d => d.ToStockId);
        });

        modelBuilder.Entity<StockMovementReason>(entity =>
        {
            entity.HasIndex(e => e.FromWarehouseId, "IX_StockMovementReasons_FromWarehouseId");

            entity.HasIndex(e => e.ToWarehouseId, "IX_StockMovementReasons_ToWarehouseId");

            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.GroupName).HasDefaultValueSql("(N'')");
            entity.Property(e => e.IsGenerateAccountingEvent)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.RivileCenter).HasDefaultValueSql("(N'')");

            entity.HasOne(d => d.FromWarehouse).WithMany(p => p.StockMovementReasonFromWarehouses).HasForeignKey(d => d.FromWarehouseId);

            entity.HasOne(d => d.ToWarehouse).WithMany(p => p.StockMovementReasonToWarehouses).HasForeignKey(d => d.ToWarehouseId);
        });

        modelBuilder.Entity<StockSetting>(entity =>
        {
            entity.HasIndex(e => e.FolderHierarchyId, "IX_StockSettings_FolderHierarchyId");

            entity.HasIndex(e => e.MaterialId, "IX_StockSettings_MaterialId").IsUnique();

            entity.Property(e => e.CollectionType)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasDefaultValueSql("('Warehouse')");
            entity.Property(e => e.Reproducible)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.HasOne(d => d.FolderHierarchy).WithMany(p => p.StockSettings).HasForeignKey(d => d.FolderHierarchyId);

            entity.HasOne(d => d.Material).WithOne(p => p.StockSetting).HasForeignKey<StockSetting>(d => d.MaterialId);
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_UserPermissions_EmployeeId");

            entity.HasIndex(e => e.PermissionId, "IX_UserPermissions_PermissionId");

            entity.HasOne(d => d.Employee).WithMany(p => p.UserPermissions).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.Permission).WithMany(p => p.UserPermissions).HasForeignKey(d => d.PermissionId);
        });

        modelBuilder.Entity<ViewDmcTabular>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_DMC_Tabular");

            entity.Property(e => e.Alias).HasMaxLength(255);
            entity.Property(e => e.BarLength).HasMaxLength(255);
            entity.Property(e => e.Code).HasMaxLength(255);
            entity.Property(e => e.CollectionType).HasMaxLength(255);
            entity.Property(e => e.Color).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.FolderHierarchyId).HasMaxLength(255);
            entity.Property(e => e.MaterialId).HasMaxLength(255);
            entity.Property(e => e.Reproducible).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);
        });

        modelBuilder.Entity<ViewStockSettingsMaterialFolderHierarchy>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_StockSettingsMaterial_FolderHierarchy");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
