using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StockModule.DAL.DBContexts;
using StockModule.DAL.Models;
using StockModule.DAL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services
{
    public class FolderHierarchyService : EntityService<FolderHierarchy>, IFolderHierarchyService
    {
        private readonly IntusPrefContext _dbContext;
        public FolderHierarchyService(IntusPrefContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        List<FolderHierarchy> IFolderHierarchyService.GetAllParentFoldersByMaterialId(int rowNo, string colName)
        {
            List<FolderHierarchy> fdlHis = new List<FolderHierarchy>();

            var dmc = _dbContext.MaterialColumn.Where(dmc => dmc.Name == colName).FirstOrDefault();
            if (_dbContext.MaterialColumnValue.Where(mcv => mcv.RowNo == rowNo && mcv.MaterialColumnId == dmc!.Id).Any())
            {
                MaterialColumnValue mcv = _dbContext.MaterialColumnValue.Where(mcv => mcv.RowNo == rowNo && mcv.MaterialColumnId == dmc!.Id).FirstOrDefault()!;
                int? folderHierarchyId = Convert.ToInt32(mcv.Value);
                if (folderHierarchyId != null)
                {
                    var columnName = "id";
                    var columnValue = new SqlParameter("columnValue", folderHierarchyId);

                    fdlHis = _dbContext.FolderHierarchies
                    .FromSqlRaw($@"DECLARE @TABLEVAR TABLE ([Id] [int] ,[Name] [nvarchar](max),[ParentId] [int],[Icon] [nvarchar](max))
                            ;WITH name_tree AS 
                            (
                                SELECT [Id]
                                    ,[Name]
                                    ,[ParentId]
                                    ,[Icon]
                                FROM [dbo].[FolderHierarchy]
                                WHERE {columnName} = @columnValue 
                                UNION ALL
                                SELECT C.[Id]
                                    ,C.[Name]
                                    ,C.[ParentId]
                                    ,C.[Icon]
                                FROM [dbo].[FolderHierarchy] C
                                JOIN name_tree P ON C.[Id] = P.[ParentId]
                            ) 
                            INSERT INTO @TABLEVAR
                            SELECT *
                            FROM name_tree
                            OPTION (MAXRECURSION 0)

                            SELECT * FROM @TABLEVAR", columnValue).ToList();
                }
            }

            return fdlHis;
        }

        List<FolderHierarchy> IFolderHierarchyService.GetAllParentFoldersByFolderId(int folderHierarchyId)
        {
            List<FolderHierarchy> fdlHis = new List<FolderHierarchy>();
            if (folderHierarchyId >0)
            {
                var columnName = "id";
                var columnValue = new SqlParameter("columnValue", folderHierarchyId);

                fdlHis = _dbContext.FolderHierarchies
                .FromSqlRaw($@"DECLARE @TABLEVAR TABLE ([Id] [int] ,[Name] [nvarchar](max),[ParentId] [int],[Icon] [nvarchar](max))
                            ;WITH name_tree AS 
                            (
                                SELECT [Id]
                                    ,[Name]
                                    ,[ParentId]
                                    ,[Icon]
                                FROM [dbo].[FolderHierarchy]
                                WHERE {columnName} = @columnValue 
                                UNION ALL
                                SELECT C.[Id]
                                    ,C.[Name]
                                    ,C.[ParentId]
                                    ,C.[Icon]
                                FROM [dbo].[FolderHierarchy] C
                                JOIN name_tree P ON C.[Id] = P.[ParentId]
                            ) 
                            INSERT INTO @TABLEVAR
                            SELECT *
                            FROM name_tree
                            OPTION (MAXRECURSION 0)

                            SELECT * FROM @TABLEVAR", columnValue).ToList();
            }

            return fdlHis;
        }


        public IEnumerable<FolderHierarchy> GetAll()
        {
            return _dbContext.FolderHierarchies.AsEnumerable();
        }
        public IEnumerable<FolderHierarchy> GetAllRoots()
        {

            return _dbContext.FolderHierarchies.Where(fldrHirarchy => fldrHirarchy.ParentId==null).AsEnumerable();
        }

        public IEnumerable<FolderHierarchy> GetAllChildren(int parentId)
        {

            return _dbContext.FolderHierarchies.Where(fldrHirarchy => fldrHirarchy.ParentId == parentId).AsEnumerable();
        }

        public IEnumerable<FolderHierarchy> GetAllNodes(string names)
        {
            return _dbContext.FolderHierarchies.Where(fldrHirarchy => EF.Functions.Like(fldrHirarchy.Name, "%" + names + "%")).AsEnumerable();
        }

        public bool HasChildren(int parentId)
        {
            return _dbContext.FolderHierarchies.Where(fldrHirarchy => fldrHirarchy.ParentId == parentId).Any();
        }

        public IEnumerable<FolderHierarchy> GetAllNodes()
        {
            return _dbContext.FolderHierarchies
                .FromSqlRaw($@"SELECT id,
                                    [Name],
                                    [ParentId],
                                    [Icon]
                                FROM [dbo].[FolderHierarchy]
	                            UNION
                                SELECT [Id]
                                      ,[Code] + ' ' +[Description] AS [Name]
                                      ,[FolderHierarchyId]  AS [ParentId]
	                                  , ' ' AS [Icon]
                                  FROM [dbo].[View_StockSettingsMaterial_FolderHierarchy] 
                                    WHERE [FolderHierarchyId] IS NOT NULL AND StockSettingsId IS NOT NULL").ToList();
        }

        public List<FolderHierarchy> GetAllParentFoldersByFolderName(string folderName)
        {
            List<FolderHierarchy> fdlHis = new List<FolderHierarchy>();

                var columnName = "Name";
                var columnValue = new SqlParameter("columnValue",SqlDbType.VarChar,200);
                columnValue.Value = folderName;

            //string query = String.Format($@"DECLARE @TABLEVAR TABLE ([Id] [int] ,[Name] [nvarchar](max),[ParentId] [int],[Icon] [nvarchar](max))
            //                ;WITH name_tree AS 
            //                (
            //                    SELECT [Id]
            //                        ,[Name]
            //                        ,[ParentId]
            //                        ,[Icon]
            //                    FROM [dbo].[FolderHierarchy]
            //                    WHERE {columnName} LIKE'%@columnValue%' 
            //                    UNION ALL
            //                    SELECT C.[Id]
            //                        ,C.[Name]
            //                        ,C.[ParentId]
            //                        ,C.[Icon]
            //                    FROM [dbo].[FolderHierarchy] C
            //                    JOIN name_tree P ON C.[Id] = P.[ParentId]
            //                ) 
            //                INSERT INTO @TABLEVAR
            //                SELECT *
            //                FROM name_tree
            //                OPTION (MAXRECURSION 0)

            //                SELECT * FROM @TABLEVAR", columnValue);

                fdlHis = _dbContext.FolderHierarchies
                .FromSqlRaw($@"DECLARE @TABLEVAR TABLE ([Id] [int] ,[Name] [nvarchar](max),[ParentId] [int],[Icon] [nvarchar](max))
                            ;WITH name_tree AS 
                            (
                                SELECT [Id]
                                    ,[Name]
                                    ,[ParentId]
                                    ,[Icon]
                                FROM [dbo].[FolderHierarchy]
                                WHERE {columnName} LIKE '%' + @columnValue + '%' 
                                UNION ALL
                                SELECT C.[Id]
                                    ,C.[Name]
                                    ,C.[ParentId]
                                    ,C.[Icon]
                                FROM [dbo].[FolderHierarchy] C
                                JOIN name_tree P ON C.[Id] = P.[ParentId]
                            ) 
                            INSERT INTO @TABLEVAR
                            SELECT *
                            FROM name_tree
                            OPTION (MAXRECURSION 0)

                            SELECT DISTINCT * FROM @TABLEVAR", columnValue).ToList();


            return fdlHis;
        }
    }
}
